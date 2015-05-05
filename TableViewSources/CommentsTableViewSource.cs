using System;
using UIKit;
using Feelknit.Model;
using System.Collections.Generic;
using Foundation;
using System.Linq;
using Feelknit.iOS.Model;

namespace Feelknit.iOS
{
	public class CommentsTableViewSource: UITableViewSource
	{
		private List<Comment> _comments;
		private Feeling _feeling;

		public CommentsTableViewSource (Feeling feeling)
		{
			_feeling = feeling;
			_comments = feeling.Comments.ToList();
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return _comments.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var comment =_comments[indexPath.Row];
			var cell = (CommentCellView)tableView.DequeueReusableCell (CommentCellView.Key);
			if (cell == null) {
				cell = CommentCellView.Create ();
			}
			cell.Comment = comment;
			cell.Feeling = _feeling;

			return cell;
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			var comment = _comments[(int)indexPath.Item];

			var f = EstimateHeight (comment.Text, UIScreen.MainScreen.Bounds.Width, UIFont.FromName ("Helvetica",14));
			return f;
		}

		private float EstimateHeight(String text, nfloat width, UIFont font)
		{

			CoreGraphics.CGSize size = ((NSString)text).StringSize (font, new CoreGraphics.CGSize (width, float.MaxValue),
				UILineBreakMode.WordWrap);
			return (float)size.Height + 130; // The 50 is just padding
		}

	}
}

