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
	[Register ("CommentsViewController")]
	partial class CommentsViewController
	{
		[Outlet]
		UIKit.UIButton AddCommentButton { get; set; }

		[Outlet]
		UIKit.UILabel CommentsCountLabel { get; set; }

		[Outlet]
		UIKit.UITableView CommentsTable { get; set; }

		[Outlet]
		UIKit.UITextView CommentTextView { get; set; }

		[Outlet]
		UIKit.UIImageView UserIcon { get; set; }

		[Outlet]
		UIKit.UILabel UserNameLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
