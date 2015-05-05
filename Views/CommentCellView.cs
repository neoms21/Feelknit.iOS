
using System;
using CoreGraphics;

using Foundation;
using UIKit;
using Feelknit.Model;
using Feelknit.iOS.Helpers;
using System.Collections.Specialized;
using Feelknit.iOS.Model;

namespace Feelknit.iOS
{
	public partial class CommentCellView : UITableViewCell
	{
		public Comment Comment{ get; set; }
		public Feeling Feeling{ get; set;}

		public static readonly UINib Nib = UINib.FromName ("CommentCellView", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("CommentCellView");

		public CommentCellView (IntPtr handle) : base (handle)
		{

		}

		public static CommentCellView Create ()
		{
			return (CommentCellView)Nib.Instantiate (null, null) [0];
		
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			ReportAbuseButton.BackgroundColor = Resources.LightButtonColor;
			ReportAbuseButton.SetTitleColor (UIColor.White, UIControlState.Normal);
			ReportAbuseButton.Layer.BorderColor = UIColor.Black.CGColor;
			ReportAbuseButton.Layer.CornerRadius = 5f;
			ReportAbuseButton.Layer.BorderWidth = 1f;


			ReportAbuseButton.TouchUpInside+= (object sender, EventArgs e) => {
				Comment.IsReported = true;
				CommentReportedLabel.Hidden = false;
				ReportAbuseButton.Enabled = false;
				ReportAbuseButton.BackgroundColor = Resources.DisabledColor;
				ReportComment();
			};
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			var frame = CommentTextLabel.Frame;
			CommentTextLabel.SizeToFit ();
			CommentTextLabel.PreferredMaxLayoutWidth = 200;
			CommentTextLabel.LineBreakMode = UILineBreakMode.CharacterWrap;

			CommentTextLabel.Text = Comment.Text.Replace("\n","");
			ResizeHeigthWithText (CommentTextLabel);

			UserNameLabel.Text = Comment.User;
			UserNameLabel.TextColor = Resources.LightButtonColor;
			CommentTimeLabel.Text = Comment.PostedAt.ToLocalTime().ToString ("dd-MMM-yyyy HH:mm");
			UserIconImageView.Image = UIImage.FromBundle ( !string.IsNullOrEmpty(Comment.UserAvatar) ? "Avatars/" + Comment.UserAvatar + ".png" : "userIcon.png");
			ReportAbuseButton.Hidden = Comment.User == ApplicationHelper.UserName;

			if (Comment.IsReported) {
				CommentReportedLabel.Hidden = false;
				ReportAbuseButton.Enabled = false;
				ReportAbuseButton.BackgroundColor = Resources.DisabledColor;
			} else {
				CommentReportedLabel.Hidden = true;
				ReportAbuseButton.Enabled = true;
				ReportAbuseButton.BackgroundColor = Resources.LightButtonColor;
			}
		}

		private void ResizeHeigthWithText(UILabel label,float maxHeight = 100f) 
		{
			label.AdjustsFontSizeToFitWidth = false;
			float width = 280;// label.Frame.Width;  
			label.Lines = 0;
			CGSize size = ((NSString)label.Text).StringSize(label.Font,  
				constrainedToSize:new CGSize(width,maxHeight) ,lineBreakMode:UILineBreakMode.WordWrap);

			var labelFrame = label.Frame;
			labelFrame.Size = new CGSize(280,size.Height);
			label.Frame = labelFrame; 
		}

		private void ReportComment()
		{
			var client = new JsonHttpClient (UrlHelper.REPORT_COMMENT);
			var collection = new NameValueCollection ();
			collection.Add ("feelingId", Feeling.Id);
			collection.Add ("id", Comment.Id.ToString());
			collection.Add ("reportedBy", ApplicationHelper.UserName);
			client.PostRequestWithParams (collection);
		}

	}
}

