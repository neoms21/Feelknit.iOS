
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Feelknit.Model;

namespace Feelknit.iOS
{
	public partial class CommentCellView : UITableViewCell
	{
		public Comment Comment{ get; set; }


		public static readonly UINib Nib = UINib.FromName ("CommentCellView", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("CommentCellView");

		public CommentCellView (IntPtr handle) : base (handle)
		{

		}

		public static CommentCellView Create ()
		{
			return (CommentCellView)Nib.Instantiate (null, null) [0];
		
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
			CommentTimeLabel.Text = Comment.PostedAt.ToString ("dd-MMM-yyyy HH:mm");
			UserIconImageView.Image = UIImage.FromBundle ("Avatars/" + Comment.UserAvatar + ".png");

//			ReportAbuseLabel.Touch
		}

		private void ResizeHeigthWithText(UILabel label,float maxHeight = 100f) 
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

