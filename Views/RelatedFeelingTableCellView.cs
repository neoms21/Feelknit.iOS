
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
			FeelingTextLabel.Text = Feeling.GetFeelingFormattedText ("");
			CommentsLabel.Text = string.Format ("Comments {0}", Feeling.Comments.Count);
			SupportLabel.Text = string.Format ("Support {0}", Feeling.SupportCount);
			FeelingDate.Text = Feeling.FeelingDate.ToString ("dd MMM yyyy");
		}
	}
}

