using System.Collections.Generic;
using Feelknit.iOS.Model;
using MonoTouch.UIKit;
using System;
using Feelknit.iOS.Helpers;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Feelknit.iOS.Controllers
{
	partial class RelatedFeelingsViewController : BaseController
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
			FeelingNumberLabel.BackgroundColor = Resources.LoginButtonColor;
			FeelingTextLabel.SizeToFit ();
			FeelingTextLabel.PreferredMaxLayoutWidth = 200;
			FeelingTextLabel.LineBreakMode = UILineBreakMode.WordWrap;

			if (Feeling != null)
				PopulateView ();
			else {
				Task.Factory.StartNew (() => GetFeelings ());
			}
		}

		private void OnRowSelection (Feeling feeling)
		{
			var commentsViewController =
				this.Storyboard.InstantiateViewController ("CommentsViewController") as CommentsViewController;
			if (commentsViewController != null) {
				commentsViewController.Feeling = feeling;
				this.NavigationController.PushViewController (commentsViewController, true);
			}
		}

		private async void GetFeelings()
		{
			var client = new JsonHttpClient(string.Format(UrlHelper.RELATED_FEELINGS,ApplicationHelper.UserName));

			var responseText = await client.GetRequest ();
			var feelings = JsonConvert.DeserializeObject<IList<Feeling>> (responseText);
			if (!feelings.Any ())
				return;
			Feeling = feelings.First();

			feelings.Remove (Feeling);
			RelatedFeelings = feelings;

			InvokeOnMainThread (() => {
				PopulateView();
			
			});
		}

		private void PopulateView()
		{
			UserImageView.Image = UIImage.FromBundle ("Avatars/" + ApplicationHelper.Avatar + ".png");
			FeelingTextLabel.Text = Feeling.GetFeelingFormattedText ("I");
			FeelingNumberLabel.Text = string.Format ("  {0} people feeling {1} currently", RelatedFeelings.Count, Feeling.FeelingText);
			RelatedFeelingsTable.Source = new RelatedFeelingsTableViewSource (RelatedFeelings, OnRowSelection);
			RelatedFeelingsTable.ReloadData ();
		}
	}
}
