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

namespace Feelknit.iOS.Controllers
{
	[Register ("RegisterViewController")]
	partial class RegisterViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField LocationTextView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton RegisterButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView RegisterController { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField RegisterUserName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField RegistrationEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField RegistrationPassword { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (LocationTextView != null) {
				LocationTextView.Dispose ();
				LocationTextView = null;
			}
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
			if (RegistrationEmail != null) {
				RegistrationEmail.Dispose ();
				RegistrationEmail = null;
			}
			if (RegistrationPassword != null) {
				RegistrationPassword.Dispose ();
				RegistrationPassword = null;
			}
		}
	}
}
