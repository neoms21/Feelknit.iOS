using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Feelknit.iOS.Views;

namespace Feelknit.iOS
{
	partial class LoadingViewController : UIViewController
	{
		private LoadingOverlay _loadingOverlay;

		public LoadingViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.NavigationController.NavigationBarHidden = true;
		}
	}
}
