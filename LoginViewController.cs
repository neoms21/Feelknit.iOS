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


            RegistrationButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                // Launches a new instance of CallHistoryController
                var registration = this.Storyboard.InstantiateViewController(typeof(RegistrationViewController).Name)
                    as RegistrationViewController;
                if (registration != null)
                {
                    this.NavigationController.PushViewController(registration, true);
                }
            };
	    }
	}
}
