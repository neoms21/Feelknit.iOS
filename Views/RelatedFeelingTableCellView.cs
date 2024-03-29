
using System;
using CoreGraphics;

using Foundation;
using UIKit;
using Feelknit.iOS.Model;
using System.Threading.Tasks;
using Feelknit.iOS.Helpers;
using System.Collections.Specialized;
using DSoft.Messaging;

namespace Feelknit.iOS
{
	public partial class RelatedFeelingTableCellView : UITableViewCell
	{
		public Feeling Feeling{ get; set; }

		public static readonly UINib Nib = UINib.FromName ("RelatedFeelingTableCellView", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("RelatedFeelingTableCellView");

		public RelatedFeelingTableCellView (IntPtr handle) : base (handle)
		{
			EventHelper.RegisterEvent (Constants.DeRegisterEvents, RemoveEventHandlers);
		}

		public override void RemoveFromSuperview ()
		{
			base.RemoveFromSuperview ();
		}

		public static RelatedFeelingTableCellView Create ()
		{
			return (RelatedFeelingTableCellView)Nib.Instantiate (null, null) [0];

		}

		private void RemoveEventHandlers (object sender, MessageBusEvent evnt)
		{

		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			SupportButton.TouchUpInside += ProcessSupportCount;
			ReportButton.TouchUpInside += ExecuteReportButtonClick;
			CommentButton.TouchUpInside += ExecuteCommentButtonClick;
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			SupportButton.TouchUpInside -= ProcessSupportCount;
			ReportButton.TouchUpInside -= ExecuteReportButtonClick;
			CommentButton.TouchUpInside -= ExecuteCommentButtonClick;
		}
	
		private void ExecuteCommentButtonClick (object sender, EventArgs e)
		{
			MessageBus.PostEvent (new CoreMessageBusEvent (Constants.GoToCommentsEvent) {
				Sender = this,
				Data = new object[]{ Feeling }
			});
		}

		private void ExecuteReportButtonClick (object sender, EventArgs e)
		{
			Feeling.IsReported = true;
			FormatButtons ();
			Task.Factory.StartNew (() => {
				ReportFeeling ();
			});
		}

		private void ProcessSupportCount (object sender, EventArgs e)
		{
			var isIncrease = true;
			if (Feeling.SupportUsers.Contains (ApplicationHelper.UserName))
				isIncrease = false;

			if (isIncrease) {
				SupportButton.SetTitle ("Un-Support", UIControlState.Normal);
				Feeling.SupportCount += 1;
				Feeling.SupportUsers.Add (ApplicationHelper.UserName);
			} else {
				SupportButton.SetTitle ("Support", UIControlState.Normal);
				Feeling.SupportCount -= 1;
				Feeling.SupportUsers.Remove (ApplicationHelper.UserName);
			}

			SupportLabel.Text = string.Format ("Support {0}", Feeling.SupportCount);
			Task.Factory.StartNew (() => {
				SaveSupportCountModification (isIncrease);
			});
		}

		private void DecreaseSupportCount (object sender, EventArgs e)
		{
			Feeling.SupportCount -= 1;
			SupportLabel.Text = string.Format ("Support {0}", Feeling.SupportCount);
			Task.Factory.StartNew (() => {
				SaveSupportCountModification (false);
			});
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			if (Feeling.SupportUsers.Contains (ApplicationHelper.UserName)) {
				SupportButton.SetTitle ("Un-Support", UIControlState.Normal);

			} else {
				SupportButton.SetTitle ("Support", UIControlState.Normal);
			}

			var firstAttributes = new UIStringAttributes {
				ForegroundColor = Resources.LightButtonColor,
				Font = UIFont.BoldSystemFontOfSize (12)
			};

			var boldAttributes = new UIStringAttributes {

				Font = UIFont.BoldSystemFontOfSize (12)
			};
			Feeling.UserName = ApplicationHelper.UserName == Feeling.UserName ? "I" : Feeling.UserName;
			var fulltext = string.Format ("{0} {1}", Feeling.UserName, Feeling.GetFeelingFormattedText (""));
			var startIndexOfFeeling = Feeling.UserName.Length + 14; // fulltext is in format of 'username was feeling' FeellingText
			var prettyString = new NSMutableAttributedString (fulltext);
			prettyString.SetAttributes (firstAttributes.Dictionary, new NSRange (0, Feeling.UserName.Length));
			prettyString.SetAttributes (boldAttributes.Dictionary, new NSRange (startIndexOfFeeling, Feeling.FeelingText.Length));

			userImageView.Image = UIImage.FromBundle (string.IsNullOrWhiteSpace (Feeling.UserAvatar) ? "userIcon.png" :
				string.Format ("Avatars/{0}.png", Feeling.UserAvatar));
			FeelingTextLabel.AttributedText = prettyString;

			ResizeHeigthWithText (FeelingTextLabel);
			CommentsLabel.Text = string.Format ("Comments {0}", Feeling.Comments.Count == 0 ? Feeling.CommentsCount : Feeling.Comments.Count);
			SupportLabel.Text = string.Format ("Support {0}", Feeling.SupportCount);
			FeelingDate.Text = Feeling.FeelingDate.ToLocalTime().ToString ("dd MMM yyyy HH:mm");

			FormatButtons ();
		}

		void FormatButtons ()
		{
			FormatButton (SupportButton);
			FormatButton (CommentButton);
			FormatButton (ReportButton);

			if(Feeling.UserName.Equals("I"))
				{
				ReportButton.Enabled = false;
				ReportButton.BackgroundColor = Resources.DisabledColor;
				}
		}

		private void FormatButton (UIButton button)
		{

			if (Feeling.IsReported) {
				ReportedLabel.Opaque = true;
				ReportedLabel.Hidden = false;
				button.Enabled = false;
				button.BackgroundColor = Resources.DisabledColor;
				CommentsLabel.Hidden = true;
			} else {
				button.Enabled = true;
				ReportedLabel.Hidden = true;
				CommentsLabel.Hidden = false;
				button.BackgroundColor = Resources.LightButtonColor;
			}

			button.SetTitleColor (Resources.WhiteColor, UIControlState.Normal);
		}

		private void ResizeHeigthWithText (UILabel label, float maxHeight = 960f)
		{

			label.AdjustsFontSizeToFitWidth = false;
			float width = 280;// label.Frame.Width;  
			label.Lines = 0;
			CGSize size = ((NSString)label.Text).StringSize (label.Font,  
				             constrainedToSize: new CGSize (width, maxHeight), lineBreakMode: UILineBreakMode.WordWrap);

			var labelFrame = label.Frame;
			labelFrame.Size = new CGSize (280, size.Height);
			label.Frame = labelFrame; 
		}

		private void SaveSupportCountModification (bool isIncrease = true)
		{
			var url = isIncrease ? UrlHelper.INCREASE_SUPPORT : UrlHelper.DECREASE_SUPPORT;
			var client = new JsonHttpClient (url);
			var collection = new NameValueCollection ();
			collection.Add ("feelingId", Feeling.Id);
			collection.Add ("username", ApplicationHelper.UserName);

			client.PostRequestWithParams (collection);
		}


		private void ReportFeeling ()
		{
			var client = new JsonHttpClient (UrlHelper.REPORT_FEELING);
			var collection = new NameValueCollection ();
			collection.Add ("feelingId", Feeling.Id);
			collection.Add ("username", ApplicationHelper.UserName);
			client.PostRequestWithParams (collection);
		}
	}
}

