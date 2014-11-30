using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Feelknit.iOS.Helpers;

namespace Feelknit.iOS
{
	[Register("LeftDrawerView")] 
	public partial class LeftDrawerView : UIView
	{
		public static readonly UINib Nib = UINib.FromName ("LeftDrawerView", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("LeftDrawerView");

		public LeftDrawerView (IntPtr handle) : base (handle)
		{

		}

		public static LeftDrawerView Create ()
		{
			return (LeftDrawerView)Nib.Instantiate (null, null) [0];

		}

		public override void LayoutSubviews ()  
		{
			base.LayoutSubviews ();

			UserNameLabel.Text = "neo";
			UserImageView.Image = UIImage.FromBundle ("Avatars/girl1.png");

//			SignoutButton.TouchUpInside+=
		}
	}
}

