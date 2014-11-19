using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Feelknit.iOS.Helpers;
using Feelknit.iOS.Model;
using Feelknit.iOS.TableViewSources;
using Feelknit.iOS.Views;
using MonoTouch.UIKit;
using Newtonsoft.Json;

namespace Feelknit.iOS.Controllers
{
	partial class UserFeelingsController : UIViewController
	{
		private IEnumerable<Feeling> _feelings;

		private LoadingOverlay _loadingOverlay;

		event GetUserFeelingsDelegate GetFeelings;

		public UserFeelingsController (IntPtr handle)
			: base (handle)
		{
			//this.EdgesForExtendedLayout = UIRectEdge.None;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.SetToolbarItems( new UIBarButtonItem[] {
				new UIBarButtonItem(UIBarButtonSystemItem.Refresh, (s,e) => {
					Console.WriteLine("Refresh clicked");
				})
				, new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) { Width = 50 }
				, new UIBarButtonItem(UIBarButtonSystemItem.Pause, (s,e) => {
					Console.WriteLine ("Pause clicked");
				})
			}, false);
			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds

			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft
			             || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
				bounds.Size = new SizeF (bounds.Size.Height, bounds.Size.Width);
			}

			CreateNewFeelingButton.TouchUpInside+= (object sender,EventArgs e) => {
				var addFeelingViewController =
					this.Storyboard.InstantiateViewController("AddFeelingViewController") as AddFeelingViewController;
				if (addFeelingViewController != null)
				{
					this.NavigationController.PushViewController(addFeelingViewController, true);
				}
			};

			this.NavigationController.NavigationBarHidden = true;
			// show the loading overlay on the UI thread using the correct orientation sizing
			_loadingOverlay = new LoadingOverlay (bounds,"Getting feelings..");
			this.View.Add (_loadingOverlay);

			GetFeelings += async () => {
				_feelings = await GetUserFeelings ();

				UserFeelingsTableView.Source = new UserFeelingsTableViewSource (_feelings.ToList (),OnRowSelection);
				UserFeelingsTableView.ReloadData ();

				_loadingOverlay.Hide ();
			};

			GetFeelings.Invoke ();
		}

		private void OnRowSelection(Feeling feeling)
		{
			var commentsViewController =
				this.Storyboard.InstantiateViewController("CommentsViewController") as CommentsViewController;
			if (commentsViewController != null)
			{
				commentsViewController.Feeling = feeling;
				this.NavigationController.PushViewController(commentsViewController, true);
			}
		}

		private async Task<IEnumerable<Feeling>> GetUserFeelings ()
		{
			var client = new JsonHttpClient (string.Format(UrlHelper.USER_FEELINGS,ApplicationHelper.UserName));
			var result = await client.GetRequest ();

			_feelings = JsonConvert.DeserializeObject<IEnumerable<Feeling>> (result);
			_feelings.First ().IsFirstFeeling = true;
			return _feelings;
		}
	}

	internal delegate void GetUserFeelingsDelegate ();

}
