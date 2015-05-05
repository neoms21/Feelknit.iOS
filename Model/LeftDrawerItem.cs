using System;

namespace Feelknit.iOS
{
	public class LeftDrawerItem
	{
		public LeftDrawerItem (string text, string image, Container container)
		{
			Text = text;
			Image = image;
			Container = container;
		}

		public string Text{ get; set; }

		public string Image{ get; set; }

		public Container Container{ get; set; }
	}

	public class Container
	{
	
		public string Name{ get; set; }

		public object Data{ get; set; }
	}
}

