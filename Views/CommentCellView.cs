
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
			var frame = CommentTextView.Frame;

//			frame.Width = ContentView.Bounds.Width;
//			frame.Height = 100;
//			CommentTextView.Frame = frame;
			CommentTextView.Text = Comment.Text;
			CommentTextView.ScrollEnabled = false;
			UserNameLabel.Text = Comment.User;
			CommentTimeLabel.Text = Comment.PostedAt.ToString ("dd-MMM-yyyy HH:mm");
			UserIconImageView.Image = UIImage.FromBundle ("Avatars/" + Comment.UserAvatar + ".png");
		}

	}
}

