using System;
using System.Collections.Generic;
using Feelknit.Model;

namespace Feelknit.iOS.Model
{
    public class Feeling
    {
        private double _latitude;
        private double _longitude;
        private string _feelingText;

        public Feeling()
        {
            Location = new double[2];
            Comments = new List<Comment>();
            SupportUsers = new List<string>();
        }

        public string Id { get; set; }

        public string FeelingText
        {
            get { return _feelingText; }
            set
            {
                _feelingText = value;
				FeelingTextLower = value!=null ? value.ToLower():string.Empty;
            }
        }

        public string FeelingTextLower { get; set; }

        public DateTime FeelingDate { get; set; }

        public string UserName { get; set; }

        public string Reason { get; set; }

        public string Action { get; set; }

        public int SupportCount { get; set; }

        public double[] Location { get; set; }

        public IList<Comment> Comments { get; set; }

        public IList<string> SupportUsers { get; set; }

        public bool IsReported { get; set; }

        public bool IsCurrentFeeling { get; set; }

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                Location[1] = value;
                _latitude = value;
            }
        }

        public double Longitude
        {
            get { return _longitude; }
            set
            {
                Location[0] = value;
                _longitude = value;
            }
        }


        public String GetFeelingFormattedText(String pronoun)
        {
            var p = pronoun.Equals("I") ? "am" : "is";

            if (string.IsNullOrEmpty(Reason) && string.IsNullOrEmpty(Action))
				return String.Format("{0} {1} feeling {2}",pronoun, IsFirstFeeling ? p : "was", FeelingText);
            if (string.IsNullOrEmpty(Reason) && !string.IsNullOrEmpty(Action))
				return String.Format("{0} {1} feeling {2} so {3}",pronoun, IsFirstFeeling ? p : "was", FeelingText, Action);
            if (!string.IsNullOrEmpty(Reason) && string.IsNullOrEmpty(Action))
				return String.Format("{0} {1} feeling {2} because {3}",pronoun, IsFirstFeeling ? p : "was", FeelingText, Reason);

			return String.Format("{0} {1} feeling {2} because {3} so {4}",pronoun, IsFirstFeeling ? p : "was", FeelingText, Reason, Action);
        }

        public bool IsFirstFeeling { get; set; }
    }
}