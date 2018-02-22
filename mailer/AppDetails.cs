﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Function
{
    class AppDetails
    {
        public string AppID { get; set; }
        public string AppName { get; set; }
        public string AlertType { get; set; }
        public string Message { get; set; }
        public List<string> ToAddress { get; set; }
        public AppDetails()
        {
            ToAddress = new List<string>();
        }
    }
}
