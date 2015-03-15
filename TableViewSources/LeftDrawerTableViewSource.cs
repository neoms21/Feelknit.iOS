﻿using System;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace Feelknit.iOS
{
	public class LeftDrawerTableViewSource : UITableViewSource
	{
		List<LeftDrawerItem> _items;
		public LeftDrawerTableViewSource (List<LeftDrawerItem> items)
		{
			_items = items;
		}


		private const string CellIdentifier = "LeftDrawerTableCell";

		readonly Action<int> _action;

	

		public override int RowsInSection (UITableView tableview, int section)
		{
			return _items.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var item = _items[indexPath.Row];
			var cell = (LeftDrawerTableCellView)tableView.DequeueReusableCell (LeftDrawerTableCellView.Key);
			if (cell == null) {
				cell = LeftDrawerTableCellView.Create ();
			}
			cell.LeftDrawerItem = item;

			return cell;
		}

		public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			_action.Invoke (_items[indexPath.Row].Id);
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			//throw new NotImplementedException ();
		}
	}
}
