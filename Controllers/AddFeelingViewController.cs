using System;
using System.Collections.Generic;
using System.Linq;
using Feelknit.iOS.Helpers;
using Feelknit.iOS.Model;
using Feelknit.iOS.TableViewSources;
using Feelknit.iOS.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Newtonsoft.Json;

namespace Feelknit.iOS.Controllers
{
	partial class AddFeelingViewController : BaseController
	{
		string _feelingText;
		private LoadingOverlay _loadingOverlay;

	
		public AddFeelingViewController () : base (null,null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationController.NavigationBarHidden = false;
			#region View lifecycle
			FeelingsTableView.Hidden = true;
				
			//string[] tableItems = new string[] {"Sad","Angry","Worried","Frustrated"};
			var feelings  = JsonConvert.DeserializeObject<List<string>>(ApplicationHelper.FeelTexts);
			FeelingsTableView.Source = new FeelingsTableViewSources (feelings, SetSelection);
			FeelingsTableView.Hidden = true;
			FeelingsTableView.RowHeight = 30;
			SelectFeelingButton.TouchUpInside += (sender, e) => {
				FeelingsTableView.Hidden = false;

			};
			_loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds, "Sharing..");
			ShareFeelingButton.TouchUpInside += (sender, e) => {
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
