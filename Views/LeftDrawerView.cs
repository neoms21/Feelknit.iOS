using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Feelknit.iOS.Helpers;
using System.Threading.Tasks;

namespace Feelknit.iOS
{
	[Register("LeftDrawerView")] 
	public partial class LeftDrawerView : UIView
	{
		public static readonly UINib Nib = UINib.FromName ("LeftDrawerView", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("LeftDrawerView");
		private static Action<string> _action;
		private static Action _actionSignOut;

		public LeftDrawerView (IntPtr handle) : base (handle)
		{

		}

		public static LeftDrawerView Create (Action<string> action, Action actionSignOut)
		{
			_action = action;
			_actionSignOut = actionSignOut;
			return (LeftDrawerView)Nib.Instantiate (null, null) [0];
		}


		public override void LayoutSubviews ()  
		{
			base.LayoutSubviews ();
			UserNameLabel.TextColor = Resources.LightButtonColor;
			SignoutButton.BackgroundColor = Resources.LightButtonColor;
			UserNameLabel.Text = ApplicationHelper.UserName;
			UserImageView.Image = UIImage.FromBundle (string.Format("Avatars/{0}.png", ApplicationHelper.Avatar));
			LeftDrawerTableView.Source = new LeftDrawerTableViewSource (Resources.LeftDrawerItems,_action);

			SignoutButton.TouchUpInside += (object sender, EventArgs e) => {
				_action.Invoke("");
				Task.Factory.StartNew (() => {

					_actionSignOut.Invoke();
				});
			};
		}
		
}
}

