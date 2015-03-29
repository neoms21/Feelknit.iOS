
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Feelknit.iOS.Model;
using System.Threading.Tasks;
using Feelknit.iOS.Helpers;
using System.Collections.Specialized;

namespace Feelknit.iOS
{
	public partial class RelatedFeelingTableCellView : UITableViewCell
	{
		public Feeling Feeling{ get; set; }

		public static readonly UINib Nib = UINib.FromName ("RelatedFeelingTableCellView", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("RelatedFeelingTableCellView");

		public RelatedFeelingTableCellView (IntPtr handle) : base (handle)
		{


		}

		public static RelatedFeelingTableCellView Create ()
		{
			return (RelatedFeelingTableCellView)Nib.Instantiate (null, null) [0];

		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			SupportButton.TouchUpInside += ProcessSupportCount;
			ReportButton.TouchUpInside += ExecuteReportButtonClick;

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

			var firstAttributes = new UIStringAttributes {
				ForegroundColor = Resources.LightButtonColor,
				Font = UIFont.BoldSystemFontOfSize (12)
			};

			var boldAttributes = new UIStringAttributes {

				Font = UIFont.BoldSystemFontOfSize (12)
			};
			if (Feeling.SupportUsers.Contains (ApplicationHelper.UserName)) {
				SupportButton.SetTitle ("Un-Suppport", UIControlState.Normal);

			} else {
				SupportButton.SetTitle ("Suppport", UIControlState.Normal);
			}
			var fulltext = string.Format ("{0} {1}", Feeling.UserName, Feeling.GetFeelingFormattedText (""));
			var startIndexOfFeeling = Feeling.UserName.Length + 13; // fulltexy is in format of username was feeling FeellingText
			var prettyString = new NSMutableAttributedString (fulltext);
			prettyString.SetAttributes (firstAttributes.Dictionary, new NSRange (0, Feeling.UserName.Length));
			prettyString.SetAttributes (boldAttributes.Dictionary, new NSRange (startIndexOfFeeling, Feeling.FeelingText.Length));

			userImageView.Image = UIImage.FromBundle (string.IsNullOrWhiteSpace (Feeling.UserAvatar) ? "userIcon.png" :
				string.Format ("Avatars/{0}.png", Feeling.UserAvatar));
			FeelingTextLabel.AttributedText = prettyString;

			ResizeHeigthWithText (FeelingTextLabel);
			CommentsLabel.Text = string.Format ("Comments {0}", Feeling.Comments.Count == 0 ? Feeling.CommentsCount : Feeling.Comments.Count);
			SupportLabel.Text = string.Format ("Support {0}", Feeling.SupportCount);
			FeelingDate.Text = Feeling.FeelingDate.ToString ("dd MMM yyyy HH:mm");
			FormatButtons ();
		}

		void FormatButtons ()
		{
			FormatButton (SupportButton);
			FormatButton (CommentButton);
			FormatButton (ReportButton);
		}

		private void FormatButton (UIButton button)
		{
			if (Feeling.IsReported) {
				button.Enabled = false;
				button.BackgroundColor = Resources.DisabledColor;
			} else {
				button.Enabled = true;
				button.BackgroundColor = Resources.LightButtonColor;
			}

			button.SetTitleColor (Resources.WhiteColor, UIControlState.Normal);
		}

		private void ResizeHeigthWithText (UILabel label, float maxHeight = 960f)
		{

			label.AdjustsFontSizeToFitWidth = false;
			float width = 280;// label.Frame.Width;  
			label.Lines = 0;
			SizeF size = ((NSString)label.Text).StringSize (label.Font,  
				             constrainedToSize: new SizeF (width, maxHeight), lineBreakMode: UILineBreakMode.WordWrap);

			var labelFrame = label.Frame;
			labelFrame.Size = new SizeF (280, size.Height);
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

