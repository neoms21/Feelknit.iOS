using System.Collections.Generic;
using System.Linq;
using Feelknit.iOS.Model;
using Feelknit.iOS.Views;
using Feelknit.Model;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Feelknit.iOS.TableViewSources
{
    public class UserFeelingsTableViewSource : UITableViewSource
    {
        private IList<Feeling> _feelings;
        string cellIdentifier = "TableCell";


        public UserFeelingsTableViewSource(IList<Feeling> feelings)
        {
            _feelings = feelings;
        }


        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(cellIdentifier) as UserFeelingCellView ??
                                   new UserFeelingCellView(cellIdentifier);
            // if there are no cells to reuse, create a new one
            var feeling = _feelings[indexPath.Row];
            cell.UpdateCell(feeling.GetFeelingFormattedText("I"), string.Format("{0} Comments", feeling.Comments.Count),
                string.Format("{0} Suppport", feeling.SupportCount), feeling.FeelingDate.ToString("yy-mm-dd"));
            return cell;
        }

        public override int RowsInSection(UITableView tableview, int section)
        {
            return _feelings.Count();
        }

        public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 80;
        }
    }
}