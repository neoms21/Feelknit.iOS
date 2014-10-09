using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Feelknit
{
    [Register("PaddingTextField"), DesignTimeVisible(true)]
    public class PaddingTextField : UITextField
    {
        public UIEdgeInsets EdgeInsets { get; set; }

        public PaddingTextField()
        {
            EdgeInsets = UIEdgeInsets.Zero;
        }
        public PaddingTextField(IntPtr intPtr)
            : base(intPtr)
        {
            EdgeInsets = new UIEdgeInsets(2, 10, 2, 2);
        }

        public override RectangleF TextRect(RectangleF forBounds)
        {
            return base.TextRect(InsetRect(forBounds, EdgeInsets));
        }

        public override RectangleF EditingRect(RectangleF forBounds)
        {
            return base.EditingRect(InsetRect(forBounds, EdgeInsets));
        }

        // Workaround until this method is available in Xamarin.iOS
        public static RectangleF InsetRect(RectangleF rect, UIEdgeInsets insets)
        {
            return new RectangleF(rect.X + insets.Left,
                                   rect.Y + insets.Top,
                                   rect.Width - insets.Left - insets.Right,
                                   rect.Height - insets.Top - insets.Bottom);
        }
    }
}
