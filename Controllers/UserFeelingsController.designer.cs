// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace Feelknit.iOS.Controllers
{
	[Register ("UserFeelingsController")]
	partial class UserFeelingsController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton CreateNewFeelingButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView UserFeelingsTableView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (CreateNewFeelingButton != null) {
				CreateNewFeelingButton.Dispose ();
				CreateNewFeelingButton = null;
			}
			if (UserFeelingsTableView != null) {
				UserFeelingsTableView.Dispose ();
				UserFeelingsTableView = null;
			}
		}
	}
}
