using airbornefrs.Data.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airbornefrs.Models
{
    public class SupportCorporateAdvertising
    {
        public airbornefrs.Entities.CorporateAdvertising CorporateAdvertisingData { get; set; }
        public string reCaptchaResponse { get; set; }
        public SupportCorporateAdvertising()
        {
            CorporateAdvertisingData = new airbornefrs.Entities.CorporateAdvertising();
        }
        public string GetJsonData()
        {
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(this);
        }

        public CorporateAdvertisingRepository.BoolResponse SaveDetails()
        {
            if (string.IsNullOrEmpty(CorporateAdvertisingData.Name)) CorporateAdvertisingData.Name = "";

            using (CorporateAdvertisingRepository objSCA = new CorporateAdvertisingRepository())
            {
                CorporateAdvertisingRepository.BoolResponse saveStatus = objSCA.SavePockup(this.CorporateAdvertisingData);
                if (saveStatus.status == 0)
                {
                    airbornefrs.Data.AppEmail.AppEmails appEmail = new airbornefrs.Data.AppEmail.AppEmails(airbornefrs.Data.AppEmail.AppEmails.EmailSettingIDs.CorporateAdvertising);
                    string body = appEmail.MAIL.Body;
                    body = body.Replace("#Name#", CorporateAdvertisingData.Name).Replace("#Email#", CorporateAdvertisingData.Email).Replace("#CompanyName#", CorporateAdvertisingData.CompanyName).Replace("#Telephone#", CorporateAdvertisingData.Telephone).Replace("#Teletype#", CorporateAdvertisingData.Teletype).
                        Replace("#Location#", CorporateAdvertisingData.Location).Replace("#Primary Type Of Buisness#", CorporateAdvertisingData.BuisnessType).Replace("#Howcanwehelp#", CorporateAdvertisingData.AdditionalInfo);

                    appEmail.MAIL.Body = body;
                    appEmail.MAIL.Subject = appEmail.MAIL.Subject + " : " + CorporateAdvertisingData.Name;

                    airbornefrs.Framework.BoolResponse response = appEmail.FireEmail();
                    saveStatus.status = response.status;
                    saveStatus.message = response.message;
                }
                return saveStatus;
            }

        }
    }
}