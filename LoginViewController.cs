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
			var imageView = new UIImageView(UIImage.FromBundle("usericon.png"))
			{
				// Indent it 10 pixels from the left.
				Frame = new RectangleF(10,0,20,20)
			};

			UIView objLeftView = new UIView(new Rectangle(0,0,30,20));
			objLeftView.AddSubview(imageView);
			//and then on the UITextField set the LeftView as such:-

			UserName.LeftViewMode = UITextFieldViewMode.Always;
			UserName.LeftView = objLeftView;
//
//            var imageView = new UIImageView(UIImage.FromBundle("usericon.png"))
//            {
//                Frame = new RectangleF(new PointF(20, 1), new SizeF(20, 20))
//            };
//            UserName.LeftViewMode = UITextFieldViewMode.Always;
//            UserName.LeftView = imageView;

            var imageView2 = new UIImageView(UIImage.FromBundle("password.png"))
            {
                Frame = new RectangleF(new PointF(20, 1), new SizeF(20, 20))
            };
            Password.LeftViewMode = UITextFieldViewMode.Always;
            Password.LeftView = imageView2;

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
