using MonoTouch.UIKit;

namespace Feelknit.iOS.TableViewSources
{
    public class SideMenuTableViewSource : UITableViewSource
    {
        string[] tableItems;
        string cellIdentifier = "TableCell";

        public SideMenuTableViewSource(string[] items)
        {
            tableItems = items;
        }
        public override int RowsInSection(UITableView tableview, int section)
        {
            return tableItems.Length;
        }
        public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            // if there are no cells to reuse, create a new one
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
            cell.TextLabel.Text = tableItems[indexPath.Row];
            return cell;
        }

        public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
        {
            UIAlertView alertview = new UIAlertView("tableiTem", tableItems[indexPath.Row], null, "ok", null);
            alertview.Show();
        }
    }
}