using System;
using MonoTouch.UIKit;
using Feelknit.Model;
using System.Collections.Generic;
using MonoTouch.Foundation;
using System.Linq;

namespace Feelknit.iOS
{
	public class CommentsTableViewSource: UITableViewSource
	{
		private List<Comment> _comments;

		public CommentsTableViewSource (List<Comment> comments)
		{
			_comments = comments;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return _comments.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var comment =_comments[indexPath.Row];
			var cell = (CommentCellView)tableView.DequeueReusableCell (CommentCellView.Key);
			if (cell == null) {
				cell = CommentCellView.Create ();
			}
			cell.Comment = comment;

			return cell;
		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			var comment = _comments[indexPath.Item];

			var f = EstimateHeight (comment.Text, UIScreen.MainScreen.Bounds.Width, UIFont.FromName ("Helvetica",14));
			return f;
		}

		private float EstimateHeight(String text, float width, UIFont font)
		{

			System.Drawing.SizeF size = ((NSString)text).StringSize (font, new System.Drawing.SizeF (width, float.MaxValue),
				UILineBreakMode.WordWrap);
			return (float)size.Height + 130; // The 50 is just padding
		}

	}
}

