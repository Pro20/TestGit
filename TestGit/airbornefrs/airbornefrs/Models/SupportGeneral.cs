using airbornefrs.Data.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airbornefrs.Models
{
    public class SupportGeneral
    {
        public airbornefrs.Entities.ContactUsGeneral contactData { get; set; }
        public string reCaptchaResponse { get; set; }
        public SupportGeneral()
        {
            contactData = new airbornefrs.Entities.ContactUsGeneral();
        }
        public string GetJsonData()
        {
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(this);
        }

        public GeneralRepository.BoolResponse SaveDetails()
        {
            if (string.IsNullOrEmpty(contactData.Name)) contactData.Name = "";

            using (GeneralRepository objCR = new GeneralRepository())
            {
                GeneralRepository.BoolResponse saveStatus = objCR.SavePockup(this.contactData);
                if (saveStatus.status == 0)
                {
                    airbornefrs.Data.AppEmail.AppEmails appEmail = new airbornefrs.Data.AppEmail.AppEmails(airbornefrs.Data.AppEmail.AppEmails.EmailSettingIDs.contactus);
                    string body = appEmail.MAIL.Body;
                    body = body.Replace("#Name#", contactData.Name).Replace("#Email#", contactData.Email).Replace("#CompanyName#", contactData.CompanyName).Replace("#ReasonInquiry#", contactData.ReasonInquiry).Replace("#Product#", contactData.Product).Replace("#IMEI#", contactData.IMEI).Replace("#AdditionalInfo#", contactData.AdditionalInfo);

                    appEmail.MAIL.Body = body;
                    appEmail.MAIL.Subject = appEmail.MAIL.Subject + " : " + contactData.Name;

                    airbornefrs.Framework.BoolResponse response = appEmail.FireEmail();
                    saveStatus.status = response.status;
                    saveStatus.message = response.message;
                }
                return saveStatus;
            }

        }
    }
}