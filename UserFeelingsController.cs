using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Feelknit.iOS.Model;
using Feelknit.iOS.TableViewSources;
using Feelknit.iOS.Views;
using Feelknit.Model;
using MonoTouch.UIKit;
using Newtonsoft.Json;

namespace Feelknit.iOS
{
    partial class UserFeelingsController : UIViewController
    {
        private IEnumerable<Feeling> _feelings;

        private LoadingOverlay _loadingOverlay;

        event GetUserFeelingsDelegate GetFeelings;
        public UserFeelingsController(IntPtr handle)
            : base(handle)
        {
			//this.EdgesForExtendedLayout = UIRectEdge.None;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var bounds = UIScreen.MainScreen.Bounds; // portrait bounds

            if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft
                || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight)
            {
                bounds.Size = new SizeF(bounds.Size.Height, bounds.Size.Width);
            }

			this.NavigationController.NavigationBarHidden = true;
			//CreateNewFeelingButton.VerticalAlignment = UIControlContentVerticalAlignment.Bottom;
			//CreateNewFeelingButton.Frame = new RectangleF(0,bounds.Height-50,bounds.Width,35);
			//CreateNewFeelingButton.SizeToFit();
            // show the loading overlay on the UI thread using the correct orientation sizing
            _loadingOverlay = new LoadingOverlay(bounds);
            this.View.Add(_loadingOverlay);

            GetFeelings += async () =>
            {
                _feelings = await GetUserFeelings();


				//CreateNewFeelingButton.Frame = new RectangleF(0,bounds.Height-35,bounds.Width,35);
				//MyFeelingsLabel.Frame = new RectangleF(0, this.NavigationController.NavigationBar.Frame.Height,320,35);

//				UserFeelingsTable.Frame = new RectangleF(0,30,bounds.Size.Width,bounds.Size.Height - 30);
                UserFeelingsTable.Source = new UserFeelingsTableViewSource(_feelings.ToList());
                UserFeelingsTable.ReloadData();

//				UIView header = new UIView{Frame = new RectangleF(0,0,320,50), BackgroundColor = UIColor.Blue};
//				UILabel label = new UILabel{Text="My Recent Feelings"};
//				label.Frame = new RectangleF(80,0,320,50);
//
//				header.AddSubview(label);
//				UserFeelingsTable.TableHeaderView = header;

				_loadingOverlay.Hide();
            };

            GetFeelings.Invoke();
        }

        private async Task<IEnumerable<Feeling>> GetUserFeelings()
        {
            var client = new JsonHttpClient("feelings/username/neo");
            var result = await client.GetRequest();

            _feelings = JsonConvert.DeserializeObject<IEnumerable<Feeling>>(result);
            _feelings.First().IsFirstFeeling = true;
            return _feelings;
        }
    }

    internal delegate void GetUserFeelingsDelegate();

}
