using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace Feelknit.iOS
{
	public class AvatarCell : UICollectionViewCell
	{
		UIImageView imageView;

		[Export ("initWithFrame:")]
		public AvatarCell (RectangleF frame) : base (frame)
		{
			BackgroundView = new UIView{BackgroundColor = UIColor.Orange};

			SelectedBackgroundView = new UIView{BackgroundColor = UIColor.Green};

			ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
			ContentView.Layer.BorderWidth = 1.0f;
			ContentView.BackgroundColor = Resources.WhiteColor;
			//ContentView.Transform = CGAffineTransform.MakeScale (1.2f, 1.2f);

			imageView = new UIImageView (UIImage.FromBundle ("userIcon.png"));
			imageView.Center = ContentView.Center;
//			imageView.Transform = CGAffineTransform.MakeScale (1.5f, 1.5f);

			ContentView.AddSubview (imageView);
		}

		public UIImage Image {
			set {
				imageView.Image = value;
			}
		}

//		[Export("custom")]
//		void Custom()
//		{
//			// Put all your custom menu behavior code here
//			Console.WriteLine ("custom in the cell");
//		}

//
//		public override bool CanPerform (Selector action, NSObject withSender)
//		{
//			if (action == new Selector ("custom"))
//				return true;
//			else
//				return false;
//		}
	}
}

