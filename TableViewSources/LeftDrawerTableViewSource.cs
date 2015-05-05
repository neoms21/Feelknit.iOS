using System;
using UIKit;
using System.Collections.Generic;

namespace Feelknit.iOS
{
	public class LeftDrawerTableViewSource : UITableViewSource
	{
		List<LeftDrawerItem> _items;

		readonly Action<Container> _action;

		public LeftDrawerTableViewSource (List<LeftDrawerItem> items, Action<Container> action)
		{
			_items = items;
			_action = action;
		}

		private const string CellIdentifier = "LeftDrawerTableCell";


		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return _items.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var item = _items[indexPath.Row];
			var cell = (LeftDrawerTableCellView)tableView.DequeueReusableCell (LeftDrawerTableCellView.Key);
			if (cell == null) {
				cell = LeftDrawerTableCellView.Create ();
			}
			cell.LeftDrawerItem = item;

			return cell;
		}

		public override void RowSelected (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			_action.Invoke (_items[indexPath.Row].Container);
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			//throw new NotImplementedException ();
		}
	}
}

