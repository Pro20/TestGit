using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using airbornefrs.Data.DBML;

namespace airbornefrs.Data.MobileApp
{
    public class MobileAppRepository : IDisposable
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

        public BoolResponse SaveContactus(airbornefrs.Entities.MobileApp mobileapp)
        {
            BoolResponse rslt = (from o in objDB.Support_SupportTicket_MobileApp_Save_SP(mobileapp.Name, mobileapp.Email,
                                     mobileapp.Companyname, mobileapp.Os, mobileapp.PrimaryPlat, mobileapp.Phone, mobileapp.Use, mobileapp.Howcanhelp)
                                 select new BoolResponse { Id = o.ID, status = o.ErrorCode }).FirstOrDefault();


            return rslt;
        }
    }
}