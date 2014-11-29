using System;
using System.Drawing;
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

            SetImageAndMargin(RegisterUserName, "userIcon.png");
            SetImageAndMargin(RegistrationPassword, "password.png");
            SetImageAndMargin(RegistrationEmail, "004.png");
            RegisterButton.BackgroundColor = Resources.LoginButtonColor;

            RegisterButton.TouchUpInside += (sender, e) =>
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


        private void SetImageAndMargin(UITextField uiTextField, string image)
        {
            var imageView = new UIImageView(UIImage.FromBundle(image))
            {
                // Indent it 10 pixels from the left.
                Frame = new RectangleF(10, 0, 20, 20)
            };

            var leftView = new UIView(new Rectangle(0, 0, 30, 20));
            leftView.AddSubview(imageView);
            uiTextField.LeftViewMode = UITextFieldViewMode.Always;
            uiTextField.LeftView = leftView;
        }
    }
}
