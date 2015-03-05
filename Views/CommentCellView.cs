
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Feelknit.Model;

namespace Feelknit.iOS
{
	public partial class CommentCellView : UITableViewCell
	{
		public Comment Comment{ get; set;}


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
			CommentTextView.Text = Comment.Text;
				UserNameLabel.Text = Comment.User;
			CommentTimeLabel.Text = Comment.PostedAt.ToShortTimeString ();
			//UserIconImageView.Image = UIImage.FromBundle ("Avatars/"+ Comment.UserAvatar +".png")
			
		}
	}
}

