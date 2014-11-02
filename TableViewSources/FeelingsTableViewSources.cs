using System;
using MonoTouch.UIKit;

namespace Feelknit.iOS
{
	public class FeelingsTableViewSources : UITableViewSource
	{
		string[] tableItems;
		string cellIdentifier = "TableCell";

		Action<string> _action;

		public FeelingsTableViewSources (string [] items, Action<string> action)
		{
			_action = action;
			tableItems = items;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return tableItems.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
			cell.TextLabel.Text = tableItems[indexPath.Row];
			return cell;
		}

		public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			_action.Invoke (tableItems[indexPath.Row]);
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			//throw new NotImplementedException ();
		}
	}
}

