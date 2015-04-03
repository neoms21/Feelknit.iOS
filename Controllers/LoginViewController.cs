using System;
using System.Drawing;
using System.Threading.Tasks;
using Feelknit.iOS.Helpers;
using Feelknit.iOS.Views;
using Feelknit.Model;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Newtonsoft.Json;
using DSoft.Messaging;

namespace Feelknit.iOS.Controllers
{
    partial class LoginViewController : BaseController
    {
        //

		public LoginViewController(IntPtr handle)
            : base(handle)
        {
        }


        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.View.BackgroundColor = Resources.MainBackgroundColor;
			this.LoginButton.BackgroundColor = Resources.LoginButtonColor;
			RegisterButton.BackgroundColor = Resources.LoginButtonColor;
			NavigationController.NavigationBarHidden = true;
			LoginButton.SetTitleColor(Resources.WhiteColor, UIControlState.Normal);
			RegisterButton.SetTitleColor(Resources.WhiteColor, UIControlState.Normal);

        }


        public override bool ShouldAutorotate()
        {
            return false;
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return UIInterfaceOrientationMask.Portrait;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			LoadingOverlay = new LoadingOverlay(UIScreen.MainScreen.Bounds, "Logging In");

            SetImageAndMargin(UserName, "userIcon.png");
            SetImageAndMargin(Password, "password.png");

            this.Password.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };
            RegisterButton.TouchUpInside += (sender, e) =>
            {
                // Launches a new instance of RegistrationController
                var registration = MainStoryboard.InstantiateViewController("RegisterViewController") as RegisterViewController;
                if (registration != null)
                {
                    NavController.PushViewController(registration, true);
                }
            };

            LoginButton.TouchUpInside += (sender, e) =>
                        {
				View.Add(LoadingOverlay);
                            var user = new User { UserName = UserName.Text, Password = Password.Text };
                            VerifyUser(user);
                        };
			this.Password.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder();
				return true; 
			};
        }

        private void SetImageAndMargin(UITextField uiTextField, string image)
        {
			uiTextField.BackgroundColor = Resources.WhiteColor;
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

        private async void VerifyUser(User user)
        {
            var client = new JsonHttpClient(UrlHelper.USER_VERIFY);
            var result = await client.PostRequest(user);
			LoadingOverlay.Hide();

            var loginResult = JsonConvert.DeserializeObject<LoginResult>(result);
            if (loginResult.IsLoginSuccessful)
            {
				ApplicationHelper.UserName = UserName.Text;
				ApplicationHelper.EmailAddress = loginResult.UserEmail;
				ApplicationHelper.IsAuthenticated = true;
				ApplicationHelper.AuthorizationToken = loginResult.Token;
				ApplicationHelper.Avatar = loginResult.Avatar;

//                NSUserDefaults.StandardUserDefaults.SetBool(true, "IsAuthenticated");
//                NSUserDefaults.StandardUserDefaults.SetString(UserName.Text, "UserName");
				//Creare a MessageBusEvent
			

				//send it
				MessageBus.Default.Post (new CoreMessageBusEvent (Constants.UserDetailsUpdateEvent) {
					Sender = this,
					});

                await Task.Factory.StartNew(async () =>
                {
                    client = new JsonHttpClient(UrlHelper.USER_KEY);
						user.IosKey = ApplicationHelper.ApnsToken;
                    await client.PostRequest(user);
                });

				MoveToNextController(typeof(CurrentFeelingsViewController).Name);
                return;
            }

            var alert = new UIAlertView("Error", "Invalid username/password", null, "OK", null);
            alert.Show();
        }

    }
}
