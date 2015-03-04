using System;
using Feelknit.iOS.Model;
using MonoTouch.UIKit;
using Feelknit.iOS.Helpers;
using System.Drawing;

namespace Feelknit.iOS.Controllers
{
	partial class CommentsViewController : BaseController
	{
		public Feeling Feeling{ get; set; }

		public CommentsViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewWillAppear(bool animated)
		{
			View.BackgroundColor = Resources.MainBackgroundColor;
			UserNameLabel.TextColor = Resources.ButtonColor;
			//CommentsCountLabel.BackgroundColor = Resources.LoginButtonColor;
		}
//		public CommentsViewController () : base (null,null)
//		{

	

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.NavigationController.NavigationBarHidden = false;
			UserIcon.Image =  ResizeImage( UIImage.FromBundle ("Avatars/" + ApplicationHelper.Avatar + ".png"),45,45);
			//UserIcon.Frame = new RectangleF (10,10,36, 30);
			UserNameLabel.Text = ApplicationHelper.UserName == Feeling.UserName ? "I" : Feeling.UserName;
			FeelingTextView.Text = Feeling.GetFeelingFormattedText ("");

			//CommentsCountLabel.Text = string.Format ("{0} comments on this feeling", Feeling.Comments.Count);

			//CommentsTable.Source = new CommentsViewController
	

		}

		// resize the image (without trying to maintain aspect ratio)
		public UIImage ResizeImage(UIImage sourceImage, float width, float height)
		{
			UIGraphics.BeginImageContextWithOptions (new SizeF (width, height), false, 2.0f);
			sourceImage.Draw(new RectangleF(0, 0, width, height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return resultImage;
		}
}
}
