using System;
using Feelknit.iOS.Model;
using MonoTouch.UIKit;

namespace Feelknit.iOS.Controllers
{
	partial class CommentsViewController : BaseController
	{
		public Feeling Feeling{ get; set; }

		public CommentsViewController () : base (null,null)
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
