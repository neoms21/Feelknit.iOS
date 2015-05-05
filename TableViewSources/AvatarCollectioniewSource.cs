using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;

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

		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var avatarCell = (AvatarCell)collectionView.DequeueReusableCell (avatarCellId, indexPath);

			var avatar = _imageNames [indexPath.Row];

			avatarCell.Image = UIImage.FromBundle(string.Format("Avatars/{0}.png",avatar));
			return avatarCell;
		}

		public override nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return _imageNames.Count;
		}

		public override void ItemSelected (UICollectionView collectionView, NSIndexPath indexPath)
		{
			_action.Invoke (_imageNames [indexPath.Row]);
		}

		public override nint NumberOfSections (UICollectionView collectionView)
		{
			return 1;
		}
			
	}
}

