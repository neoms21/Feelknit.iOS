// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using UIKit;
using System;
using System.CodeDom.Compiler;

namespace Feelknit.iOS.Controllers
{
	[Register ("AddFeelingViewController")]
	partial class AddFeelingViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView FeelingsTableView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField ReasonText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton SelectFeelingButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ShareFeelingButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (FeelingsTableView != null) {
				FeelingsTableView.Dispose ();
				FeelingsTableView = null;
			}
			if (ReasonText != null) {
				ReasonText.Dispose ();
				ReasonText = null;
			}
			if (SelectFeelingButton != null) {
				SelectFeelingButton.Dispose ();
				SelectFeelingButton = null;
			}
			if (ShareFeelingButton != null) {
				ShareFeelingButton.Dispose ();
				ShareFeelingButton = null;
			}
		}
	}
}
