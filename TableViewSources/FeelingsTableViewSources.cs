using System;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace Feelknit.iOS.TableViewSources
{
	public class FeelingsTableViewSources : UITableViewSource
	{
	    readonly IList<string> _feelings;
	    private const string CellIdentifier = "TableCell";

	    readonly Action<string> _action;

		public FeelingsTableViewSources (IList<string> feelings, Action<string> action)
		{
			_action = action;
			_feelings = feelings;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return _feelings.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier);
			cell.TextLabel.Text = _feelings[indexPath.Row];
			return cell;
		}

		public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			_action.Invoke (_feelings[indexPath.Row]);
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			//throw new NotImplementedException ();
		}
	}
}

