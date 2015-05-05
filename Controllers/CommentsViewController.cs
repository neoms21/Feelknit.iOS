using System;
using Feelknit.iOS.Model;
using UIKit;
using Feelknit.iOS.Helpers;
using CoreGraphics;
using Foundation;
using System.Linq;
using Feelknit.Model;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Feelknit.iOS.Views;

//using MonoTouch.ObjCRuntime.Libraries.Foundation;
//using MonoTouch.ObjCRuntime.Libraries.UIKit;
using System.Collections.Generic;

namespace Feelknit.iOS.Controllers
{
	partial class CommentsViewController : BaseController
	{
		public Feeling Feeling{ get; set; }
		bool isKeyboardVisible;
		private LoadingOverlay _loadingOverlay;
		NSLayoutConstraint heightConstraint;


		public CommentsViewController (IntPtr handle)
			: base (handle)
		{
			Title = "Comments";
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

			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillHideNotification, OnKeyboardNotification, this.View.Window);
			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillShowNotification, OnKeyboardNotification, this.View.Window);

			var constraints = CommentTextView.Constraints.ToList ();
			heightConstraint = constraints.FirstOrDefault (c => c.FirstAttribute == NSLayoutAttribute.Height);


			//CommentsCountLabel.BackgroundColor = Resources.LoginButtonColor;
		}

		private void OnKeyboardNotification (NSNotification notification)
		{
			if (IsViewLoaded) {

				//Check if the keyboard is becoming visible
				bool visible = notification.Name == UIKeyboard.WillShowNotification;

				//Start an animation, using values from the keyboard
				UIView.BeginAnimations ("AnimateForKeyboard");
				UIView.SetAnimationBeginsFromCurrentState (true);
				UIView.SetAnimationDuration (UIKeyboard.AnimationDurationFromNotification (notification));
				UIView.SetAnimationCurve ((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification (notification));

				//Pass the notification, calculating keyboard height, etc.
				bool landscape = InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight;
				if (visible) {
					var keyboardFrame = UIKeyboard.FrameEndFromNotification (notification);

					OnKeyboardChanged (visible, landscape ? keyboardFrame.Width : keyboardFrame.Height);
				} else {
					var keyboardFrame = UIKeyboard.FrameBeginFromNotification (notification);

					OnKeyboardChanged (visible, landscape ? keyboardFrame.Width : keyboardFrame.Height);
				}

				//Commit the animation
				UIView.CommitAnimations ();	
			}
		}

		/// <summary>
		/// Override this method to apply custom logic when the keyboard is shown/hidden
		/// </summary>
		/// <param name='visible'>
		/// If the keyboard is visible
		/// </param>
		/// <param name='height'>
		/// Calculated height of the keyboard (width not generally needed here)
		/// </param>
		protected virtual void OnKeyboardChanged (bool visible, System.nfloat height)
		{
			//Console.WriteLine ("{0} - {1}", visible, height);
			var frame = this.View.Frame;
			if (visible && !isKeyboardVisible) {
				isKeyboardVisible = true;
				frame.Height -= height;
			} else {
				isKeyboardVisible = false;
				frame.Height += height;
			}	
			this.View.Frame = frame;

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if (Data != null) {
				var fromLoading = (bool)Data;
				if (fromLoading) {
					var bounds = UIScreen.MainScreen.Bounds; // portrait bounds

					_loadingOverlay = new LoadingOverlay(bounds, "Getting comments");
					this.View.Add(_loadingOverlay);
					Task.Factory.StartNew (RefreshFeeling).ContinueWith (DisplayComments);
				}

			} else {
				PopulateDetails ();
				CommentsTable.Source = new CommentsTableViewSource (Feeling);
			}

			CommentsTable.SeparatorColor = Resources.MainBackgroundColor;

			var tapRecognizer = new UITapGestureRecognizer ();
			CommentsTable.AddGestureRecognizer (tapRecognizer);
			tapRecognizer.AddTarget (() => {
				this.CommentTextView.ResignFirstResponder();
			});

			AddCommentButton.TouchUpInside += (sender, e) => {
				var comment = new Comment {
					Text = CommentTextView.Text,
					User = ApplicationHelper.UserName,
					UserAvatar = ApplicationHelper.Avatar,
					PostedAt = DateTime.UtcNow
				};
				CommentTextView.Text = string.Empty;
				SaveComment (comment);
			};
			var originalWidth = ((NSString)CommentTextView.Text).StringSize (font: CommentTextView.Font);
			var originalLines = 1;
			var lastCharLineBreak = false;
			var previousLength = CommentTextView.Text.Length;

			CommentTextView.Changed += (object sender, EventArgs e) => {
				InvokeOnMainThread (() => {

					if(CommentTextView.Text.Length == 0)
					{
						this.AddCommentButton.Hidden = true;
						return;
					}
					this.AddCommentButton.Hidden = false;
					if(originalLines == 4)
						return;

					var currentLength = CommentTextView.Text.Length;
					var size = ((NSString)CommentTextView.Text).StringSize (font: CommentTextView.Font);
					var tfWidth = size.Width;
					var x = (int) (originalWidth.Width ==0 ? 1: Math.Ceiling( tfWidth /originalWidth.Width));


					if(CommentTextView.Text.ElementAt(CommentTextView.Text.Length-1).ToString() == "\n" )
					{
						lastCharLineBreak = true;
						if (heightConstraint != null && previousLength < currentLength)
						{
							heightConstraint.Constant += 20;
							originalLines +=1;
							x = originalLines;
						}else
						{
							heightConstraint.Constant -= 20;
							originalLines -=1;
							x = originalLines;
						}
						originalLines = originalLines == 0 ? 1 :originalLines;
						previousLength = currentLength;
						originalWidth = ((NSString)CommentTextView.Text).StringSize (font: CommentTextView.Font);
						return;
					}
					else
					{
						if(lastCharLineBreak)
							x-=1;
						lastCharLineBreak = false;
					}
						
					if (originalLines != x || lastCharLineBreak) {
						if (heightConstraint != null)
						{
							heightConstraint.Constant = x < originalLines ? heightConstraint.Constant - 20 : heightConstraint.Constant + 20;
							originalLines = x == 0 ? 1 : x;
						}
					}
					originalWidth = ((NSString)CommentTextView.Text).StringSize (font: CommentTextView.Font);
					previousLength = currentLength;
					});
			};


			CommentsTable.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;


		}

		private void PopulateDetails()
		{
			UserIcon.Image = ResizeImage (UIImage.FromBundle (Feeling.UserAvatar != null ?
				"Avatars/" + Feeling.UserAvatar + ".png" : "userIcon.png"), 100, 100);

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

		}

		private void DisplayComments (Task task)
		{
			//CommentsTable.Source = new CommentsTableViewSource (Feeling.Comments.ToList ());
		}

		private async void SaveComment (Comment comment)
		{
			var client = new JsonHttpClient (Path.Combine (UrlHelper.COMMENTS, Feeling.Id));
			await client.PostRequest (comment);
			Feeling.Comments.Add (comment);
			InvokeOnMainThread (() => {
				CommentsTable.Source = new CommentsTableViewSource (Feeling);
				CommentsTable.ReloadData ();
			});
		}

		// resize the image (without trying to maintain aspect ratio)
		public UIImage ResizeImage (UIImage sourceImage, float width, float height)
		{
			UIGraphics.BeginImageContextWithOptions (new CGSize (width, height), false, 2.0f);
			sourceImage.Draw (new CGRect (0, 0, width, height));
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
				CommentsTable.Source = new CommentsTableViewSource (Feeling);
				CommentsTable.ReloadData ();

				CommentsCountLabel.Text = string.Format ("  {0} comments", Feeling.Comments.Count);
				PopulateDetails();
			});
		}

		private void ResizeHeigthWithText(UILabel label,float maxHeight = 960f) 
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
	}
}
