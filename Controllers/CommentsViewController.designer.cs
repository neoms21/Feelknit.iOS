// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Feelknit.iOS.Controllers
{
	[Register ("CommentsViewController")]
	partial class CommentsViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton AddCommentButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel CommentsCountLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView CommentsTable { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView CommentTextView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView FeelingTextView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView UserIcon { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel UserNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CommentsCountLabel != null) {
				CommentsCountLabel.Dispose ();
				CommentsCountLabel = null;
			}

			if (CommentsTable != null) {
				CommentsTable.Dispose ();
				CommentsTable = null;
			}

			if (CommentTextView != null) {
				CommentTextView.Dispose ();
				CommentTextView = null;
			}

			if (FeelingTextView != null) {
				FeelingTextView.Dispose ();
				FeelingTextView = null;
			}

			if (UserIcon != null) {
				UserIcon.Dispose ();
				UserIcon = null;
			}

			if (UserNameLabel != null) {
				UserNameLabel.Dispose ();
				UserNameLabel = null;
			}

			if (AddCommentButton != null) {
				AddCommentButton.Dispose ();
				AddCommentButton = null;
			}
		}
	}
}
