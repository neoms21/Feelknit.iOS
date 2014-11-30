using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Feelknit.iOS.Controllers
{
    public class BaseController : UIViewController
    {

        public BaseController(IntPtr handle)
            : base(handle)
        {
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

            NavigationItem.SetLeftBarButtonItem(
                new UIBarButtonItem(UIImage.FromBundle("threelines")
                    , UIBarButtonItemStyle.Plain
                    , (sender, args) => SidebarController.ToggleMenu()), true);
        }

        protected void MoveToNextController(string controllerName)
        {
            var controller = (UIViewController)MainStoryboard.InstantiateViewController(controllerName);

            if (controller != null)
            {
                NavController.PushViewController(controller, true);
            }
        }
    }
}
