using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Feelknit.iOS.Model;
using System.Collections.Generic;

namespace Feelknit.iOS
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
