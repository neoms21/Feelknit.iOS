
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Feelknit.iOS
{
	public partial class LeftDrawerTableCellView : UITableViewCell
	{
		public LeftDrawerItem LeftDrawerItem{ get; set;}
		public static readonly UINib Nib = UINib.FromName ("LeftDrawerTableCellView", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("LeftDrawerTableCellView");

		public LeftDrawerTableCellView (IntPtr handle) : base (handle)
		{

		}

		public static LeftDrawerTableCellView Create ()
		{
			return (LeftDrawerTableCellView)Nib.Instantiate (null, null) [0];
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			LeftDrawerItemImage.Image = UIImage.FromBundle (LeftDrawerItem.Image + ".png");
			LeftDrawerItemText.Text = LeftDrawerItem.Text;

		}


	}
}

