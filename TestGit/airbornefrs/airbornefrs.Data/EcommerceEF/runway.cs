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
    
    public partial class runway
    {
        public long id { get; set; }
        public Nullable<long> airport_ref { get; set; }
        public string airport_ident { get; set; }
        public string length_ft { get; set; }
        public string width_ft { get; set; }
        public string surface { get; set; }
        public string lighted { get; set; }
        public string closed { get; set; }
        public string le_ident { get; set; }
        public string le_latitude_deg { get; set; }
        public string le_longitude_deg { get; set; }
        public string le_elevation_ft { get; set; }
        public string le_heading_degT { get; set; }
        public string le_displaced_threshold_ft { get; set; }
        public string he_ident { get; set; }
        public string he_latitude_deg { get; set; }
        public string he_longitude_deg { get; set; }
        public string he_elevation_ft { get; set; }
        public string he_heading_degT { get; set; }
        public string he_displaced_threshold_ft { get; set; }
    
        public virtual airport airport { get; set; }
    }
}
