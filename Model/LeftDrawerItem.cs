using System;

namespace Feelknit.iOS
{
	public class LeftDrawerItem
	{
		public LeftDrawerItem (string text, string image, string id)
		{
			Text = text;
			Image = image;
			Id = id;
		}

		public string Text{ get; set;}
		public string Image{get;set;}
		public string Id{get;set;}
	}
}

