using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using airbornefrs.Data.EcommerceEF;
using airbornefrs.Models;
using System.IO;
using static airbornefrs.Models.ShoppingcartModel;

namespace airbornefrs.Areas.Admin.Controllers
{
    [Authorize]
    public class StoreManagerController : Controller
    {
        // GET: Admin/StoreManager
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadFile()
        {
            string Message, fileName;
            Message = fileName = string.Empty;
            bool flag = false;
            if (Request.Files != null)
            {
                var file = Request.Files[0];
                fileName = file.FileName;
                try
                {
                    file.SaveAs(Path.Combine(Server.MapPath("~/images/Products"), fileName));
                    Message = "File uploaded";
                    flag = true;
                }
                catch (Exception)
                {
                    Message = "File upload failed! Please try again";
                }

            }
            return new JsonResult { Data = new { Message = Message, Status = flag } };
        }


        public ActionResult Edit(int? id)
        {
            ShoppingcartModel model = new ShoppingcartModel();

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "In stock", Value = "0" });
            items.Add(new SelectListItem { Text = "Out of stock", Value = "1" });

            ViewBag.ListItemStatus = new SelectList(items.Select(x => new SelectListItem()
            {
                Text = x.Text.ToString(),
                Value = x.Value
            }), "Value", "Text");

            return View(model.GetItemDetail(id));
            //return View();
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, itemDetailsDataContract model)
        {
            var id = Convert.ToInt32(model.Item_ID);
            try
            {
                string Message, fileName = string.Empty;
                if (file != null)
                {
                    fileName = file.FileName;
                    try
                    {
                        file.SaveAs(Path.Combine(Server.MapPath("~/images/Products"), fileName));
                        Message = "File uploaded";
                    }
                    catch (Exception)
                    {
                        Message = "File upload failed! Please try again";
                    }
                }

                db_AirborneEntities OEM = new db_AirborneEntities();
                var GetProductDetails = OEM.ItemDetails.Where(x => x.Item_ID == id).FirstOrDefault();
                GetProductDetails.Item_code = model.Item_code;
                GetProductDetails.Item_Name = model.Item_Name;
                GetProductDetails.Item_Price = model.Item_Price;
                GetProductDetails.Image_Name = (!string.IsNullOrEmpty(fileName)) ? fileName : model.Image_Name;
                GetProductDetails.Status = model.Status;
                GetProductDetails.Description = model.Description;
                GetProductDetails.Short_Description = model.Short_Description;
                GetProductDetails.ShippingCost = model.Shipping_Price;
                GetProductDetails.TaxAmount = model.Tax_Amount;
                OEM.SaveChanges();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "StoreManager", new { area = "Admin" });
            }
            return RedirectToAction("Index", "StoreManager", new { area = "Admin" });
        }

        public ActionResult DeleteProduct(int? id)
        {
            if (id != null && id > 0)
            {
                db_AirborneEntities OEM = new db_AirborneEntities();
                var GetProductDetails = OEM.ItemDetails.Where(x => x.Item_ID == id).FirstOrDefault();
                if (GetProductDetails != null)
                    OEM.ItemDetails.Remove(GetProductDetails);
                OEM.SaveChanges();
            }
            return RedirectToAction("Index", "StoreManager", new { area = "Admin" });

        }

    }
}
