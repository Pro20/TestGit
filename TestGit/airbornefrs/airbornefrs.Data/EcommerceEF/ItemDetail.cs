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
    
    public partial class ItemDetail
    {
        public int Item_ID { get; set; }
        public string Item_code { get; set; }
        public string Item_Name { get; set; }
        public decimal Item_Price { get; set; }
        public string Image_Name { get; set; }
        public string Short_Description { get; set; }
        public string Description { get; set; }
        public string AddedBy { get; set; }
        public string Status { get; set; }
        public Nullable<decimal> ShippingCost { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}