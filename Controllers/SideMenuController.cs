using Feelknit.iOS.TableViewSources;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using Feelknit.iOS.Helpers;

namespace Feelknit.iOS.Controllers
{
    public class SideMenuController : BaseController
    {
		public Action<bool> Action { get; set; }

        public SideMenuController()
            : base(null, null)
        {
        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			View = LeftDrawerView.Create(DrawerItemSelected,SignOutHandler);
            View.BackgroundColor = UIColor.FromRGB(.9f, .9f, .9f);
        }

		public void DrawerItemSelected (string id)
		{
			if (string.IsNullOrWhiteSpace (id)) {
				Action.Invoke (true);
				return;
			}

			MoveToNextController (id);
				Action.Invoke (true);
		}

		public void SignOutHandler ()
		{
			ApplicationHelper.UserName = string.Empty;
			ApplicationHelper.EmailAddress = string.Empty;
			ApplicationHelper.Avatar = string.Empty;
			ApplicationHelper.ApnsToken = string.Empty;
			ApplicationHelper.AuthorizationToken = string.Empty;
			ApplicationHelper.IsAuthenticated = false;
			InvokeOnMainThread(()=> {MoveToNextController (typeof(LoginViewController).Name);});
		}
    }
}