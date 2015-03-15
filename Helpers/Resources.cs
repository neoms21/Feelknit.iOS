using System;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace Feelknit.iOS
{
	public static class Resources
	{

		public static int temp = 0;
		public static UIColor ListBackgroundColor =UIColor.Clear.FromHex(0xCCFFFF);
		public static UIColor LoginButtonColor =UIColor.Clear.FromHex(0x2659A1);
		public static UIColor TextColor =UIColor.Clear.FromHex(0xFFFFFF);
		public static UIColor ButtonColor =UIColor.Clear.FromHex(0x629E36);
		public static UIColor LightButtonColor =UIColor.Clear.FromHex(0x2B6600);
		public static UIColor MainBackgroundColor =UIColor.Clear.FromHex(0xA4C3DC);
		public static UIColor DisabledColor =UIColor.Clear.FromHex(0x6F7376);
//			<color name="loginLabel">#2659A1</color>
//			<color name="textColor">#FFFFFF</color>
//			<color name="buttonColor">#629E36</color>
//			<color name="lightButtonColor">#2B6600</color>
//			<color name="whiteBg">#FFFFFF</color>
//			<color name="mainBg">#A4C3DC</color>
//			<color name="greyColor">#6F7376</color>

		public static IList<string> Avatars = new List<string> {"agnes", "blackwidow", "chef", "evilminion", "girl1",
			"girl2", "girl3", "gru", "hulk", "ironman", "kungfuminion", "thor"
		};

		public static List<LeftDrawerItem> LeftDrawerItems = new List<LeftDrawerItem>
		{
			new LeftDrawerItem("Profile","notifications",101),
			new LeftDrawerItem("Current Feelings","userdrawer",102),
			new LeftDrawerItem("My Feelings","userdrawer",103),
			new LeftDrawerItem("Comments","comments",104),
			new LeftDrawerItem("Related Feelings","userdrawer", 105),
			new LeftDrawerItem("About","settings",106),
		};
	}
}

