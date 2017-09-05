using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using airbornefrs.Data.EcommerceEF;
using airbornefrs.Models;

namespace airbornefrs.Areas.Shopping.Controllers
{
    public class ProductController : Controller
    {
        // GET: Shopping/Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Detail(int? id)
        {
            ShoppingcartModel model = new ShoppingcartModel();
            return View(model.GetItemDetail(id));
            //return View();
        }
        public ActionResult Buynow()
        {
            return View();
        }
    }
}