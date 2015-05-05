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
	[Register ("RelatedFeelingTableCellView")]
	partial class RelatedFeelingTableCellView
	{
		[Outlet]
		UIKit.UIButton CommentButton { get; set; }

		[Outlet]
		UIKit.UILabel CommentsLabel { get; set; }

		[Outlet]
		UIKit.UILabel FeelingDate { get; set; }

		[Outlet]
		UIKit.UILabel FeelingTextLabel { get; set; }

		[Outlet]
		UIKit.UIButton ReportButton { get; set; }

		[Outlet]
		UIKit.UILabel ReportedLabel { get; set; }

		[Outlet]
		UIKit.UIButton SupportButton { get; set; }

		[Outlet]
		UIKit.UILabel SupportLabel { get; set; }

		[Outlet]
		UIKit.UIImageView userImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CommentButton != null) {
				CommentButton.Dispose ();
				CommentButton = null;
			}

			if (CommentsLabel != null) {
				CommentsLabel.Dispose ();
				CommentsLabel = null;
			}

			if (FeelingDate != null) {
				FeelingDate.Dispose ();
				FeelingDate = null;
			}

			if (ReportedLabel != null) {
				ReportedLabel.Dispose ();
				ReportedLabel = null;
			}

			if (FeelingTextLabel != null) {
				FeelingTextLabel.Dispose ();
				FeelingTextLabel = null;
			}

			if (ReportButton != null) {
				ReportButton.Dispose ();
				ReportButton = null;
			}

			if (SupportButton != null) {
				SupportButton.Dispose ();
				SupportButton = null;
			}

			if (SupportLabel != null) {
				SupportLabel.Dispose ();
				SupportLabel = null;
			}

			if (userImageView != null) {
				userImageView.Dispose ();
				userImageView = null;
			}
		}
	}
}
