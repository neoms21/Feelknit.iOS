// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace Feelknit
{
	[Register ("RegisterViewController")]
	partial class RegisterViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton RegisterButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView RegisterController { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField RegisterUserName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (RegisterButton != null) {
				RegisterButton.Dispose ();
				RegisterButton = null;
			}
			if (RegisterController != null) {
				RegisterController.Dispose ();
				RegisterController = null;
			}
			if (RegisterUserName != null) {
				RegisterUserName.Dispose ();
				RegisterUserName = null;
			}
		}
	}
}
