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
		private static Action<string> _action;

		public LeftDrawerView (IntPtr handle) : base (handle)
		{

		}

		public static LeftDrawerView Create (Action<string> action)
		{
			_action = action;
			return (LeftDrawerView)Nib.Instantiate (null, null) [0];
		}

		public override void LayoutSubviews ()  
		{
			base.LayoutSubviews ();
			UserNameLabel.Text = ApplicationHelper.UserName;
			UserImageView.Image = UIImage.FromBundle (string.Format("Avatars/{0}.png", ApplicationHelper.Avatar));
			LeftDrawerTableView.Source = new LeftDrawerTableViewSource (Resources.LeftDrawerItems,_action);
		}
	}
}

