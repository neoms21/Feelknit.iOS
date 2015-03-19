using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;
using Feelknit.iOS.Helpers;
using Feelknit.iOS.Controllers;
using DSoft.Messaging;

namespace Feelknit.iOS.Controllers
{
	partial class ProfileViewController : BaseController
	{
		public ProfileViewController (IntPtr handle) : base (handle)
		{
			;
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
			UserEmailTextView.Text = ApplicationHelper.EmailAddress;
			UserNameLabel.Text = ApplicationHelper.UserName;
			UserImageButton.SetBackgroundImage (UIImage.FromBundle (string.Format ("Avatars/{0}", ApplicationHelper.Avatar)), UIControlState.Normal);
		
			UserImageButton.TouchUpInside+= (object sender, EventArgs e) => {
				MoveToNextController(typeof(AvatarViewController).Name);
			};

			SaveButton.TouchUpInside += (object sender, EventArgs e) => {

		
			};
		

			CancelButton.TouchUpInside += (object sender, EventArgs e) => {

			};


		}

		/// <summary>
		/// Messages the bus event handler.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="evnt">Evnt.</param>
		public void MessageBusEventHandler (object sender, MessageBusEvent evnt)
		{
			var avatar = evnt.Data [0] as string;
			//execute on the UI thread
			BeginInvokeOnMainThread (() => {
				//post to the output box
			
				UserImageButton.SetBackgroundImage (UIImage.FromBundle (string.Format("Avatars/{0}.png", avatar)), UIControlState.Normal);
			});

		}

	}
}
