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
		MonoTouch.UIKit.UIButton AddCommentButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel CommentsCountLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView CommentsTable { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView CommentTextView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView UserIcon { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel UserNameLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
