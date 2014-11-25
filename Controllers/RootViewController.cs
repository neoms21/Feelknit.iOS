using System;
using MonoTouch.UIKit;

namespace Feelknit.iOS.Controllers
{
    public class RootViewController : UIViewController
    {
        // the sidebar controller for the app
        public SidebarController SidebarController { get; private set; }

        // the navigation controller
        public NavController NavController { get; private set; }

        public RootViewController()
            : base(null, null)
        {

        }
        public RootViewController(IntPtr handle)
            : base(handle)
        {
        }

        //public RootViewController()
        //    : base(null, null)
        //{

        //}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavController = new NavController();
            //Set Navigation controller reference so we can use it everywhere
            AppDelegate appD = UIApplication.SharedApplication.Delegate as AppDelegate;
            appD.NavController = new NavController();

            var storyboard = UIStoryboard.FromName("MainStoryboard", null);
            var loadingViewController = storyboard.InstantiateViewController("LoadingViewController") as LoadingViewController;

            if (loadingViewController != null)
            {
                NavController.PushViewController(loadingViewController, true);
            }

            //VCConnection vConnect = (VCConnection)Storyboard.InstantiateViewController("VCConnection");
            //VCLeftMenu vLeftMenu = (VCLeftMenu)Storyboard.InstantiateViewController("VCLeftMenu");
            //appD.GETSET_NavController.PushViewController(vConnect, false);
            //appD.GETSET_SidebarController = new SidebarController(this, appD.GETSET_NavController, vLeftMenu);
            //appD.GETSET_SidebarController.MenuWidth = 250;
            //appD.GETSET_SidebarController.ReopenOnRotate = false;
            //appD.GETSET_SidebarController.IsOpening += appD.GETSET_Menu.UpdatePicturesToSynch;

            // create a slideout navigation controller with the top navigation controller and the menu view controller
            
            // NavController.PushViewController(new LoadingViewController(), false);
            SidebarController = new SidebarController(this, NavController, new SideMenuController())
            {
                MenuLocation = SidebarController.MenuLocations.Left,
                MenuWidth = 220,
                ReopenOnRotate = false
            };
        }
    }
}