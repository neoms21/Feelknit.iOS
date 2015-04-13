using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Feelknit.iOS.Model;

namespace Feelknit.iOS.Controllers
{
    public class RootViewController : UIViewController
    {
        // the sidebar controller for the app
        public SidebarController SidebarController { get; private set; }

        // the navigation controller
        public NavController NavController { get; private set; }
		private NSDictionary _options;

		public bool Other{ get; set;}

		public RootViewController(NSDictionary options)
            : base(null, null)
        {
			_options = options;
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
            var appD = UIApplication.SharedApplication.Delegate as AppDelegate;
            appD.NavController = new NavController();

            var storyboard = UIStoryboard.FromName("MainStoryboard", null);

            var loadingViewController = storyboard.InstantiateViewController(typeof(LoadingViewController).Name) as LoadingViewController;

			if (_options == null) {
				NavController.PushViewController (loadingViewController, true);
			} 
			else {
			
				if (_options ["feelingId"] != null) {
					CommentsViewController commentsViewController = storyboard.InstantiateViewController ("CommentsViewController") as CommentsViewController;
					commentsViewController.Feeling = new Feeling {
						Id = _options ["feelingId"].ToString ()
					};
					commentsViewController.Data = true;

					NavController.PushViewController (commentsViewController, true);

					return;
				} else {
					RelatedFeelingsViewController relatedFeelingsViewController = storyboard.InstantiateViewController ("RelatedFeelingsViewController") as RelatedFeelingsViewController;
					NavController.PushViewController (relatedFeelingsViewController, true);
				}
			}

			SidebarController = new SidebarController(this, NavController,new SideMenuController())
            {
				MenuLocation = SidebarController.MenuLocations.Left,
                MenuWidth = 220,
                ReopenOnRotate = false
            };
        }
    }
}