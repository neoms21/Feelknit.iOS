// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace SlideDownMenu
{
	[Register ("MenuItemView")]
	partial class MenuItemView
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView BackgroundView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView ImageView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel Label { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView SeperatorView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (BackgroundView != null) {
				BackgroundView.Dispose ();
				BackgroundView = null;
			}

			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}

			if (Label != null) {
				Label.Dispose ();
				Label = null;
			}

			if (SeperatorView != null) {
				SeperatorView.Dispose ();
				SeperatorView = null;
			}
		}
	}
}
