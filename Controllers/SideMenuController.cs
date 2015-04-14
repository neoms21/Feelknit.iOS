using Feelknit.iOS.TableViewSources;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using Feelknit.iOS.Helpers;
using DSoft.Messaging;
using System.Collections.Specialized;

namespace Feelknit.iOS.Controllers
{
    public class SideMenuController : BaseController
    {
		public Action<bool> Action { get; set; }
		private MessageBusEventHandler mEvHandler;

        public SideMenuController()
            : base(null, null)
        {
			mEvHandler = new MessageBusEventHandler () {
				EventId = Constants.SignoutEvent,
				EventAction = SignoutEventHandler,
			};
			MessageBus.Default.Register (mEvHandler);
        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			View = LeftDrawerView.Create(DrawerItemSelected,SignOutHandler);
            View.BackgroundColor = UIColor.FromRGB(.9f, .9f, .9f);
        }

		public void DrawerItemSelected (Container container)
		{
			if (container == null) {
				Action.Invoke (true);
				return;
			}

			MoveToNextController (container.Name,container.Data);
				Action.Invoke (true);
		}

		public void SignOutHandler ()
		{
			ApplicationHelper.UserName = string.Empty;
			ApplicationHelper.EmailAddress = string.Empty;
			ApplicationHelper.Avatar = string.Empty;
			ApplicationHelper.AuthorizationToken = string.Empty;
			ApplicationHelper.IsAuthenticated = false;


			InvokeOnMainThread (() => {
				MoveToNextController (typeof(LoginViewController).Name);
			});
		}

		private void SignoutEventHandler(object sender, MessageBusEvent evnt)
		{
			ApplicationHelper.UserName = string.Empty;
			ApplicationHelper.EmailAddress = string.Empty;
			ApplicationHelper.Avatar = string.Empty;
			ApplicationHelper.AuthorizationToken = string.Empty;
			ApplicationHelper.IsAuthenticated = false;

			JsonHttpClient client = new JsonHttpClient(UrlHelper.CLEAR_USER_KEY);
			var collection = new NameValueCollection ();
			collection.Add ("username", ApplicationHelper.UserName);
			client.PostRequestWithParams(collection);
			InvokeOnMainThread (() => {
				MoveToNextController (typeof(LoginViewController).Name);
			});

		}
    }
}