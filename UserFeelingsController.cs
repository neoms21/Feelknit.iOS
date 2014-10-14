using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Feelknit.Model;
using Feelknit.TableViewSources;
using Feelknit.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Newtonsoft.Json;

namespace Feelknit
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
            if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight)
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
