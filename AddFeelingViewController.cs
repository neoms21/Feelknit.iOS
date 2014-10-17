using System;
using MonoTouch.UIKit;

namespace Feelknit
{
	partial class AddFeelingViewController : UIViewController
	{
		public AddFeelingViewController (IntPtr handle) : base (handle)
		{
		}

	    public override void ViewDidLoad()
	    {
	        base.ViewDidLoad();
            this.NavigationController.NavigationBarHidden = false;

	    }
	}
}
