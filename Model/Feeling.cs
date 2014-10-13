﻿using System;
using System.Collections.Generic;

namespace Feelknit.Model
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
                FeelingTextLower = value.ToLower();
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

    }
}