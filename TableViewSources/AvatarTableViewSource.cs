using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Feelknit.iOS
{
	public class AvatarTableViewSource : UITableViewSource
	{

		IList<string> _imageNames;

		Action<string> _action;
		public AvatarTableViewSource (IList<string> imageNames, Action<string> action)
		{
			_action = action;
			_imageNames = imageNames;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return _imageNames.ToList().Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var imageName = _imageNames[indexPath.Row];
			var cell = new UITableViewCell ();

			var imageView = new UIImageView (UIImage.FromBundle ("Avatars/"+ imageName+".png"));
			imageView.Frame = new RectangleF (10,10,50, 50);
			cell.Add (imageView);
			return cell;
		}

		public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			_action.Invoke (_imageNames[indexPath.Row]);
		}

		public override float GetHeightForRow (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			return 65;
		}
	}
}

