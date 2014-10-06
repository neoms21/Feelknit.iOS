using System;
using Feelknit.Model;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace Feelknit
{
    partial class RegisterViewController : UIViewController
    {
        public RegisterViewController(IntPtr handle)
            : base(handle)
        {
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            RegisterButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                var user = new User { UserName = RegisterUserName.Text, Password = RegistrationPassword.Text, EmailAddress = RegistrationEmail.Text };
                SaveUser(user);
            };
        }

        private async void SaveUser(User user)
        {
            var client = new JsonHttpClient("Users");
            await client.PostRequest(user);
        }
    }
}
