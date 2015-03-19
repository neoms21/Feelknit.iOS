using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MonoTouch.Foundation;

namespace Feelknit.iOS
{
	public class AvatarCollectioniewSource : UICollectionViewSource
	{

		IList<string> _imageNames;
		static NSString avatarCellId = new NSString ("AvatarCell");

		Action<string> _action;
		public AvatarCollectioniewSource (IList<string> imageNames, Action<string> action)
		{
			_action = action;
			_imageNames = imageNames;
		}

		public override UICollectionViewCell GetCell (UICollectionView collectionView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var avatarCell = (AvatarCell)collectionView.DequeueReusableCell (avatarCellId, indexPath);

			var avatar = _imageNames [indexPath.Row];

			avatarCell.Image = UIImage.FromBundle(string.Format("Avatars/{0}.png",avatar));
			return avatarCell;
		}

		public override int GetItemsCount (UICollectionView collectionView, int section)
		{
			return _imageNames.Count;
		}

		public override void ItemSelected (UICollectionView collectionView, NSIndexPath indexPath)
		{
			_action.Invoke (_imageNames [indexPath.Row]);
		}

		public override int NumberOfSections (UICollectionView collectionView)
		{
			return 1;
		}
			
	}
}

