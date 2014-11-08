// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//

using System.CodeDom.Compiler;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Feelknit.iOS.Controllers
{
	[Register ("RelatedFeelingsViewController")]
	partial class RelatedFeelingsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel FeelingNumberLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel FeelingTextLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView RelatedFeelingsTable { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (FeelingNumberLabel != null) {
				FeelingNumberLabel.Dispose ();
				FeelingNumberLabel = null;
			}
			if (FeelingTextLabel != null) {
				FeelingTextLabel.Dispose ();
				FeelingTextLabel = null;
			}
			if (RelatedFeelingsTable != null) {
				RelatedFeelingsTable.Dispose ();
				RelatedFeelingsTable = null;
			}
		}
	}
}
