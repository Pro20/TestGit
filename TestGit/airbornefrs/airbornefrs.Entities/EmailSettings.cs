using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airbornefrs.Framework
{
    public class EmailSettings
    {

        public int EmailSettingID { get; set; }
        public string DefaultTo { get; set; }
        public string DefaultCC { get; set; }
        public string DefaultBCC { get; set; }
        public string SMTP { get; set; }
        public string Port { get; set; }
        public string FromEmailAddress { get; set; }
        public string FromName { get; set; }
        public string FromEmailPassword { get; set; }

        public bool EnableSSL { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }

        public bool IsBodyHtml { get; set; }
    }
}