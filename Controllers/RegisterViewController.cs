using System;
using CoreGraphics;
using Feelknit.Model;
using UIKit;
using Feelknit.iOS.Helpers;
using CoreLocation;
using MapKit;
using Feelknit.iOS.Views;
using Newtonsoft.Json;
using DSoft.Messaging;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
			DisableRegisterButton ();
			Manager.StartLocationUpdates ();

			RegisterUserName.EditingChanged += HandleEditingChanged;
			RegistrationPassword.EditingChanged += HandleEditingChanged;
			RegistrationEmail.EditingChanged += HandleEditingChanged;
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
				if(!Validate())
				{
					return;
				}

				View.Add(LoadingOverlay);

                var user = new User { UserName = RegisterUserName.Text, 
					Password = RegistrationPassword.Text, 
					EmailAddress = RegistrationEmail.Text,
					Longitude = longitude,
					Latitude = latitude,
					DeviceName = UIScreen.MainScreen.Bounds.Height.ToString()
				};
                SaveUser(user);
            };
        }

		void HandleEditingChanged (object sender, EventArgs e)
		{
			if (RegisterUserName.Text.Length > 0 && RegistrationPassword.Text.Length > 0 &&  RegistrationEmail.Text.Length > 0) {
				RegisterButton.BackgroundColor = Resources.LoginButtonColor;
				RegisterButton.Enabled = true;
			} else {
				DisableRegisterButton ();
			}
		}

		void DisableRegisterButton ()
		{
			RegisterButton.BackgroundColor = Resources.DisabledColor;
			RegisterButton.Enabled = false;
		}


		bool Validate()
		{
			if (!Regex.IsMatch (RegistrationEmail.Text, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")) {
				ShowMessage ("Please enter valid email.");
				return false;
			}
			if (RegisterUserName.Text.Length < 3) {
				ShowMessage ("Please enter at least 3 characters for username");
				return false;
			}
			if (RegistrationPassword.Text.Length < 6) {
				ShowMessage ("Please enter at least 6 characters for password");
				return false;
			}
			return true;
		}

		private void ShowMessage(string message)
		{
			//var pleaseEnterValidEmail = ;
			var alert = new UIAlertView ("Error", message, null, "OK",null);
			alert.Show();
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
                Frame = new CGRect(10, 0, 20, 20)
            };

            var leftView = new UIView(new CGRect(0, 0, 30, 20));
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
