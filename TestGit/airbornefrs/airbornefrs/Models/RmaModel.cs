using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using airbornefrs.Data.Rma;

namespace airbornefrs.Models
{
    public class RmaModel
    {
        public airbornefrs.Entities.Rma rmaData { get; set; }

         public string reCaptchaResponse { get; set; }
         public RmaModel()
        {
            rmaData = new airbornefrs.Entities.Rma();
        }

        public string GetJsonData()
        {
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(this);
        }

        public RmaRepository.BoolResponse SaveDetails()
        {
            if (string.IsNullOrEmpty(rmaData.Name)) rmaData.Name = "";
            using (RmaRepository objRMA = new RmaRepository())
            {
                RmaRepository.BoolResponse saveStatus = objRMA.SaveContactus(this.rmaData);
                if (saveStatus.status == 0)
                {
                    airbornefrs.Data.AppEmail.AppEmails appEmail = new airbornefrs.Data.AppEmail.AppEmails(airbornefrs.Data.AppEmail.AppEmails.EmailSettingIDs.rma);
                    string body = appEmail.MAIL.Body;

                    body = body.Replace("#Name#", rmaData.Name).Replace("#Email#", rmaData.Email).Replace("#ZipCode#", rmaData.Zipcode);
                    body = body.Replace("#Country#", rmaData.Country).Replace("#IMEI#", rmaData.IMEI).Replace("#DID#", rmaData.DID);
                    body = body.Replace("#ProductRequiringRMA#", rmaData.ProductReqRMA).Replace("#PersonalorBusiness#", rmaData.PersonalOrBuzz);
                    
                    appEmail.MAIL.Body = body;
                    appEmail.MAIL.Subject = appEmail.MAIL.Subject + " : " + rmaData.Name;

                    airbornefrs.Framework.BoolResponse response = appEmail.FireEmail();
                    saveStatus.status = response.status;
                    saveStatus.message = response.message;
                }
                return saveStatus;
            }

        }
    }
}