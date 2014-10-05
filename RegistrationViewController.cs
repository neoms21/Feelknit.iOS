using System;
using Feelknit.Model;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace Feelknit
{
	partial class RegistrationViewController : UIViewController
	{
		public RegistrationViewController (IntPtr handle) : base (handle)
		{

            
		}

	    public override void ViewDidLoad()
	    {
	        base.ViewDidLoad();

            //var user = new User
            //{
            //    UserName = "xxx",
            //    Password = RegistrationPassword.Text,
            //    EmailAddress = "jskdfj@dal.com"
            //};
            //var client = new JsonHttpClient("Users");

            //var result = client.PostRequest(user);

	    }
	}
}
