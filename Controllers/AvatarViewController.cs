using Foundation;
using UIKit;
using System;
using System.CodeDom.Compiler;
using Feelknit.iOS.Helpers;
using Feelknit.iOS.Controllers;
using DSoft.Messaging;
using System.Threading.Tasks;
using Feelknit.Model;

namespace Feelknit.iOS
{
	partial class AvatarViewController : BaseController
	{
		static NSString avatarCellId = new NSString ("AvatarCell");
		private string _avatar = "";

		public AvatarViewController (IntPtr handle) : base (handle)
		{
			NavigationButtonVisible = false;

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			SkipButton.BackgroundColor = Resources.LightButtonColor;
			SkipButton.SetTitleColor (Resources.WhiteColor, UIControlState.Normal);

			SaveButton.BackgroundColor = Resources.LightButtonColor;
			SaveButton.SetTitleColor (Resources.WhiteColor, UIControlState.Normal);

			AvatarCollectionView.BackgroundColor = Resources.WhiteColor;
		}

		public override void ViewDidLoad ()
		{
			var tableSource = new AvatarCollectioniewSource (Resources.Avatars, AvatarSelected);
			AvatarCollectionView.RegisterClassForCell (typeof(AvatarCell), avatarCellId);
			AvatarCollectionView.Source = tableSource;

			UserAvatarImageView.Image = UIImage.FromBundle ("Avatars/" + ApplicationHelper.Avatar + ".png"); 
			var isFromProfile = Data == null ? false : (bool)Data;

			SkipButton.TouchUpInside += (object sender, EventArgs e) => {
				if(isFromProfile)
					NavigationController.PopViewController (true);
				else
					MoveToNextController(typeof(AddFeelingViewController).Name);
			};
			SaveButton.TouchUpInside += (object sender, EventArgs e) => {


				if (isFromProfile) {
					MessageBus.PostEvent (new CoreMessageBusEvent (Constants.AvatarSelectedEvent) {
						Sender = this,
						Data = new object[]{ _avatar }
					});

					NavigationController.PopViewController (true);
				}
				else{
					ApplicationHelper.Avatar = _avatar;
					//Update Left Hand menu
					MessageBus.Default.Post (new CoreMessageBusEvent (Constants.UserDetailsUpdateEvent) {
						Sender = this,
					});

					 Task.Factory.StartNew (async () => {
						var client = new JsonHttpClient (UrlHelper.SAVE_AVATAR);
						await client.PostRequest (new User{ UserName = ApplicationHelper.UserName, Avatar = _avatar });
						InvokeOnMainThread(()=> MoveToNextController(typeof(AddFeelingViewController).Name));
					});
				}
			};
			
		}


		private void AvatarSelected (string avatar)
		{
			_avatar = avatar;
			UserAvatarImageView.Image = UIImage.FromBundle ("Avatars/" + avatar + ".png"); 
		}

	}
}
