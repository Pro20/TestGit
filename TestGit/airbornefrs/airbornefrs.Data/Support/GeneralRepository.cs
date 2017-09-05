using airbornefrs.Data.DBML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airbornefrs.Data.Support
{
    public class GeneralRepository : IDisposable
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

        public BoolResponse SavePockup(airbornefrs.Entities.ContactUsGeneral objContactUsGeneral)
        {

            BoolResponse rslt = (from o in objDB.Support_ContactUs_General_Save_SP(objContactUsGeneral.Name, objContactUsGeneral.Email, objContactUsGeneral.CompanyName, objContactUsGeneral.ReasonInquiry, objContactUsGeneral.Product, objContactUsGeneral.IMEI, objContactUsGeneral.AdditionalInfo)
                                 select new BoolResponse { Id = o.ID, status = o.ErrorCode }).FirstOrDefault();
            return rslt;
        }
    }
}