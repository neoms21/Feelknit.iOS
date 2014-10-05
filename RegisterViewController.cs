using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace Feelknit
{
	partial class RegisterViewController : UIViewController
	{
		public RegisterViewController (IntPtr handle) : base (handle)
		{
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

						RegisterButton.TouchUpInside += (object sender, EventArgs e) =>
			            {
				var user = new Model.User{UserName="xxx",Password="yyy",EmailAddress="sss@ss.com"};
				var client = new JsonHttpClient("Users");
				client.PostRequest(user);
			            };
		}
	}
}
