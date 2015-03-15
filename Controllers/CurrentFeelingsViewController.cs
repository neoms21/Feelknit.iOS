using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Feelknit.iOS.Model;
using System.Threading.Tasks;
using Feelknit.iOS.Helpers;
using Newtonsoft.Json;
using Feelknit.iOS.Views;
using System.Drawing;
using System.Linq;
using Feelknit.iOS.Controllers;

namespace Feelknit.iOS
{
	partial class CurrentFeelingsViewController : UITableViewController
	{
		private IEnumerable<Feeling> _feelings;

		private LoadingOverlay _loadingOverlay;

		event GetUserFeelingsDelegate GetFeelings;


		public CurrentFeelingsViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds

			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft
				|| UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight)
			{
				bounds.Size = new SizeF(bounds.Size.Height, bounds.Size.Width);
			}

			// show the loading overlay on the UI thread using the correct orientation sizing
			_loadingOverlay = new LoadingOverlay(bounds, "Getting feelings..");
			this.View.Add(_loadingOverlay);

			GetFeelings += async () =>
			{
				_feelings = await GetCurrentFeelings();

				this.TableView.Source = new RelatedFeelingsTableViewSource(_feelings.ToList(), OnRowSelection);
				this.TableView.ReloadData();

				_loadingOverlay.Hide();
			};


			GetFeelings.Invoke();
			//this.TableView.Source = new 
		}

		private async Task<IEnumerable<Feeling>> GetCurrentFeelings()
		{
			var client = new JsonHttpClient(string.Format(UrlHelper.CURRENT, ApplicationHelper.UserName));
			var result = await client.GetRequest();

			_feelings = JsonConvert.DeserializeObject<IEnumerable<Feeling>>(result);

			return _feelings;
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


		internal delegate void GetUserFeelingsDelegate();
	}
}
