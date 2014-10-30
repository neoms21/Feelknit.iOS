using System;
using MonoTouch.UIKit;
using SlideDownMenu;
using System.Drawing;
using System.Collections.Generic;

namespace Feelknit
{
	partial class AddFeelingViewController : UIViewController
	{
		private SlideMenu slideMenu;

		public AddFeelingViewController (IntPtr handle) : base (handle)
		{
		}

	    public override void ViewDidLoad()
	    {
	        base.ViewDidLoad();
            this.NavigationController.NavigationBarHidden = false;
			#region View lifecycle
		
				

				this.MainButton.TouchUpInside += MainButtonPressed;
			}

			#endregion

			void MainButtonPressed (object sender, EventArgs e)
			{
				this.slideMenu.ToggleMenu ();
			}

			private void ScrollViewDidScroll (object sender, EventArgs e)
			{
				this.slideMenu.OpenIconMenu ();
			}

			private void MoveButtonToXY(float x, float y)
			{
				UIView.Animate(0.2, () => {
					this.MainButton.Frame = new RectangleF(x, y, this.MainButton.Bounds.Width, this.MainButton.Bounds.Height);
				});
			}

			private void ChangeButtonBackground(int buttonNumber)
			{
//				UIView.Animate(0.2, () => {
//					this.MainButton.SetBackgroundImage(UIImage.FromBundle (string.Format("Images/a{0}.png", buttonNumber)), UIControlState.Normal);
//				});
			}
	    
	}
}
