using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airbornefrs.Entities
{
    public class ContactUsGeneral
    {
        public string Name { get; set; }        
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string ReasonInquiry { get; set; }
        public string Product { get; set; }
        public string IMEI { get; set; }
        public string AdditionalInfo { get; set; }
    }
}