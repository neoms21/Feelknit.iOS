using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;
using Feelknit.iOS.Helpers;
using Feelknit.iOS.Controllers;
using DSoft.Messaging;

namespace Feelknit.iOS
{
	partial class AvatarViewController : BaseController
	{
		static NSString avatarCellId = new NSString ("AvatarCell");
		private string _avatar = "";
		public AvatarViewController (IntPtr handle) : base (handle)
		{
			 

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

			SkipButton.TouchUpInside += (object sender, EventArgs e) => {
				NavigationController.PopViewControllerAnimated(true);
			};

			SaveButton.TouchUpInside += (object sender, EventArgs e) => {
				MessageBus.PostEvent(new CoreMessageBusEvent(Constants.AvatarSelectedEvent){ Sender = this, Data = new object[]{_avatar}  });

				NavigationController.PopViewControllerAnimated(true);
			};
		}


		private void AvatarSelected(string avatar)
		{
			_avatar = avatar;
			UserAvatarImageView.Image = UIImage.FromBundle ("Avatars/" + avatar + ".png"); 
		}

	}
}
