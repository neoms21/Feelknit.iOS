using System;
using Feelknit.iOS.Model;
using MonoTouch.UIKit;
using Feelknit.iOS.Helpers;
using System.Drawing;
using MonoTouch.Foundation;
using System.Linq;
using Feelknit.Model;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Feelknit.iOS.Views;

namespace Feelknit.iOS.Controllers
{
	partial class CommentsViewController : BaseController
	{
		public Feeling Feeling{ get; set; }

		private LoadingOverlay _loadingOverlay;

		public CommentsViewController (IntPtr handle)
			: base (handle)
		{
			NavigationButtonVisible = false;
		}

		public override void ViewWillAppear (bool animated)
		{
			View.BackgroundColor = Resources.MainBackgroundColor;
			CommentsCountLabel.BackgroundColor = Resources.LoginButtonColor;

			this.NavigationController.NavigationBarHidden = false;
			AddCommentButton.Hidden = true;

			CommentTextView.Layer.BorderColor = UIColor.Black.CGColor;
			CommentTextView.Layer.CornerRadius = 2;
			CommentTextView.ScrollEnabled = false;

			UserNameLabel.SizeToFit ();
			UserNameLabel.PreferredMaxLayoutWidth = 200;
			UserNameLabel.LineBreakMode = UILineBreakMode.WordWrap;

			//CommentsCountLabel.BackgroundColor = Resources.LoginButtonColor;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			UserIcon.Image = ResizeImage (UIImage.FromBundle ("Avatars/" + ApplicationHelper.Avatar + ".png"), 100, 100);

			//UserNameLabel.Text = ApplicationHelper.UserName == Feeling.UserName ? "I" : Feeling.UserName;
			//FeelingTextLabel.Text = Feeling.GetFeelingFormattedText ("");

			var firstAttributes = new UIStringAttributes {
				ForegroundColor = Resources.LightButtonColor,
				Font = UIFont.BoldSystemFontOfSize (12)
			};

			var boldAttributes = new UIStringAttributes {

				Font = UIFont.BoldSystemFontOfSize (12)
			};

			var fulltext = string.Format ("{0} {1}", Feeling.UserName, Feeling.GetFeelingFormattedText (""));
			var startIndexOfFeeling = Feeling.UserName.Length + 13; // fulltext is in format of 'username was feeling' FeellingText
			var prettyString = new NSMutableAttributedString (fulltext);
			prettyString.SetAttributes (firstAttributes.Dictionary, new NSRange (0, Feeling.UserName.Length));
			prettyString.SetAttributes (boldAttributes.Dictionary, new NSRange (startIndexOfFeeling, Feeling.FeelingText.Length));

			UserNameLabel.AttributedText = prettyString;

			ResizeHeigthWithText (UserNameLabel, 400);
			CommentsCountLabel.Text = string.Format ("  {0} comments", Feeling.Comments.Count);

			CommentsTable.SeparatorColor = Resources.MainBackgroundColor;
			var constraints = CommentTextView.Constraints;
			//var heightConstraint = constraints.FirstOrDefault(c=>c.Constant == 30);
			//AddCommentButton.Title = "Cli";
			AddCommentButton.TouchUpInside += (sender, e) => {
				var comment = new Comment {
					Text = CommentTextView.Text,
					User = ApplicationHelper.UserName,
					UserAvatar = ApplicationHelper.Avatar,
					PostedAt = DateTime.UtcNow
				};

				SaveComment (comment);
			};

			CommentTextView.Changed += (object sender, EventArgs e) => {
				InvokeOnMainThread (() => {
					if (this.CommentTextView.Text.Length > 0) {
						//						var size =	this.CommentTextView.StringSize(CommentTextView.Text,UIFont.SystemFontOfSize(12));
						this.AddCommentButton.Hidden = false;
						//	heightConstraint.Constant = 50;
						//var size = CommentTextView.SizeThatFits(Comm;


					} else {

						this.AddCommentButton.Hidden = true;
					}
				});
			};


			CommentsTable.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;

			if (Data != null) {
				var fromLoading = (bool)Data;
				if (fromLoading) {
					var bounds = UIScreen.MainScreen.Bounds; // portrait bounds

					if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft
						|| UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight)
					{
						bounds.Size = new SizeF(bounds.Size.Height, bounds.Size.Width);
					}
					_loadingOverlay = new LoadingOverlay(bounds, "Getting comments");
					this.View.Add(_loadingOverlay);
					Task.Factory.StartNew (RefreshFeeling).ContinueWith (DisplayComments);
				}

			} else {
				CommentsTable.Source = new CommentsTableViewSource (Feeling.Comments.ToList ());
			}
		}

		private void DisplayComments (Task task)
		{
			CommentsTable.Source = new CommentsTableViewSource (Feeling.Comments.ToList ());
		}

		private async void SaveComment (Comment comment)
		{
			var client = new JsonHttpClient (Path.Combine (UrlHelper.COMMENTS, Feeling.Id));
			await client.PostRequest (comment);
			Feeling.Comments.Add (comment);
			InvokeOnMainThread (() => {
				CommentsTable.Source = new CommentsTableViewSource (Feeling.Comments.ToList ());
				CommentsTable.ReloadData ();
			});
		}

		// resize the image (without trying to maintain aspect ratio)
		public UIImage ResizeImage (UIImage sourceImage, float width, float height)
		{
			UIGraphics.BeginImageContextWithOptions (new SizeF (width, height), false, 2.0f);
			sourceImage.Draw (new RectangleF (0, 0, width, height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return resultImage;
		}

		private async void RefreshFeeling ()
		{
			var client = new JsonHttpClient (string.Format (UrlHelper.FEELINGS + "/" + Feeling.Id, ApplicationHelper.UserName));
			var result = await client.GetRequest ();

			Feeling = JsonConvert.DeserializeObject<Feeling> (result);

			InvokeOnMainThread (() => {
				_loadingOverlay.Hide();
				CommentsTable.Source = new CommentsTableViewSource (Feeling.Comments.ToList ());
				CommentsTable.ReloadData ();
			});
		}

		private void ResizeHeigthWithText(UILabel label,float maxHeight = 960f) 
		{
			label.AdjustsFontSizeToFitWidth = false;
			float width = 280;// label.Frame.Width;  
			label.Lines = 0;
			SizeF size = ((NSString)label.Text).StringSize(label.Font,  
				constrainedToSize:new SizeF(width,maxHeight) ,lineBreakMode:UILineBreakMode.WordWrap);

			var labelFrame = label.Frame;
			labelFrame.Size = new SizeF(280,size.Height);
			label.Frame = labelFrame; 
		}
	}
}
