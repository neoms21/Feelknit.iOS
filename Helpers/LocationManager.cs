using System;
using MonoTouch.CoreLocation;
using MonoTouch.UIKit;

namespace Feelknit.iOS
{
	public class LocationManager
	{
		protected CLLocationManager locMgr;
		public event EventHandler<LocationUpdatedEventArgs> LocationUpdated = delegate { };

		public LocationManager (){
			this.locMgr = new CLLocationManager();
			// iOS 8 has additional permissions requirements
			//
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				locMgr.RequestWhenInUseAuthorization (); // works in background
				//locMgr.RequestWhenInUseAuthorization (); // only in foreground
			}
		}

		public CLLocationManager LocMgr{
			get { return this.locMgr; }
		}

		public void StartLocationUpdates()
		{
			if (CLLocationManager.LocationServicesEnabled) {
				//set the desired accuracy, in meters
				LocMgr.DesiredAccuracy = 1;
				LocMgr.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
				{
					// fire our custom Location Updated event
					LocationUpdated (this, new LocationUpdatedEventArgs (e.Locations [e.Locations.Length - 1]));
				};
				LocMgr.StartUpdatingLocation();
			}
		}

	}

	public class LocationUpdatedEventArgs : EventArgs
	{
		CLLocation location;

		public LocationUpdatedEventArgs(CLLocation location)
		{
			this.location = location;
		}

		public CLLocation Location
		{
			get { return location; }
		}
	}
}

