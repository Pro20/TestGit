using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using airbornefrs.Data.PurchasedProduct;

namespace airbornefrs.Models
{
    public class PurchasedProductModel
    {
         public airbornefrs.Entities.PurchasedProduct purchasedproductData { get; set; }
        public string reCaptchaResponse { get; set; }
        public PurchasedProductModel()
        {
            purchasedproductData = new airbornefrs.Entities.PurchasedProduct();
        }

        public string GetJsonData()
        {
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(this);
        }

        public PurchasedProductRepository.BoolResponse SaveDetails()
        {
            if (string.IsNullOrEmpty(purchasedproductData.Name)) purchasedproductData.Name = "";
            using (PurchasedProductRepository objPPR  = new PurchasedProductRepository())
            {
                PurchasedProductRepository.BoolResponse saveStatus = objPPR.SaveContactus(this.purchasedproductData);
                if (saveStatus.status == 0)
                {
                    airbornefrs.Data.AppEmail.AppEmails appEmail = new airbornefrs.Data.AppEmail.AppEmails(airbornefrs.Data.AppEmail.AppEmails.EmailSettingIDs.purchasedproduct);
                    string body = appEmail.MAIL.Body;

                    body = body.Replace("#Name#", purchasedproductData.Name).Replace("#Email#", purchasedproductData.Email).Replace("#CompanyName#", purchasedproductData.Companyname);
                    body = body.Replace("#Product#", purchasedproductData.product).Replace("#IMEI#", purchasedproductData.IMEI).Replace("#Location#", purchasedproductData.Location).Replace("#Howcanwehelp#", purchasedproductData.Howcanwehelp);
                    
                    appEmail.MAIL.Body = body;
                    appEmail.MAIL.Subject = appEmail.MAIL.Subject + " : " + purchasedproductData.Name;

                    airbornefrs.Framework.BoolResponse response = appEmail.FireEmail();
                    saveStatus.status = response.status;
                    saveStatus.message = response.message;
                }
                return saveStatus;
            }

        }
    }
}