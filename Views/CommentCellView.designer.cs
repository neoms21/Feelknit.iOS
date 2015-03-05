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
	[Register ("CommentCellView")]
	partial class CommentCellView
	{
		[Outlet]
		MonoTouch.UIKit.UITextView CommentTextView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel CommentTimeLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel ReportAbuseLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView UserIconImageView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel UserNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (UserIconImageView != null) {
				UserIconImageView.Dispose ();
				UserIconImageView = null;
			}

			if (UserNameLabel != null) {
				UserNameLabel.Dispose ();
				UserNameLabel = null;
			}

			if (ReportAbuseLabel != null) {
				ReportAbuseLabel.Dispose ();
				ReportAbuseLabel = null;
			}

			if (CommentTextView != null) {
				CommentTextView.Dispose ();
				CommentTextView = null;
			}

			if (CommentTimeLabel != null) {
				CommentTimeLabel.Dispose ();
				CommentTimeLabel = null;
			}
		}
	}
}
