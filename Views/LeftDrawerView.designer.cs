// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Feelknit.iOS
{
	partial class LeftDrawerView
	{
		[Outlet]
		MonoTouch.UIKit.UITableView LeftDrawerTableView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton SignoutButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView UserImageView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel UserNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (UserImageView != null) {
				UserImageView.Dispose ();
				UserImageView = null;
			}

			if (UserNameLabel != null) {
				UserNameLabel.Dispose ();
				UserNameLabel = null;
			}

			if (LeftDrawerTableView != null) {
				LeftDrawerTableView.Dispose ();
				LeftDrawerTableView = null;
			}

			if (SignoutButton != null) {
				SignoutButton.Dispose ();
				SignoutButton = null;
			}
		}
	}
}
