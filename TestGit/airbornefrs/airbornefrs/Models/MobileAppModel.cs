using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using airbornefrs.Data.MobileApp;

namespace airbornefrs.Models
{
    public class MobileAppModel
    {
          public airbornefrs.Entities.MobileApp mobileappData { get; set; }
        public string reCaptchaResponse { get; set; }
        public MobileAppModel()
        {
            mobileappData = new airbornefrs.Entities.MobileApp();
        }

        public string GetJsonData()
        {
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(this);
        }

        public MobileAppRepository.BoolResponse SaveDetails()
        {
            if (string.IsNullOrEmpty(mobileappData.Name)) mobileappData.Name = "";
            using (MobileAppRepository objMAR = new MobileAppRepository())
            {
                MobileAppRepository.BoolResponse saveStatus = objMAR.SaveContactus(this.mobileappData);
                if (saveStatus.status == 0)
                {
                    airbornefrs.Data.AppEmail.AppEmails appEmail = new airbornefrs.Data.AppEmail.AppEmails(airbornefrs.Data.AppEmail.AppEmails.EmailSettingIDs.mobileapp);
                    string body = appEmail.MAIL.Body;

                    body = body.Replace("#Name#", mobileappData.Name).Replace("#Email#", mobileappData.Email).Replace("#CompanyName#", mobileappData.Companyname);
                    body = body.Replace("#Os#", mobileappData.Os).Replace("#PrimaryPlat#", mobileappData.PrimaryPlat).Replace("#Phone#", mobileappData.Phone).Replace("#Use#", mobileappData.Use).Replace("#Howcanhelp#", mobileappData.Howcanhelp);
                    appEmail.MAIL.Body = body;
                    appEmail.MAIL.Subject = appEmail.MAIL.Subject + " : " + mobileappData.Name;
                    airbornefrs.Framework.BoolResponse response = appEmail.FireEmail();
                    saveStatus.status = response.status;
                    saveStatus.message = response.message;
                }
                return saveStatus;
            }

        }
    }
}