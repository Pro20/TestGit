using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using airbornefrs.Data.DBML;

namespace airbornefrs.Data.PurchasedProduct
{
    public class PurchasedProductRepository : IDisposable
    {
        public Boolean IsSaved;
        public class BoolResponse
        {
            public Guid? Id { get; set; }
            public int? status { get; set; }
            public string message { get; set; }
        }

        airbornefrsDataContext objDB = new airbornefrsDataContext();

        public void Dispose()
        {
            objDB = null;
        }

        public BoolResponse SaveContactus(airbornefrs.Entities.PurchasedProduct purchproduct)
        {
            BoolResponse rslt = (from o in objDB.Support_SupportTicket_PurchasedProduct_Save_SP( purchproduct.Name, purchproduct.Email,
                                     purchproduct.Companyname, purchproduct.product, purchproduct.IMEI, purchproduct.Location, purchproduct.Howcanwehelp)
                             select new BoolResponse { Id = o.ID, status = o.ErrorCode }).FirstOrDefault();
                                   
                                 
            return rslt;
        }
    }
}