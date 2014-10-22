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
		
				var frame = UIScreen.MainScreen.ApplicationFrame;
				this.View.Frame = frame;
				this.ScrollView.Frame = this.View.Bounds;
				this.ScrollView.ContentSize = new SizeF (this.View.Frame.Width, this.View.Bounds.Height);
				this.ScrollView.Scrolled += ScrollViewDidScroll;

				var item0 = new MenuItem ("Slide Menu",  (menuItem) => {
					Console.WriteLine("Item: {0}", menuItem);
					this.MoveButtonToXY(10,200);
					this.ChangeButtonBackground(0);
				});

				var item1 = new MenuItem ("Favourite",  (menuItem) => {
					Console.WriteLine("Item: {0}", menuItem);
					this.MoveButtonToXY(10,150);
					this.ChangeButtonBackground(1);
				});

				var item2 = new MenuItem ("Search", (menuItem) => {
					Console.WriteLine("Item: {0}", menuItem);
					this.MoveButtonToXY(10,250);
					this.ChangeButtonBackground(2);
				});

			for(int i = 0 ;i<15;i++)
			{
				var menuItem new MenuItem();
			}

				item0.Tag = 0;
				item1.Tag = 1;
				item2.Tag = 2;

				this.slideMenu = new SlideMenu (new List<MenuItem> { item0, item1, item2 }, new PointF(0f,100f));

				this.View.AddSubview (this.slideMenu);

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
