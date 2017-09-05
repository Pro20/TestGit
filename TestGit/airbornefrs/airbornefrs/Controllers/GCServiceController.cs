
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Services;
using Newtonsoft.Json;
using airbornefrs.Models;

namespace airbornefrs.Controllers
{
    public class GCServiceController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        //GET api/<controller>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Navg_Servicecontroller/GetAirportList")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage GetAirportList(string aiportname)
        {
            GCServiceModels Servicemodel = new GCServiceModels();
            string json = JsonConvert.SerializeObject(Servicemodel.GetAirportList(aiportname));
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //GET api/<controller>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Navg_Servicecontroller/GetAirportDetails")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage GetAirportDetails(string aiportname)
        {
            GCServiceModels Servicemodel = new GCServiceModels();
            string json = JsonConvert.SerializeObject(Servicemodel.GetAirportData(aiportname));
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        ////GET api/<controller>
        //[AllowAnonymous]
        //[HttpGet]
        //[Route("api/Navg_Servicecontroller/GetAirportIACO")]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public HttpResponseMessage GetAirportIACO(string aiportname)
        //{
        //    GCServiceModels Servicemodel = new GCServiceModels();
        //    string json = JsonConvert.SerializeObject(Servicemodel.GetAirportIACOCode(aiportname));
        //    return Request.CreateResponse(HttpStatusCode.OK, json);
        //}


        //GET api/<controller>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Navg_Servicecontroller/GetAirportIACO")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HttpResponseMessage GetAirportIACO(string aiportLat, string aiportLong)
        {
            GCServiceModels Servicemodel = new GCServiceModels();
            string json = JsonConvert.SerializeObject(Servicemodel.GetAirportIACOCode(aiportLat, aiportLong));
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }


        //[AllowAnonymous]
        //[HttpGet]
        //[Route("api/Navg_Servicecontroller/GetAirportListTest")]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public HttpResponseMessage GetAirportListTest(string sidx,string page, string sord, string rows,string searchTerm)
        //{
        //    GCServiceModels Servicemodel = new GCServiceModels();
        //    string json = JsonConvert.SerializeObject(Servicemodel.GetAirportList(searchTerm));
        //    return Request.CreateResponse(HttpStatusCode.OK, json);
        //}
    }
}