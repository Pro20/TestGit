using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airbornefrs.Entities
{
    public class Rma
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string IMEI { get; set; }
        public string DID { get; set; }
        public string ProductReqRMA { get; set; }
        public string PersonalOrBuzz { get; set; }
    }
}