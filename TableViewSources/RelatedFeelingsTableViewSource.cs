using System;
using MonoTouch.UIKit;
using Feelknit.iOS.Model;
using System.Collections.Generic;
using System.Linq;

namespace Feelknit.iOS
{
	public class RelatedFeelingsTableViewSource: UITableViewSource
	{
		IList<Feeling> _feelings;
		string cellIdentifier = "TableCell";

		Action<string> _action;

		public RelatedFeelingsTableViewSource (IList<Feeling> feelings, Action<string> action)
		{
			_action = action;
			_feelings = feelings;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return _feelings.ToList().Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var feeling = _feelings[indexPath.Row];
			var cell = (RelatedFeelingTableCellView)tableView.DequeueReusableCell (RelatedFeelingTableCellView.Key);
			if (cell == null) {
				cell = RelatedFeelingTableCellView.Create ();
			}
			cell.Feeling = feeling;

			return cell;
		}

		public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			_action.Invoke (_feelings[indexPath.Row].Id);
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			//throw new NotImplementedException ();
		}

		public override float GetHeightForRow (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			return 175;
		}
	}
}

