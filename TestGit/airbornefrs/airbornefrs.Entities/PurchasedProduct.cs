using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airbornefrs.Entities
{
    public class PurchasedProduct
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Companyname { get; set; }
        public string product { get; set; }
        public string IMEI { get; set; }
        public string Location { get; set; }
        public string Howcanwehelp  { get; set; }
    }
}