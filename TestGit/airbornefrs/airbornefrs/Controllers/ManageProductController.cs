using Newtonsoft.Json;
using airbornefrs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Script.Services;
using static airbornefrs.Models.ShoppingcartModel;

namespace airbornefrs.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ManageProductController : ApiController
    {
        // GET api/<controller>
        [AllowAnonymous]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage Get()
        {
            ShoppingcartModel Shoppingmodel = new ShoppingcartModel();
            string json = JsonConvert.SerializeObject(Shoppingmodel.GetItemDetails());
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        // GET api/<controller>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Ordercontroller/GetOrdersList")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage GetOrdersList()
        {
            ShoppingcartModel Shoppingmodel = new ShoppingcartModel();
            string json = JsonConvert.SerializeObject(Shoppingmodel.GetOrdersList());
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        // GET api/<controller>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Ordercontroller/GetOrder/{val1}")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage GetOrderDetails(string val1)
        {
            ShoppingcartModel Shoppingmodel = new ShoppingcartModel();
            string json = JsonConvert.SerializeObject(Shoppingmodel.GetOrdersList());
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }



        // GET api/<controller>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Ext_CartController/GetItem")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage GetItem()
        {
            string json = JsonConvert.SerializeObject("Not in use");
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        // GET api/<controller>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Ext_CartController/GetItem/{val1}")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage GetItem(string val1)
        {
            ShoppingcartModel Shoppingmodel = new ShoppingcartModel();
            string json = JsonConvert.SerializeObject(Shoppingmodel.GetItemForAddtocart(val1));
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/ManageProduct/DeleteItems")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage DeleteItems(string[] ItemList)
        {
            ShoppingcartModel Shoppingmodel = new ShoppingcartModel();
            string json = JsonConvert.SerializeObject(Shoppingmodel.GetItemDelete(ItemList));
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [AllowAnonymous]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage Post(itemDetailsDataContract ItemDetails)
        {
            ShoppingcartModel Shoppingmodel = new ShoppingcartModel();
            string json = JsonConvert.SerializeObject(Shoppingmodel.addItemMaster(ItemDetails));
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}