using System;
using Feelknit.Model;
using MonoTouch.UIKit;

namespace Feelknit.iOS.Controllers
{
    partial class RegisterViewController : BaseController
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
            var client = new JsonHttpClient(UrlHelper.USER);
            await client.PostRequest(user);
        }
    }
}
