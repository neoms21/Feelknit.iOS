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
