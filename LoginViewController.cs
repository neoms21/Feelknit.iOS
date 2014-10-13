using System;
using System.Drawing;
using System.Threading.Tasks;
using Feelknit.Model;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Feelknit
{
    partial class LoginViewController : UIViewController
    {
        public LoginViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
//            var isAuthenticated = NSUserDefaults.StandardUserDefaults.BoolForKey("IsAuthenticated");
//            if (isAuthenticated)
//            {
//                NavigateToAddFeeling();
//                return;
//            }
		
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
            var client = new JsonHttpClient("Users/Verify");
            var result = await client.PostRequest(user);

            if (bool.Parse(result))
            {
                NSUserDefaults.StandardUserDefaults.SetBool(true, "IsAuthenticated");
                NavigateToAddFeeling();
                return;

            };

            var alert = new UIAlertView("Error", "Invalid username/password", null, "OK", null);
            alert.Show();
        }

        private void NavigateToAddFeeling()
        {
            var addFeelingController =
                this.Storyboard.InstantiateViewController("AddFeelingViewController") as AddFeelingViewController;
            if (addFeelingController != null)
            {
                this.NavigationController.PushViewController(addFeelingController, true);
            }
        }
    }
}
