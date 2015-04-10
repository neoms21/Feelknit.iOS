using System;
using System.Drawing;
using Feelknit.Model;
using MonoTouch.UIKit;
using Feelknit.iOS.Helpers;
using MonoTouch.CoreLocation;
using MonoTouch.MapKit;
using Feelknit.iOS.Views;
using Newtonsoft.Json;
using DSoft.Messaging;
using System.Threading.Tasks;

namespace Feelknit.iOS.Controllers
{
    partial class RegisterViewController : BaseController
    {
		private double latitude = 0.0;
		private double longitude = 0.0;

        public RegisterViewController(IntPtr handle)
            : base(handle)
        {
        }
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			NavigationController.NavigationBarHidden = true;
			this.View.BackgroundColor = Resources.MainBackgroundColor;
			RegisterButton.SetTitleColor(Resources.WhiteColor, UIControlState.Normal);
			SetImageAndMargin(RegisterUserName, "userIcon.png");
			SetImageAndMargin(RegistrationPassword, "password.png");
			SetImageAndMargin(RegistrationEmail, "004.png");
			SetImageAndMargin(LocationTextView, "compass.png");
			RegisterButton.BackgroundColor = Resources.LoginButtonColor;
			Manager.StartLocationUpdates ();
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			Manager.LocationUpdated += HandleLocationChanged;
			this.RegistrationEmail.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder();
				return true; 
			};
			LoadingOverlay = new LoadingOverlay(UIScreen.MainScreen.Bounds, "Registering");
            RegisterButton.TouchUpInside += (sender, e) =>
            {
				View.Add(LoadingOverlay);

                var user = new User { UserName = RegisterUserName.Text, 
					Password = RegistrationPassword.Text, 
					EmailAddress = RegistrationEmail.Text,
					Longitude = longitude,
					Latitude = latitude
				};
                SaveUser(user);
            };
        }

        private async void SaveUser(User user)
		{
			var client = new JsonHttpClient (UrlHelper.USER);
			var result = await client.PostRequest (user);

			LoadingOverlay.Hide ();
			var loginResult = JsonConvert.DeserializeObject<LoginResult> (result);
			if (loginResult.IsLoginSuccessful) {

				ApplicationHelper.UserName = RegisterUserName.Text;
				ApplicationHelper.EmailAddress = RegistrationEmail.Text;
				ApplicationHelper.IsAuthenticated = true;
				ApplicationHelper.AuthorizationToken = loginResult.Token;

				await Task.Factory.StartNew(async () =>
					{
						client = new JsonHttpClient(UrlHelper.USER_KEY);
						user.IosKey = ApplicationHelper.ApnsToken;
						await client.PostRequest(user);
					});

				//send it
				MessageBus.Default.Post (new CoreMessageBusEvent (Constants.UserDetailsUpdateEvent) {
					Sender = this,
				});

				MoveToNextController (typeof(AvatarViewController).Name,false);
			} else {
				var alert = new UIAlertView("Error", loginResult.Error, null, "OK", null);
				alert.Show();
			}
		
        }

		private void SetImageAndMargin(UITextField uiTextField, string image)
        {
            var imageView = new UIImageView(UIImage.FromBundle(image))
            {
                // Indent it 10 pixels from the left.
                Frame = new RectangleF(10, 0, 20, 20)
            };

            var leftView = new UIView(new Rectangle(0, 0, 30, 20));
            leftView.AddSubview(imageView);
            uiTextField.LeftViewMode = UITextFieldViewMode.Always;
            uiTextField.LeftView = leftView;
        }

		public async void HandleLocationChanged (object sender, LocationUpdatedEventArgs e)
		{
			// Handle foreground updates
			CLLocation location = e.Location;

			latitude = location.Coordinate.Latitude;
			longitude = location.Coordinate.Longitude;

			var geoCoder = new CLGeocoder();

			var placemarks = await geoCoder.ReverseGeocodeLocationAsync(location);

			if (placemarks != null) {
				var placemark = placemarks [0];
				LocationTextView.Text = placemark.Locality;
			}

		}
    }
}
