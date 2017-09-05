using airbornefrs.Data.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airbornefrs.Models
{
    public class SupportReseller
    {
        public airbornefrs.Entities.ContactReseller ResellerData { get; set; }
        public string reCaptchaResponse { get; set; }
        public SupportReseller()
        {
            ResellerData = new airbornefrs.Entities.ContactReseller();
        }
        public string GetJsonData()
        {
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(this);
        }

        public ContactResellerRepository.BoolResponse SaveDetails()
        {
            if (string.IsNullOrEmpty(ResellerData.Name)) ResellerData.Name = "";

            using (ContactResellerRepository objCR = new ContactResellerRepository())
            {
                ContactResellerRepository.BoolResponse saveStatus = objCR.SavePockup(this.ResellerData);
                if (saveStatus.status == 0)
                {
                    airbornefrs.Data.AppEmail.AppEmails appEmail = new airbornefrs.Data.AppEmail.AppEmails(airbornefrs.Data.AppEmail.AppEmails.EmailSettingIDs.Reseller);
                    string body = appEmail.MAIL.Body;

                    body = body.Replace("#Name#", ResellerData.Name).Replace("#Email#", ResellerData.Email).Replace("#CompanyName#", ResellerData.CompanyName).Replace("#Telephone#", ResellerData.Telephone).Replace("#Teletype#", ResellerData.TeleType).Replace("#PrimaryTypeOfBuisness#", ResellerData.BuisnessType).Replace("#Howcanwehelp#", ResellerData.AdditionalInfo);

                    appEmail.MAIL.Body = body;
                    appEmail.MAIL.Subject = appEmail.MAIL.Subject + " : " + ResellerData.Name;
                    
                    airbornefrs.Framework.BoolResponse response = appEmail.FireEmail();
                    saveStatus.status = response.status;
                    saveStatus.message = response.message;
                }
                return saveStatus;
            }

        }
    }
}