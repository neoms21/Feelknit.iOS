using System.Drawing;
using MonoTouch.UIKit;

namespace Feelknit.iOS.Views
{
    public sealed class UserFeelingCellView : UITableViewCell
    {
        private readonly UILabel _feelingTextField;
        private readonly UILabel _commentsTextField;
        private readonly UILabel _supportTextField;
        private readonly UILabel _timeTextField;

        public UserFeelingCellView(string cellId)
            : base(UITableViewCellStyle.Default, cellId)
        {
            _feelingTextField = GetNewLabel(UITextAlignment.Left);
            _commentsTextField = GetNewLabel(UITextAlignment.Left);
            _supportTextField = GetNewLabel(UITextAlignment.Left);
            _timeTextField = GetNewLabel(UITextAlignment.Right);

            _feelingTextField.SizeToFit();
            _supportTextField.SizeToFit();
            _commentsTextField.SizeToFit();
            _timeTextField.SizeToFit();


            ContentView.Add(_feelingTextField);
            ContentView.Add(_commentsTextField);
            ContentView.Add(_supportTextField);
            ContentView.Add(_timeTextField);
        }

        private static UILabel GetNewLabel(UITextAlignment alignment)
        {
            return new UILabel
            {
                Lines = 0,
                LineBreakMode = UILineBreakMode.WordWrap,
                TextAlignment = alignment,
                Font = UIFont.FromName("Helvetica", 12)
            };
        }


        public void UpdateCell(string feelingText, string commentText, string supportText, string timeText)
        {
            _feelingTextField.Text = feelingText;//+ ContentView.Bounds.Width.ToString();
            _commentsTextField.Text = commentText;
            _supportTextField.Text = supportText;
            _timeTextField.Text = timeText;


            ContentView.BackgroundColor = UIColor.White;
            _feelingTextField.TextColor = UIColor.Black;
            _commentsTextField.TextColor = UIColor.Black;
            _supportTextField.TextColor = UIColor.Black;
            _timeTextField.TextColor = UIColor.Black;


        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            _feelingTextField.Frame = new RectangleF(5, 5, ContentView.Bounds.Width, 30);
            _commentsTextField.Frame = new RectangleF(5, 35, 90, 30);
            _supportTextField.Frame = new RectangleF(100, 35, 80, 30);
            _timeTextField.Frame = new RectangleF(220, 35, 80, 30);
        }
    }
}

