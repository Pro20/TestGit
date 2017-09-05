using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using airbornefrs.Data.DBML;

namespace airbornefrs.Data.Rma
{
    public class RmaRepository: IDisposable
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

        public BoolResponse SaveContactus(airbornefrs.Entities.Rma rma)
        {
            BoolResponse rslt = (from o in objDB.Support_RMA_Save_SP(rma.Name, rma.Email, rma.Zipcode, rma.Country, rma.IMEI, rma.DID, rma.ProductReqRMA, rma.PersonalOrBuzz)
                                 select new BoolResponse { Id = o.ID, status = o.ErrorCode }).FirstOrDefault();


            return rslt;
        }
    }
}