using System;
using System.Collections.Generic;
using Feelknit.iOS.Model;
using MonoTouch.UIKit;

namespace Feelknit.iOS.Controllers
{
	partial class RelatedFeelingsViewController : UIViewController
	{
		public Feeling Feeling{ get; set; }

		public IList<Feeling> RelatedFeelings{ get; set; }

		public RelatedFeelingsViewController (IntPtr handle) : base (handle)
		{
			RelatedFeelings = new List<Feeling> ();
}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			FeelingTextLabel.SizeToFit ();
			FeelingTextLabel.PreferredMaxLayoutWidth = 200;
			FeelingTextLabel.LineBreakMode = UILineBreakMode.WordWrap;
			FeelingTextLabel.Text = Feeling.GetFeelingFormattedText ("I");
			FeelingNumberLabel.Text = string.Format("{0} people feeling {1} currently",RelatedFeelings.Count,Feeling.FeelingText);

			RelatedFeelingsTable.Source = new RelatedFeelingsTableViewSource (RelatedFeelings, OnRowSelection);
		}

		private void OnRowSelection(Feeling feeling)
		{
			var commentsViewController =
				this.Storyboard.InstantiateViewController("CommentsViewController") as CommentsViewController;
			if (commentsViewController != null)
			{
				commentsViewController.Feeling = feeling;
				this.NavigationController.PushViewController(commentsViewController, true);
			}
		}
	}
}
