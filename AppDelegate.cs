using Feelknit.iOS.Controllers;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using BugSense;
using System;
using Feelknit.iOS.Helpers;

namespace Feelknit
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		private string _deviceToken;

		public override UIWindow Window {
			get;
			set;
		}

	    public NavController NavController { get; set; }


        public RootViewController RootViewController { get { return Window.RootViewController as RootViewController; } }

        // This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
		public override void OnResignActivation (UIApplication application)
		{
		}
		
		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
		}
		
		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
		}
		
		// This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate (UIApplication application)
		{
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			BugSenseHandler.Instance.InitAndStartSession ("9dd4c16c");
            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            // If you have defined a root view controller, set it here:
            Window.RootViewController = new RootViewController();

            // make the window visible
            Window.MakeKeyAndVisible();

			if (UIDevice.CurrentDevice.CheckSystemVersion(8,0))
			{
				var settings = UIUserNotificationSettings.GetSettingsForTypes (UIUserNotificationType.Sound |
					UIUserNotificationType.Alert | UIUserNotificationType.Badge, null);

				application.RegisterUserNotificationSettings (settings);
				application.RegisterForRemoteNotifications ();

			}
			else
			{
				application.RegisterForRemoteNotificationTypes(UIRemoteNotificationType.Badge |
					UIRemoteNotificationType.Sound | UIRemoteNotificationType.Alert);
			}
			return true;

		}
			
		public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
		{
			Console.WriteLine ("in did register");
			// without this RegisteredForRemoteNotifications doesn't fire on iOS 8!
			application.RegisterForRemoteNotifications();
		}

		public override void RegisteredForRemoteNotifications (
			UIApplication application, NSData deviceToken)
		{
			ApplicationHelper.DeviceToken = deviceToken != null ?
				deviceToken.ToString().Replace(" ","").Replace("<","").Replace(">",""):string.Empty;
			new UIAlertView("Token", _deviceToken, null, "OK", null).Show();
			// code to register with your server application goes here
		}

		public override void FailedToRegisterForRemoteNotifications (UIApplication application , NSError error)
		{
			//new UIAlertView("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show();
		}
	}
}

