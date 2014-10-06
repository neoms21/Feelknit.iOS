using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace Feelknit
{
	partial class LoginViewController : UIViewController
	{
		public LoginViewController (IntPtr handle) : base (handle)
		{
		}

	    public override void ViewDidLoad()
	    {
	        base.ViewDidLoad();

			UserName.RightViewMode = UITextFieldViewMode.WhileEditing;
			var imageView = new UIImageView (UIImage.FromBundle("usericon.png"));
			imageView.Frame = new RectangleF (10,10,imageView.Image.CGImage.Width, imageView.Image.CGImage.Height);
			UserName.LeftView = imageView;
//			RegisterButton.TouchUpInside += (object sender, EventArgs e) =>
//            {
//                // Launches a new instance of RegistrationController
//                var registration = this.Storyboard.InstantiateViewController("RegisterViewController") as RegisterViewController;
//                if (registration != null)
//                {
//                    this.NavigationController.PushViewController(registration, true);
//                }
//            };
	    }
	}
}
