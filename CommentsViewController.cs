using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Feelknit.iOS.Model;

namespace Feelknit.iOS
{
	partial class CommentsViewController : UIViewController
	{
		public Feeling Feeling{ get; set; }

		public CommentsViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.NavigationController.NavigationBarHidden = false;
			UserNameLabel.Text = Feeling.UserName;
			FeelingTextView.Text = Feeling.GetFeelingFormattedText ("");

			CommentsCountLabel.Text = string.Format ("{0} comments on this feeling", Feeling.Comments.Count);
		}

	}
}
