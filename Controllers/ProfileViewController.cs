using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;
using Feelknit.iOS.Helpers;
using Feelknit.iOS.Controllers;

namespace Feelknit.iOS.Controllers
{
	partial class ProfileViewController : BaseController
	{
		public ProfileViewController (IntPtr handle) : base (handle)
		{
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
		
			SaveButton.TouchUpInside += (object sender, EventArgs e) => {

		
			};
		

			CancelButton.TouchUpInside += (object sender, EventArgs e) => {

			};


		}


	}
}
