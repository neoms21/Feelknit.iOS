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
			CommentsCountLabel.BackgroundColor = Resources.LoginButtonColor;
			//CommentsCountLabel.BackgroundColor = Resources.LoginButtonColor;
		}
//		public CommentsViewController () : base (null,null)
//		{

	

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.NavigationController.NavigationBarHidden = false;
			UserIcon.Image =  ResizeImage( UIImage.FromBundle ("Avatars/" + ApplicationHelper.Avatar + ".png"),100,100);
			//UserIcon.Frame = new RectangleF (10,10,36, 30);
			UserNameLabel.Text = ApplicationHelper.UserName == Feeling.UserName ? "I" : Feeling.UserName;
			FeelingTextView.Text = Feeling.GetFeelingFormattedText ("");

			CommentsCountLabel.Text = string.Format ("  {0} comments", Feeling.Comments.Count);
			SubmitCommentButton.Image = ResizeImage (UIImage.FromBundle ("012.png"), 40, 40);
			CommentsTable.Source = new CommentsTableViewSource (Feeling.Comments);

			//FeelingTextView.SizeToFit();
			//CommentsCountLabel.Text = string.Format ("{0} comments on this feeling", Feeling.Comments.Count);

			//CommentsTable.Source = new CommentsViewController
	

		}



//		public override void ViewWillLayoutSubviews ()
//		{
//			base.ViewWillLayoutSubviews ();
//			var width = FeelingTextView.Frame.Size.Width;
//			var size = FeelingTextView.SizeThatFits(FeelingTextView.Frame.Size);
//			var newFrame = FeelingTextView.Frame;
//			newFrame.Size = size;
//			FeelingTextView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
//			FeelingTextView.Frame = new RectangleF(0,0,200,30);
//			//FeelingTextView.ScrollEnabled = false;
//
////			CGSize newSize = [textView sizeThatFits:CGSizeMake(fixedWidth, MAXFLOAT)];
////			CGRect newFrame = textView.frame;
////			newFrame.size = CGSizeMake(fmaxf(newSize.width, fixedWidth), newSize.height);
////			textView.frame = newFrame;
//
//		}

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
