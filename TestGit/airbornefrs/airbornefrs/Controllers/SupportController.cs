using airbornefrs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace airbornefrs.Controllers
{
    public class SupportController : Controller
    {
        // GET: Support
        public ActionResult BecomeReseller()
        {
            SupportReseller objSR = new SupportReseller();
            return View(objSR);
        }

        public JsonResult SaveResellerDetails(SupportReseller SupportReseller)
        {
            if (airbornefrs.Framework.Utility.ValidateCaptcha(SupportReseller.reCaptchaResponse).status == 1)
            {
                return new JsonResult { Data = SupportReseller.SaveDetails().status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = 3, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult CorporateAdvertising()
        {
            SupportCorporateAdvertising objSCA = new SupportCorporateAdvertising();
            return View(objSCA);

        }

        public JsonResult SaveCorporateAdvertisingDetails(SupportCorporateAdvertising SupportCorporateAdvertising)
        {
            if (airbornefrs.Framework.Utility.ValidateCaptcha(SupportCorporateAdvertising.reCaptchaResponse).status == 1)
            {
                return new JsonResult { Data = SupportCorporateAdvertising.SaveDetails().status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = 3, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult Eula()
        {
            return View();
        }

        public ActionResult LiveChat()
        {
            return View();
        }

        public ActionResult PreSale()
        {
            SupportPreSales objPS = new SupportPreSales();
            return View(objPS);

        }

        public JsonResult SavePreSalesDetails(SupportPreSales SupportPreSales)
        {
            if (airbornefrs.Framework.Utility.ValidateCaptcha(SupportPreSales.reCaptchaResponse).status == 1)
            {
                return new JsonResult { Data = SupportPreSales.SaveDetails().status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = 3, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult PurchasedProduct()
        {
            PurchasedProductModel objPPM = new PurchasedProductModel();

            return View(objPPM);
        }

        public JsonResult SavePurchasedDetails(PurchasedProductModel purchasedmodel)
        {
            if (airbornefrs.Framework.Utility.ValidateCaptcha(purchasedmodel.reCaptchaResponse).status == 1)
            {
                return new JsonResult { Data = purchasedmodel.SaveDetails().status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = 3, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult Rma()
        {
            RmaModel objRMA = new RmaModel();
            return View(objRMA);
        }

        public JsonResult SaveRmaDetails(RmaModel rmamodel)
        {
            if (airbornefrs.Framework.Utility.ValidateCaptcha(rmamodel.reCaptchaResponse).status == 1)
            {
                return new JsonResult { Data = rmamodel.SaveDetails().status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = 3, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult MobileApplication()
        {
            MobileAppModel objMA = new MobileAppModel();
            return View(objMA);
        }

        public JsonResult SaveMobileAppDetails(MobileAppModel mobileappmodel)
        {
            if (airbornefrs.Framework.Utility.ValidateCaptcha(mobileappmodel.reCaptchaResponse).status == 1)
            {
                return new JsonResult { Data = mobileappmodel.SaveDetails().status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = 3, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult General()
        {
            SupportGeneral objSG = new SupportGeneral();
            return View(objSG);
        }

        public JsonResult SaveGeneralDetails(SupportGeneral contactmodel)
        {
            if (airbornefrs.Framework.Utility.ValidateCaptcha(contactmodel.reCaptchaResponse).status == 1)
            {
                return new JsonResult { Data = contactmodel.SaveDetails().status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = 3, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult TestPage()
        {
            return View();
        }

    }
}