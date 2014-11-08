using System;
using System.Collections.Generic;
using Feelknit.iOS.Helpers;
using Feelknit.iOS.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Newtonsoft.Json;

namespace Feelknit.iOS.Controllers
{
    partial class LoadingViewController : UIViewController
    {
        private LoadingOverlay _loadingOverlay;

        public LoadingViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.NavigationController.NavigationBarHidden = true;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _loadingOverlay = new LoadingOverlay(UIScreen.MainScreen.Bounds, "Loading..");
            View.Add(_loadingOverlay);

            GetFeelingsList();
        }


        private async void GetFeelingsList()
        {
            var client = new JsonHttpClient(UrlHelper.GET_FEELS);
            var result = await client.GetRequest();
            _loadingOverlay.Hide();
            ApplicationHelper.Feelings = JsonConvert.DeserializeObject<List<string>>(result);
            MoveToNextController();
        }

        private void MoveToNextController()
        {
            var isAuthenticated = NSUserDefaults.StandardUserDefaults.BoolForKey("IsAuthenticated");
            if (isAuthenticated)
            {
                ApplicationHelper.UserName = NSUserDefaults.StandardUserDefaults.StringForKey("UserName");
                NavigateToAddFeeling();
                return;
            }
        }

        private void NavigateToAddFeeling()
        {
            var userFeelingsController =
                Storyboard.InstantiateViewController("UserFeelingsController") as UserFeelingsController;
            if (userFeelingsController != null)
            {
                NavigationController.PushViewController(userFeelingsController, true);
            }
        }
    }
}
