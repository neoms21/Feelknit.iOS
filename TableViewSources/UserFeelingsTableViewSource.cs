using System.Collections.Generic;
using System.Linq;
using Feelknit.iOS.Model;
using Feelknit.iOS.Views;
using Feelknit.Model;
using Foundation;
using UIKit;
using System;

namespace Feelknit.iOS.TableViewSources
{
    public class UserFeelingsTableViewSource : UITableViewSource
    {
        private IList<Feeling> _feelings;
        string cellIdentifier = "TableCell";

		Action<Feeling> _action;

		public UserFeelingsTableViewSource(IList<Feeling> feelings, Action<Feeling> action)
		{
			_action = action;
			_feelings = feelings;
		}


        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(cellIdentifier) as UserFeelingCellView ??
                                   new UserFeelingCellView(cellIdentifier);
            // if there are no cells to reuse, create a new one
            var feeling = _feelings[indexPath.Row];
            cell.UpdateCell(feeling.GetFeelingFormattedText("I"), string.Format("{0} Comments", feeling.Comments.Count),
                string.Format("{0} Suppport", feeling.SupportCount), feeling.FeelingDate.ToString("dd-MMM-yyyy HH:mm"));
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _feelings.Count();
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 80;
        }

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			_action.Invoke (_feelings[indexPath.Row]);
		}
    }
}