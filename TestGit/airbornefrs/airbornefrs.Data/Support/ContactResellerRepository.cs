using airbornefrs.Data.DBML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airbornefrs.Data.Support
{
    public class ContactResellerRepository:IDisposable
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

        public BoolResponse SavePockup(airbornefrs.Entities.ContactReseller objContactReseller)
        {

            BoolResponse rslt = (from o in objDB.Support_ContactUs_Reseller_Save_SP(objContactReseller.Name, objContactReseller.Email, objContactReseller.CompanyName, objContactReseller.Telephone,objContactReseller.TeleType, objContactReseller.BuisnessType, objContactReseller.AdditionalInfo)
                                 select new BoolResponse { Id = o.ID, status = o.ErrorCode }).FirstOrDefault();
            return rslt;
        }
    }
}