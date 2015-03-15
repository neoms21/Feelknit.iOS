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
	[Register ("LeftDrawerTableCellView")]
	partial class LeftDrawerTableCellView
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView LeftDrawerItemImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel LeftDrawerItemText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LeftDrawerItemImage != null) {
				LeftDrawerItemImage.Dispose ();
				LeftDrawerItemImage = null;
			}

			if (LeftDrawerItemText != null) {
				LeftDrawerItemText.Dispose ();
				LeftDrawerItemText = null;
			}
		}
	}
}
