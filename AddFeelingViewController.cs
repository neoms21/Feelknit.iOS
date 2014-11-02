using System;
using MonoTouch.UIKit;
using SlideDownMenu;
using System.Drawing;
using System.Collections.Generic;
using Feelknit.iOS;
using MonoTouch.Foundation;
using Feelknit.iOS.Model;
using Newtonsoft.Json;
using Feelknit.iOS.Views;
using System.Linq;

namespace Feelknit
{
	partial class AddFeelingViewController : UIViewController
	{
		string _feelingText;
		private LoadingOverlay _loadingOverlay;

	
		public AddFeelingViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationController.NavigationBarHidden = false;
			#region View lifecycle
			FeelingsTableView.Hidden = true;
				
			string[] tableItems = new string[] {"Sad","Angry","Worried"};
			FeelingsTableView.Source = new FeelingsTableViewSources (tableItems, SetSelection);
			FeelingsTableView.Hidden = true;
			FeelingsTableView.RowHeight = 30;
			SelectFeelingButton.TouchUpInside += (object sender, EventArgs e) => {
				FeelingsTableView.Hidden = false;

			};
			_loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds, "Sharing..");
			ShareFeelingButton.TouchUpInside += (object sender, EventArgs e) => {
				this.View.Add(_loadingOverlay);
				SaveFeeling();

			};
				
		}
		private async void SaveFeeling(){
			var client = new JsonHttpClient(UrlHelper.FEELINGS);
			var feeling = new Feeling {
				UserName = ApplicationHelper.UserName,
				FeelingText = _feelingText,
				Reason = ReasonText.Text
			};
			var result = await client.PostRequest (feeling);

			var feelings = JsonConvert.DeserializeObject<IEnumerable<Feeling>> (result);
			_loadingOverlay.Hide ();

			var relatedFeelingsViewController =
				this.Storyboard.InstantiateViewController("RelatedFeelingsViewController") as RelatedFeelingsViewController;
			if (relatedFeelingsViewController != null)
			{
				relatedFeelingsViewController.Feeling = feeling;
				relatedFeelingsViewController.RelatedFeelings = feelings.ToList();
				this.NavigationController.PushViewController(relatedFeelingsViewController, true);
			}
//			var alert = new UIAlertView("Saved", "Feeling Saved", null, "OK", null);
//			alert.Show();
		}


		#endregion

		public void SetSelection (string value)
		{

			SelectFeelingButton.SetTitle (value, UIControlState.Normal);
			_feelingText = value;
			FeelingsTableView.Hidden = true;
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{

			base.TouchesEnded (touches,evt);
			FeelingsTableView.Hidden = true;
		}
	    
	}
}
