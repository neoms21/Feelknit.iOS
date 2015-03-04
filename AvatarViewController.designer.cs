// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace Feelknit.iOS
{
	[Register ("AvatarViewController")]
	partial class AvatarViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView AvatarsTableView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton SaveAvatarButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton SkipButton { get; set; }

		[Action ("SaveAvatarButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void SaveAvatarButton_TouchUpInside (UIButton sender);

		[Action ("SkipButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void SkipButton_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (AvatarsTableView != null) {
				AvatarsTableView.Dispose ();
				AvatarsTableView = null;
			}
			if (SaveAvatarButton != null) {
				SaveAvatarButton.Dispose ();
				SaveAvatarButton = null;
			}
			if (SkipButton != null) {
				SkipButton.Dispose ();
				SkipButton = null;
			}
		}
	}
}
