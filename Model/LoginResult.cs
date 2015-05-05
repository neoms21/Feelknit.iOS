using System;

namespace Feelknit.iOS
{
	public class LoginResult
	{
		public bool IsLoginSuccessful{ get; set;}
		public string Avatar{get;set;}

		public string Token{ get; set;}

		public string UserEmail{ get; set;}
		public string Error {get;set;}
		public LoginResult ()
		{
		
		}
	}
}

