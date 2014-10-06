using System;
using System.Threading.Tasks;
using Feelknit.Model;
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


            //var imageView = new UIImageView(UIImage.FromBundle("usericon.png"));
            //imageView.Frame = new RectangleF(1,1, imageView.Image.CGImage.Width, imageView.Image.CGImage.Height);
            //UserName.LeftViewMode = UITextFieldViewMode.Always;
            //UserName.LeftView = imageView;
            //			RegisterButton.TouchUpInside += (object sender, EventArgs e) =>
            //            {
            //                // Launches a new instance of RegistrationController
            //                var registration = this.Storyboard.InstantiateViewController("RegisterViewController") as RegisterViewController;
            //                if (registration != null)
            //                {
            //                    this.NavigationController.PushViewController(registration, true);
            //                }
            //            };

            LoginButton.TouchUpInside += (object sender, EventArgs e) =>
                        {
                            var user = new User { UserName = UserName.Text, Password = Password.Text };
                            VerifyUser(user);
                        };
        }

        private async void VerifyUser(User user)
        {
            var client = new JsonHttpClient("Users/Verify");
            var result = await client.PostRequest(user);

            if (bool.Parse(result))
            {

				var addFeelingController = this.Storyboard.InstantiateViewController("AddFeelingViewController") as AddFeelingViewController;
				if (addFeelingController != null)
				                {
					this.NavigationController.PushViewController(addFeelingController, true);
				                }
				return;

            };

            var alert = new UIAlertView("Error", "Invalid username/password", null, "OK", null);
            alert.Show();
        }

    }
}
