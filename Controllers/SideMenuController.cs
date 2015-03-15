using Feelknit.iOS.TableViewSources;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Feelknit.iOS.Controllers
{
    public class SideMenuController : BaseController
    {
        public SideMenuController()
            : base(null, null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			View = LeftDrawerView.Create();
            View.BackgroundColor = UIColor.FromRGB(.9f, .9f, .9f);

//            var tableView = new UITableView(View.Bounds);
//            tableView.Source = new SideMenuTableViewSource(new[] { "Related Feelings", "Comments", " My Feelings" });
//
//            View.Add(tableView);
            //			View.Add(body);
            //			View.Add(introButton);
            //			View.Add(contentButton);
        }
    }
}