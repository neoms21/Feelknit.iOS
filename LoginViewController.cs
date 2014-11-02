using System;
using System.Drawing;
using System.Threading.Tasks;
using Feelknit.iOS;
using Feelknit.Model;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Feelknit.iOS.Views;

namespace Feelknit
{
    partial class LoginViewController : UIViewController
    {
		private LoadingOverlay _loadingOverlay;

        public LoginViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			_loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds, "Logging In");
            var isAuthenticated = NSUserDefaults.StandardUserDefaults.BoolForKey("IsAuthenticated");
            if (isAuthenticated)
            {
				ApplicationHelper.UserName = NSUserDefaults.StandardUserDefaults.StringForKey ("UserName");	
                NavigateToAddFeeling();
                return;
            }

            SetImageAndMargin(UserName, "usericon.png");
            SetImageAndMargin(Password, "password.png");

            RegisterButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                // Launches a new instance of RegistrationController
                var registration = this.Storyboard.InstantiateViewController("RegisterViewController") as RegisterViewController;
                if (registration != null)
                {
                    this.NavigationController.PushViewController(registration, true);
                }
            };

            LoginButton.TouchUpInside += (object sender, EventArgs e) =>
                        {
				this.View.Add(_loadingOverlay);
                            var user = new User { UserName = UserName.Text, Password = Password.Text };
                            VerifyUser(user);
                        };
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

        private async void VerifyUser(User user)
        {
			var client = new JsonHttpClient(UrlHelper.USER_VERIFY);
            var result = await client.PostRequest(user);

            if (bool.Parse(result))
            {
				_loadingOverlay.Hide ();
                NSUserDefaults.StandardUserDefaults.SetBool(true, "IsAuthenticated");
				NSUserDefaults.StandardUserDefaults.SetString( UserName.Text, "UserName");
                NavigateToAddFeeling();
                return;

            };

            var alert = new UIAlertView("Error", "Invalid username/password", null, "OK", null);
            alert.Show();
        }

        private void NavigateToAddFeeling()
        {
            var userFeelingsController =
                this.Storyboard.InstantiateViewController("UserFeelingsController") as UserFeelingsController;
            if (userFeelingsController != null)
            {
                this.NavigationController.PushViewController(userFeelingsController, true);
            }
        }
    }
}
