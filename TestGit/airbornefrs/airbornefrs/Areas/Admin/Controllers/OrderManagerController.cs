using airbornefrs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace airbornefrs.Areas.Admin.Controllers
{
    [Authorize]
    public class OrderManagerController : Controller
    {
        // GET: Admin/OrderManager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(string id)
        {
            ShoppingcartModel model = new ShoppingcartModel();
            return View(model.GetOrderDetails(id));
        }
    }
}