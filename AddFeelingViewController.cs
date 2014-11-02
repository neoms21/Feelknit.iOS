using System;
using MonoTouch.UIKit;
using SlideDownMenu;
using System.Drawing;
using System.Collections.Generic;
using Feelknit.iOS;
using MonoTouch.Foundation;
using Feelknit.iOS.Model;

namespace Feelknit
{
	partial class AddFeelingViewController : UIViewController
	{
		string _feelingText;

	
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

			ShareFeelingButton.TouchUpInside += (object sender, EventArgs e) => {

				SaveFeeling();

			};
				
		}
		private void SaveFeeling(){
			var client = new JsonHttpClient(UrlHelper.FEELINGS);
			var result = client.PostRequest(new Feeling{ UserName = ApplicationHelper.UserName, FeelingText =_feelingText, Reason = ReasonText.Text});


			var alert = new UIAlertView("msg", "Feeling Saved", null, "OK", null);
			alert.Show();
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
