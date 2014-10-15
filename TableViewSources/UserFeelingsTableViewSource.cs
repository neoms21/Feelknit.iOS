using System.Collections.Generic;
using System.Linq;
using Feelknit.iOS.Model;
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
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            // if there are no cells to reuse, create a new one
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
            cell.TextLabel.Text = _feelings[indexPath.Row].FeelingText;
            return cell;
        }

        public override int RowsInSection(UITableView tableview, int section)
        {
            return _feelings.Count();
        }
    }
}