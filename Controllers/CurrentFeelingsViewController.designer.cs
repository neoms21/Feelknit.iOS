// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace Feelknit.iOS
{
	[Register ("CurrentFeelingsViewController")]
	partial class CurrentFeelingsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel RecentFeelingsLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView RecentFeelingsTableView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (RecentFeelingsLabel != null) {
				RecentFeelingsLabel.Dispose ();
				RecentFeelingsLabel = null;
			}
			if (RecentFeelingsTableView != null) {
				RecentFeelingsTableView.Dispose ();
				RecentFeelingsTableView = null;
			}
		}
	}
}
