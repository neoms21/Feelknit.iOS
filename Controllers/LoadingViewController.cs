using System;
using System.Collections.Generic;
using Feelknit.iOS.Helpers;
using Feelknit.iOS.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Newtonsoft.Json;

namespace Feelknit.iOS.Controllers
{
    partial class LoadingViewController : BaseController
    {
        private LoadingOverlay _loadingOverlay;

        public LoadingViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBarHidden = true;
            View.BackgroundColor = Resources.MainBackgroundColor;
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
                MoveToNextState();
            }
            catch (Exception ex)
            {
                var alert = new UIAlertView("Authentication", ex.ToString(), null, "OK", null);
                alert.Show();
            }
        }

        private void MoveToNextState()
        {
            var isAuthenticated = NSUserDefaults.StandardUserDefaults.BoolForKey("IsAuthenticated");

            if (isAuthenticated)
            {
                ApplicationHelper.UserName = NSUserDefaults.StandardUserDefaults.StringForKey("UserName");
                MoveToNextController(typeof(UserFeelingsController).Name);
                return;
            }
            MoveToNextController(typeof(LoginViewController).Name);
        }

        private void NavigateToAddFeeling()
        {
            //var userFeelingsController =
            //    Storyboard.InstantiateViewController("UserFeelingsController") as UserFeelingsController;
            //if (userFeelingsController != null)
            //{
            //    NavigationController.PushViewController(userFeelingsController, true);
            //}
        }
    }
}
