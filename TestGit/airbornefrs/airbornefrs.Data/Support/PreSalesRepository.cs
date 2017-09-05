using airbornefrs.Data.DBML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airbornefrs.Data.Support
{
    public class PreSalesRepository:IDisposable
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

        public BoolResponse SavePockup(airbornefrs.Entities.SupportPreSales objPreSales)
        {

            BoolResponse rslt = (from o in objDB.Support_SupportTicket_PreSales_Save_SP(objPreSales.Name, objPreSales.Email, objPreSales.CompanyName, objPreSales.BuisnessType, objPreSales.OtherProduct, objPreSales.Use, objPreSales.Location, objPreSales.AdditionalInfo)
                                 select new BoolResponse { Id = o.ID, status = o.ErrorCode }).FirstOrDefault();
            return rslt;
        }
    }
}