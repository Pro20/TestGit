using airbornefrs.Data.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airbornefrs.Models
{
    public class SupportPreSales
    {
        public airbornefrs.Entities.SupportPreSales PreSalesData { get; set; }
        public string reCaptchaResponse { get; set; }
        public SupportPreSales()
        {
            PreSalesData = new airbornefrs.Entities.SupportPreSales();
        }
        public string GetJsonData()
        {
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(this);
        }

        public PreSalesRepository.BoolResponse SaveDetails()
        {
            if (string.IsNullOrEmpty(PreSalesData.Name)) PreSalesData.Name = "";

            using (PreSalesRepository objPS = new PreSalesRepository())
            {
                PreSalesRepository.BoolResponse saveStatus = objPS.SavePockup(this.PreSalesData);
                if (saveStatus.status == 0)
                {
                    airbornefrs.Data.AppEmail.AppEmails appEmail = new airbornefrs.Data.AppEmail.AppEmails(airbornefrs.Data.AppEmail.AppEmails.EmailSettingIDs.PreSales);
                    string body = appEmail.MAIL.Body;
                    body = body.Replace("#Name#", PreSalesData.Name).Replace("#Email#", PreSalesData.Email).Replace("#CompanyName#", PreSalesData.CompanyName).Replace("#PrimaryTypeOfBuisness#", PreSalesData.BuisnessType).Replace("#OtherProductIntrest#", PreSalesData.OtherProduct).Replace("#Use#", PreSalesData.Use).Replace("#Location#", PreSalesData.Location).Replace("#Howcanwehelp#", PreSalesData.AdditionalInfo);

                    appEmail.MAIL.Body = body;
                    appEmail.MAIL.Subject = appEmail.MAIL.Subject + " : " + PreSalesData.Name;

                    airbornefrs.Framework.BoolResponse response = appEmail.FireEmail();
                    saveStatus.status = response.status;
                    saveStatus.message = response.message;
                }
                return saveStatus;
            }

        }
    }
}