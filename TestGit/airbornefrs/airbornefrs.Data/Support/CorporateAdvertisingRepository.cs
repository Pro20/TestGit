using airbornefrs.Data.DBML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airbornefrs.Data.Support
{
    public class CorporateAdvertisingRepository:IDisposable
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

        public BoolResponse SavePockup(airbornefrs.Entities.CorporateAdvertising objCorporateAdvertising)
        {

            BoolResponse rslt = (from o in objDB.Support_ContactUs_CorporateAdvertising_Save_SP(objCorporateAdvertising.Name, objCorporateAdvertising.Email, objCorporateAdvertising.CompanyName, objCorporateAdvertising.Telephone, objCorporateAdvertising.Teletype, objCorporateAdvertising.BuisnessType, objCorporateAdvertising.Location, objCorporateAdvertising.AdditionalInfo)
                                 select new BoolResponse { Id = o.ID, status = o.ErrorCode }).FirstOrDefault();
            return rslt;
        }
    }
}