using System.Collections.Generic;
using Feelknit.iOS.Model;
using MonoTouch.UIKit;
using System;
using Feelknit.iOS.Helpers;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using DSoft.Messaging;
using Feelknit.iOS.Views;

namespace Feelknit.iOS.Controllers
{
	partial class RelatedFeelingsViewController : BaseController
	{
		public Feeling Feeling{ get; set; }

		public IList<Feeling> RelatedFeelings{ get; set; }
		private MessageBusEventHandler gotoCommentEventHandler;
		private LoadingOverlay _loadingOverlay;

		public RelatedFeelingsViewController (IntPtr handle) : base (handle)
		{
			Title = "Related Feelings";
			RelatedFeelings = new List<Feeling> ();
			gotoCommentEventHandler = new MessageBusEventHandler () {
				EventId = Constants.GoToCommentsEvent,
				EventAction = GoToCommentEventHandler,
			};

			MessageBus.Default.Register (gotoCommentEventHandler);
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
				var bounds = UIScreen.MainScreen.Bounds; // portrait bounds

				_loadingOverlay = new LoadingOverlay(bounds, "Getting Related Feelings");
				this.View.Add(_loadingOverlay);
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
			if (feelings == null || !feelings.Any ())
				return;
			Feeling = feelings.First();

			feelings.Remove (Feeling);
			RelatedFeelings = feelings;

			InvokeOnMainThread (() => {
				_loadingOverlay.Hide();
				PopulateView();
			
			});
		}

		private void PopulateView()
		{
			UserImageView.Image = UIImage.FromBundle (!string.IsNullOrEmpty(ApplicationHelper.Avatar)? "Avatars/" + ApplicationHelper.Avatar + ".png": "userIcon.png");
			FeelingTextLabel.Text = Feeling.GetFeelingFormattedText ("I");
			FeelingNumberLabel.Text = string.Format ("  {0} people feeling {1} currently", RelatedFeelings.Count, Feeling.FeelingText);
			RelatedFeelingsTable.Source = new RelatedFeelingsTableViewSource (RelatedFeelings, OnRowSelection);
			RelatedFeelingsTable.ReloadData ();
		}

		/// <summary>
		/// Messages the bus event handler.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="evnt">Evnt.</param>
		private void GoToCommentEventHandler (object sender, MessageBusEvent evnt)
		{
			var feeling = evnt.Data [0] as Feeling;
			//execute on the UI thread
			BeginInvokeOnMainThread (() => {
				var commentsViewController =
					this.Storyboard.InstantiateViewController ("CommentsViewController") as CommentsViewController;
				if (commentsViewController != null) {
					commentsViewController.Feeling = feeling;
					commentsViewController.Data = true;
					this.NavigationController.PushViewController (commentsViewController, true);
				}
			});

		}
	}
}
