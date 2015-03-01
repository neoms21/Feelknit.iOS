﻿using System.Collections.Generic;
using MonoTouch.Foundation;

namespace Feelknit.iOS.Helpers
{
	public static class ApplicationHelper
	{


		public static string UserName { get { return NSUserDefaults.StandardUserDefaults.StringForKey ("UserName"); } 
			set { NSUserDefaults.StandardUserDefaults.SetString (value, "UserName"); } }

		public static string AuthorizationToken { get { return NSUserDefaults.StandardUserDefaults.StringForKey ("AuthToken"); } 
			set { NSUserDefaults.StandardUserDefaults.SetString (value, "AuthToken"); } }

		public static string Avatar { get { return NSUserDefaults.StandardUserDefaults.StringForKey ("Avatar"); } 
			set { NSUserDefaults.StandardUserDefaults.SetString (value, "Avatar"); } }


		public static string ApnsToken { get { return NSUserDefaults.StandardUserDefaults.StringForKey ("ApnsToken"); } 
			set { NSUserDefaults.StandardUserDefaults.SetString (value, "ApnsToken"); } }


		public static string FeelTexts { get { return NSUserDefaults.StandardUserDefaults.StringForKey ("FeelTexts"); } 
			set { NSUserDefaults.StandardUserDefaults.SetString (value, "FeelTexts"); } }

		public static string EmailAddress { get { return NSUserDefaults.StandardUserDefaults.StringForKey ("EmailAddress"); } 
			set { NSUserDefaults.StandardUserDefaults.SetString (value, "EmailAddress"); } }


		public static bool IsAuthenticated { get { return NSUserDefaults.StandardUserDefaults.BoolForKey ("IsAuthenticated"); } 
			set { NSUserDefaults.StandardUserDefaults.SetBool (value, "IsAuthenticated"); } }


//
//		public static string DeviceToken{ get; set; }

//		public static IList<string> Feelings { get; set; }

	}
}

