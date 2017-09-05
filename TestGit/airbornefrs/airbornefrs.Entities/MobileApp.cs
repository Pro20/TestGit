using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airbornefrs.Entities
{
    public class MobileApp
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Companyname { get; set; }
        public string Os { get; set; }
        public string PrimaryPlat { get; set; }
        public string Phone { get; set; }
        public string Use { get; set; }
        public string Howcanhelp { get; set; }
    }
}