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

namespace Feelknit.iOS.Controllers
{
	[Register ("CommentsViewController")]
	partial class CommentsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel CommentsCountLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView CommentsTable { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField CommentText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView FeelingTextView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView SubmitCommentButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView UserIcon { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel UserNameLabel { get; set; }

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
			if (CommentText != null) {
				CommentText.Dispose ();
				CommentText = null;
			}
			if (FeelingTextView != null) {
				FeelingTextView.Dispose ();
				FeelingTextView = null;
			}
			if (SubmitCommentButton != null) {
				SubmitCommentButton.Dispose ();
				SubmitCommentButton = null;
			}
			if (UserIcon != null) {
				UserIcon.Dispose ();
				UserIcon = null;
			}
			if (UserNameLabel != null) {
				UserNameLabel.Dispose ();
				UserNameLabel = null;
			}
		}
	}
}
