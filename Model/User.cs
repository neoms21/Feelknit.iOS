using System;

namespace Feelknit.Model
{
    public class User
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public string EmailAddress { get; set; }

        public string Key { get; set; }

		public string Avatar{ get; set; }
			
		public string IosKey { get; set; }

        public bool IsTemporary { get; set; }

        public DateTime? PasswordExpiryTime { get; set; }
    }
}