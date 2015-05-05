using Foundation;
using UIKit;
using System;
using System.CodeDom.Compiler;
using Feelknit.iOS.Helpers;
using Feelknit.iOS.Controllers;
using DSoft.Messaging;
using Newtonsoft.Json;
using Feelknit.Model;
using System.Threading.Tasks;

namespace Feelknit.iOS.Controllers
{
	partial class ProfileViewController : BaseController
	{
		private string _avatar = string.Empty;
		public ProfileViewController (IntPtr handle) : base (handle)
		{
			NavigationButtonVisible = false;
			//register for an event
			MessageBus.Default.Register (new MessageBusEventHandler () {
				EventId = Constants.AvatarSelectedEvent,
				EventAction = MessageBusEventHandler,
			});
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			CancelButton.BackgroundColor = Resources.LightButtonColor;
			SaveButton.BackgroundColor = Resources.LightButtonColor;

			UserEmailTextView.Layer.BorderColor = UIColor.Black.CGColor;
			UserEmailTextView.Layer.BorderWidth = 1.5f;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			_avatar = ApplicationHelper.Avatar;
			UserEmailTextView.Text = ApplicationHelper.EmailAddress;
			UserNameLabel.Text = ApplicationHelper.UserName;
			UserImageButton.SetBackgroundImage (UIImage.FromBundle (string.Format ("Avatars/{0}", ApplicationHelper.Avatar)), UIControlState.Normal);
		
			UserImageButton.TouchUpInside+= (object sender, EventArgs e) => {
				MoveToNextController(typeof(AvatarViewController).Name, true);
			};


			SaveButton.TouchUpInside += (object sender, EventArgs e) => {
				string email = UserEmailTextView.Text;
				Task.Factory.StartNew(()=>SaveUser(email));
			};
		

			CancelButton.TouchUpInside += (object sender, EventArgs e) => {
				NavigationController.PopViewController(true);
			};


		}

		/// <summary>
		/// Messages the bus event handler.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="evnt">Evnt.</param>
		public void MessageBusEventHandler (object sender, MessageBusEvent evnt)
		{
			_avatar = evnt.Data [0] as string;
			//execute on the UI thread
			BeginInvokeOnMainThread (() => {
				//post to the output box
			
				UserImageButton.SetBackgroundImage (UIImage.FromBundle (string.Format("Avatars/{0}.png", _avatar)), UIControlState.Normal);
			});

		}

		private async void SaveUser(string email)
		{
			//var emailAddress = "a@b.com";
			var client = new JsonHttpClient(string.Format(UrlHelper.SAVE_USER));
			var user = new User{ EmailAddress = email , Avatar = _avatar, UserName = ApplicationHelper.UserName };
			await client.PostRequest(user);
		
				ApplicationHelper.Avatar = _avatar;
			ApplicationHelper.EmailAddress = email;
			MessageBus.PostEvent (new CoreMessageBusEvent (Constants.UserDetailsUpdateEvent));
			InvokeOnMainThread(()=> NavigationController.PopViewController(true));
		}

	}
}
