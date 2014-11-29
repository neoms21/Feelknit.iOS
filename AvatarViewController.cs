using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace Feelknit.iOS
{
	partial class AvatarViewController : UIViewController
	{

	
		public AvatarViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			var tableSource = new AvatarTableViewSource (Resources.Avatars, AvatarSelected);
			AvatarsTableView.Source = tableSource;

		}

		private void AvatarSelected(string avatar)
		{

		}

		partial void SaveAvatarButton_TouchUpInside (UIButton sender)
		{
			throw new NotImplementedException ();
		}

		partial void SkipButton_TouchUpInside (UIButton sender)
		{
			throw new NotImplementedException ();
		}
	}
}
