using System;
using Foundation;
using UIKit;
using Feelknit.iOS.Views;

namespace Feelknit.iOS.Controllers
{
    public class BaseController : UIViewController
    {
		public object Data{ get; set; }

		protected bool NavigationButtonVisible{ get; set; }

		protected LocationManager Manager{ get; set; }

		protected LoadingOverlay LoadingOverlay{ get; set; }

        public BaseController(IntPtr handle)
            : base(handle)
        {
			NavigationButtonVisible = true;
        }

        protected UIStoryboard MainStoryboard
        {
            get { return UIStoryboard.FromName("MainStoryboard", null); }
        }

        // provide access to the sidebar controller to all inheriting controllers
        protected SidebarController SidebarController
        {
            get
            {
                return ((AppDelegate)UIApplication.SharedApplication.Delegate).RootViewController.SidebarController;
            }
        }

        // provide access to the sidebar controller to all inheriting controllers
        protected NavController NavController
        {
            get
            {
                return ((AppDelegate)UIApplication.SharedApplication.Delegate).RootViewController.NavController;
            }
        }

        public BaseController(string nibName, NSBundle bundle)
            : base(nibName, bundle)
        {
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			if (!NavigationButtonVisible)
				NavigationItem.SetLeftBarButtonItem (null, false);
			else
			NavigationItem.SetLeftBarButtonItem(
                new UIBarButtonItem(UIImage.FromBundle("threelines")
                    , UIBarButtonItemStyle.Plain
                    , (sender, args) => SidebarController.ToggleMenu()), true);
			Manager = new LocationManager();

        }

		protected void MoveToNextController(string controllerName, object data = null)
        {
			var controller = (BaseController)MainStoryboard.InstantiateViewController(controllerName);
			controller.Data = data;
            if (controller != null)
            {
                NavController.PushViewController(controller, true);
            }
        }
    }
}
