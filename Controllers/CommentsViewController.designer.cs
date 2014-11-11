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
		UITextView FeelingTextView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField NewCommentTextField { get; set; }

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
			if (FeelingTextView != null) {
				FeelingTextView.Dispose ();
				FeelingTextView = null;
			}
			if (NewCommentTextField != null) {
				NewCommentTextField.Dispose ();
				NewCommentTextField = null;
			}
			if (UserNameLabel != null) {
				UserNameLabel.Dispose ();
				UserNameLabel = null;
			}
		}
	}
}
