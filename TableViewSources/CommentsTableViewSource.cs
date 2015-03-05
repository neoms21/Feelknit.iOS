using System;
using MonoTouch.UIKit;
using Feelknit.Model;
using System.Collections.Generic;

namespace Feelknit.iOS
{
	public class CommentsTableViewSource: UITableViewSource
	{private IList<Comment> _comments;

		public CommentsTableViewSource (IList<Comment> comments)
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

		public override float GetHeightForRow (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			return 275;
		}
	}
}

