// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Feelknit.iOS
{
	[Register ("CommentCellView")]
	partial class CommentCellView
	{
		[Outlet]
		UIKit.UILabel CommentReportedLabel { get; set; }

		[Outlet]
		UIKit.UILabel CommentTextLabel { get; set; }

		[Outlet]
		UIKit.UITextView CommentTextView { get; set; }

		[Outlet]
		UIKit.UILabel CommentTimeLabel { get; set; }

		[Outlet]
		UIKit.UIButton ReportAbuseButton { get; set; }

		[Outlet]
		UIKit.UIButton ReportCommentButton { get; set; }

		[Outlet]
		UIKit.UIImageView UserIconImageView { get; set; }

		[Outlet]
		UIKit.UILabel UserNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CommentReportedLabel != null) {
				CommentReportedLabel.Dispose ();
				CommentReportedLabel = null;
			}

			if (CommentTextLabel != null) {
				CommentTextLabel.Dispose ();
				CommentTextLabel = null;
			}

			if (CommentTextView != null) {
				CommentTextView.Dispose ();
				CommentTextView = null;
			}

			if (CommentTimeLabel != null) {
				CommentTimeLabel.Dispose ();
				CommentTimeLabel = null;
			}

			if (ReportAbuseButton != null) {
				ReportAbuseButton.Dispose ();
				ReportAbuseButton = null;
			}

			if (UserIconImageView != null) {
				UserIconImageView.Dispose ();
				UserIconImageView = null;
			}

			if (UserNameLabel != null) {
				UserNameLabel.Dispose ();
				UserNameLabel = null;
			}

			if (ReportCommentButton != null) {
				ReportCommentButton.Dispose ();
				ReportCommentButton = null;
			}
		}
	}
}
