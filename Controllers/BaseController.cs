using System;
using System.Collections.Generic;
using System.Text;
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

        // provide access to the sidebar controller to all inheriting controllers
        protected SidebarController SidebarController
        {
            get
            {
                return (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.SidebarController;
            }
        }

        // provide access to the sidebar controller to all inheriting controllers
        protected NavController NavController
        {
            get
            {
                return (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.NavController;
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
                    , (sender, args) =>
                    {
                        SidebarController.ToggleMenu();
                    }), true);
        }
    }
}
