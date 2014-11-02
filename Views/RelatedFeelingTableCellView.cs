
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Feelknit.iOS
{
	public partial class RelatedFeelingTableCellView : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("RelatedFeelingTableCellView", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("RelatedFeelingTableCellView");

		public RelatedFeelingTableCellView (IntPtr handle) : base (handle)
		{
		}

		public static RelatedFeelingTableCellView Create ()
		{
			return (RelatedFeelingTableCellView)Nib.Instantiate (null, null) [0];
		}
	}
}

