//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace airbornefrs.Data.EcommerceEF
{
    using System;
    using System.Collections.Generic;
    
    public partial class airport_frequencies
    {
        public long id { get; set; }
        public Nullable<long> airport_ref { get; set; }
        public string airport_ident { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string frequency_mhz { get; set; }
    
        public virtual airport airport { get; set; }
    }
}
