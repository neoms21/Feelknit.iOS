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
            this.View.BackgroundColor = Resources.MainBackgroundColor;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BackgroundColor = Resources.MainBackgroundColor;
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
            try
            {
                MoveToNextController();
            }
            catch (Exception ex)
            {
                var alert = new UIAlertView("Authentication", ex.ToString(), null, "OK", null);
                alert.Show();
            }
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
            else
            {

                var loginViewController = Storyboard.InstantiateViewController("LoginViewController") as LoginViewController;

                if (loginViewController != null)
                {
                    NavigationController.PushViewController(loginViewController, true);
                }
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
