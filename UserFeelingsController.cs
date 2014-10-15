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
            // show the loading overlay on the UI thread using the correct orientation sizing
            _loadingOverlay = new LoadingOverlay(bounds);
            this.View.Add(_loadingOverlay);

            GetFeelings += async () =>
            {
                _feelings = await GetUserFeelings();
                UserFeelingsTable.Source = new UserFeelingsTableViewSource(_feelings.ToList());
                UserFeelingsTable.ReloadData();
                _loadingOverlay.Hide();
            };

            GetFeelings.Invoke();
        }

        private async Task<IEnumerable<Feeling>> GetUserFeelings()
        {
            var client = new JsonHttpClient("feelings/username/neo");
            var result = await client.GetRequest();

            _feelings = JsonConvert.DeserializeObject<IEnumerable<Feeling>>(result);
            return _feelings;
        }
    }

    internal delegate void GetUserFeelingsDelegate();

}
