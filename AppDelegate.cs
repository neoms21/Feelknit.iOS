using Feelknit.iOS.Controllers;
using Foundation;
using UIKit;
//using BugSense;
using System;
using Feelknit.iOS.Helpers;
using System.Collections;
using System.Collections.Generic;
using Feelknit.iOS.Model;

namespace Feelknit
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
		private NSDictionary _userInfo;
        public override UIWindow Window
        {
            get;
            set;
        }

        public NavController NavController { get; set; }


        public RootViewController RootViewController { get { return Window.RootViewController as RootViewController; } }

        // This method is invoked when the application is about to move from active to inactive state.
        // OpenGL applications should use this method to pause.
        public override void OnResignActivation(UIApplication application)
        {
        }

        // This method should be used to release shared resources and it should store the application state.
        // If your application supports background exection this method is called instead of WillTerminate
        // when the user quits.
        public override void DidEnterBackground(UIApplication application)
        {
        }

        // This method is called as part of the transiton from background to active state.
        public override void WillEnterForeground(UIApplication application)
        {
        }

        // This method is called when the application is about to terminate. Save data, if needed.
        public override void WillTerminate(UIApplication application)
        {
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
//			if (launchOptions != null) {
//				//new UIAlertView ("In Finished Launching", launchOptions.ToString (), null, "OK", null).Show ();
//				//Console.WriteLine (launchOptions.ToString ());
//				//ActionNotificationReceived (application,launchOptions);
//				_userInfo = launchOptions;
//				NavigateToController ();
//				return true;
//
//			}

			// NOTE: Don't call the base implementation on a Model class
            // see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
           // BugSenseHandler.Instance.InitAndStartSession("9dd4c16c");
            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            // If you have defined a root view controller, set it here:
			Window.RootViewController = new RootViewController(launchOptions);

            // make the window visible
            Window.MakeKeyAndVisible();

            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Sound |
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge, null);

                application.RegisterUserNotificationSettings(settings);
                application.RegisterForRemoteNotifications();

            }
            else
            {
                application.RegisterForRemoteNotificationTypes(UIRemoteNotificationType.Badge |
                    UIRemoteNotificationType.Sound | UIRemoteNotificationType.Alert);
            }
            return true;

        }
		public override void DidReceiveRemoteNotification (UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
		{
			ActionNotificationReceived (application, userInfo);
		}

		public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)
		{
			ActionNotificationReceived (application, userInfo);
		}

		void ActionNotificationReceived (UIApplication application, NSDictionary userInfo)
		{
				//Get the aps dictionary

			var aps = userInfo ["aps"] as NSDictionary;;
			string alert = string.Empty;

			_userInfo = userInfo;

			//Extract the alert text
			// NOTE: If you're using the simple alert by just specifying
			// "  aps:{alert:"alert msg here"}  " this will work fine.
			// But if you're using a complex alert with Localization keys, etc.,
			// your "alert" object from the aps dictionary will be another NSDictionary.
			// Basically the json gets dumped right into a NSDictionary,
			// so keep that in mind.
			if (aps.ContainsKey(new NSString("alert")))
				alert = (aps [new NSString("alert")] as NSString).ToString();//message = message;
			
			if (application.ApplicationState == UIApplicationState.Active) {
				alert = _userInfo ["feelingId"] != null ? string.Format ("{0}. Go to Comments?", alert) 
					: string.Format ("{0}. Go to Related Feelings?", alert);
				
				var alertView = new UIAlertView ("Feelknit", alert, null, "Yes", new string[]{"No"});
				alertView.Clicked += AlertDelegate;
				alertView.Show();
				return;
			}

			NavigateToController ();
		}

		private void NavigateToController()
		{
			RootViewController navigationController = (RootViewController)this.Window.RootViewController;
			var storyboard = UIStoryboard.FromName ("MainStoryboard", null);

			if (_userInfo ["feelingId"] != null) {
				CommentsViewController commentsViewController = storyboard.InstantiateViewController ("CommentsViewController") as CommentsViewController;
				commentsViewController.Feeling = new Feeling {
					Id = _userInfo ["feelingId"].ToString ()
				};
				commentsViewController.Data = true;
				InvokeOnMainThread (() =>  {
					navigationController.NavController.PushViewController (commentsViewController, true);
				});
				return;
			}
			RelatedFeelingsViewController relatedFeelingsViewController = storyboard.InstantiateViewController ("RelatedFeelingsViewController") as RelatedFeelingsViewController;
			InvokeOnMainThread (() =>  {
				navigationController.NavController.PushViewController (relatedFeelingsViewController, true);
			});
		}

		private void AlertDelegate(object a, UIButtonEventArgs b)
		{
			if (b.ButtonIndex == 0)
				NavigateToController ();
		}

        public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
        {
            // without this RegisteredForRemoteNotifications doesn't fire on iOS 8!
            application.RegisterForRemoteNotifications();
        }

        public override void RegisteredForRemoteNotifications(
            UIApplication application, NSData deviceToken)
		{
			ApplicationHelper.ApnsToken = deviceToken != null ?
                deviceToken.ToString ().Replace (" ", "").Replace ("<", "").Replace (">", "") : string.Empty;
			Console.WriteLine (ApplicationHelper.ApnsToken);
			// code to register with your server application goes here
		}

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            //new UIAlertView("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show();
        }
    }
}

