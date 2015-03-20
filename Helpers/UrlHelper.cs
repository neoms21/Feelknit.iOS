using System;

namespace Feelknit.iOS
{
	public  class UrlHelper
	{
		private static bool onEmulator{ get { return MonoTouch.ObjCRuntime.Runtime.Arch == MonoTouch.ObjCRuntime.Arch.SIMULATOR; } }

		private static  string BASE_URL_EMULATOR = "http://192.168.0.6/FeelKnitService/";
		private static  string BASE_URL = "http://feelknitservice.apphb.com/";
		public static  string COMMENTS = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "Comments";
		public static  string FEELINGS = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "feelings";
		public static  string USER_VERIFY = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "Users/login";
		public static  string USER_KEY = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "Users/devicetoken";
		public static  string CLEAR_USER_KEY = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "Users/clearkey";
		public static  string USER = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "Users";
		public static  string USERNAME = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "feelings/username/";
		public static  string INCREASE_SUPPORT = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "feelings/increasesupport";
		public static  string DECREASE_SUPPORT = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "feelings/decreasesupport";
		public static  string COMMENTSFEELING = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "feelings/comments/%s";
		public static  string EMAILREPORT = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "email/report";
		public static  string USER_FEELINGS = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "feelings/username/{0}";
		public static  string GET_FEELS = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "feelings/getfeels";
		public static  string CURRENT = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "feelings/current";
		public static  string SAVE_USER = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "users/saveuser";
		public static  string COMMENTS_FEELINGS = (onEmulator ? BASE_URL_EMULATOR : BASE_URL) + "feelings/comments/{0}";
	}
}

