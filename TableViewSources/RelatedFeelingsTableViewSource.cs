using System;
using UIKit;
using Feelknit.iOS.Model;
using System.Collections.Generic;
using System.Linq;

namespace Feelknit.iOS
{
	public class RelatedFeelingsTableViewSource: UITableViewSource
	{
		IList<Feeling> _feelings;

		Action<Feeling> _action;

		public RelatedFeelingsTableViewSource (IList<Feeling> feelings, Action<Feeling> action)
		{
			_action = action;
			_feelings = feelings;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return _feelings.ToList().Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var feeling = _feelings[indexPath.Row];
			var cell = (RelatedFeelingTableCellView)tableView.DequeueReusableCell (RelatedFeelingTableCellView.Key);
			if (cell == null) {
				cell = RelatedFeelingTableCellView.Create ();
				};

			cell.Feeling = feeling;

			return cell;
		}

		public override void RowSelected (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			if(!_feelings[indexPath.Row].IsReported)
				_action.Invoke (_feelings[indexPath.Row]);
		}

		public override nfloat GetHeightForRow (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			return 175;
		}

	}
}

