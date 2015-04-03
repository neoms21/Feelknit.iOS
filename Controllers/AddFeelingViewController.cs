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
using MonoTouch.CoreLocation;

namespace Feelknit.iOS.Controllers
{
	partial class AddFeelingViewController : BaseController
	{
		string _feelingText;
		private LoadingOverlay _loadingOverlay;
	
		private double _latitude = 0.0;
		private double _longitude = 0.0;



		public AddFeelingViewController (IntPtr handle) : base (handle)
		{
			NavigationButtonVisible = false;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			ShareFeelingButton.BackgroundColor = Resources.LightButtonColor;
			ShareFeelingButton.SetTitleColor (UIColor.White, UIControlState.Normal);
			Manager.StartLocationUpdates ();
			Manager.LocationUpdated += HandleLocationChanged;
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
				Reason = ReasonText.Text,
				Latitude = _latitude,
				Longitude = _longitude
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

		public async void HandleLocationChanged (object sender, LocationUpdatedEventArgs e)
		{
			// Handle foreground updates
			CLLocation location = e.Location;

			_latitude = location.Coordinate.Latitude;
			_longitude = location.Coordinate.Longitude;


		}
	    
	}
}
