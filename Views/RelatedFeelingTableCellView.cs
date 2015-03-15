
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Feelknit.iOS.Model;

namespace Feelknit.iOS
{
	public partial class RelatedFeelingTableCellView : UITableViewCell
	{
		public Feeling Feeling{ get; set;}
		public static readonly UINib Nib = UINib.FromName ("RelatedFeelingTableCellView", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("RelatedFeelingTableCellView");

		public RelatedFeelingTableCellView (IntPtr handle) : base (handle)
		{

		}

		public static RelatedFeelingTableCellView Create ()
		{
			return (RelatedFeelingTableCellView)Nib.Instantiate (null, null) [0];

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

			var fulltext = string.Format ("{0} {1}", Feeling.UserName, Feeling.GetFeelingFormattedText (""));
			var startIndexOfFeeling = Feeling.UserName.Length + 13; // fulltexy is in format of username was feeling FeellingText
			var prettyString = new NSMutableAttributedString (fulltext);
			prettyString.SetAttributes (firstAttributes.Dictionary, new NSRange (0, Feeling.UserName.Length));
			prettyString.SetAttributes (boldAttributes.Dictionary, new NSRange (startIndexOfFeeling, Feeling.FeelingText.Length));

			userImageView.Image = UIImage.FromBundle (string.IsNullOrWhiteSpace (Feeling.UserAvatar) ? "userIcon.png":
				string.Format("Avatars/{0}.png",Feeling.UserAvatar));
			FeelingTextLabel.AttributedText = prettyString ;

			ResizeHeigthWithText (FeelingTextLabel);
			CommentsLabel.Text = string.Format ("Comments {0}", Feeling.Comments.Count == 0 ? Feeling.CommentsCount:Feeling.Comments.Count);
			SupportLabel.Text = string.Format ("Support {0}", Feeling.SupportCount);
			FeelingDate.Text = Feeling.FeelingDate.ToString ("dd MMM yyyy HH:mm");
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

