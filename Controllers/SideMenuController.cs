using Feelknit.iOS.TableViewSources;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;

namespace Feelknit.iOS.Controllers
{
    public class SideMenuController : BaseController
    {
		public Action<bool> Action { get; set; }

        public SideMenuController()
            : base(null, null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			View = LeftDrawerView.Create(DrawerItemSelected);
            View.BackgroundColor = UIColor.FromRGB(.9f, .9f, .9f);
        }

		public void DrawerItemSelected (string id)
		{
			MoveToNextController (id);
			if (Action != null)
				Action.Invoke (true);
		}
    }
}