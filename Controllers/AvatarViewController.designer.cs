// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using UIKit;
using System;
using System.CodeDom.Compiler;

namespace Feelknit.iOS
{
	[Register ("AvatarViewController")]
	partial class AvatarViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UICollectionView AvatarCollectionView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton SaveButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton SkipButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView UserAvatarImageView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (AvatarCollectionView != null) {
				AvatarCollectionView.Dispose ();
				AvatarCollectionView = null;
			}
			if (SaveButton != null) {
				SaveButton.Dispose ();
				SaveButton = null;
			}
			if (SkipButton != null) {
				SkipButton.Dispose ();
				SkipButton = null;
			}
			if (UserAvatarImageView != null) {
				UserAvatarImageView.Dispose ();
				UserAvatarImageView = null;
			}
		}
	}
}
