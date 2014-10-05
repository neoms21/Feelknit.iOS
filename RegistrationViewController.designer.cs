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
	[Register ("RegistrationViewController")]
	partial class RegistrationViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton RegisterButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField RegistrationPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView RegistrationUserName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (RegisterButton != null) {
				RegisterButton.Dispose ();
				RegisterButton = null;
			}
			if (RegistrationPassword != null) {
				RegistrationPassword.Dispose ();
				RegistrationPassword = null;
			}
			if (RegistrationUserName != null) {
				RegistrationUserName.Dispose ();
				RegistrationUserName = null;
			}
		}
	}
}
