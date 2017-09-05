using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Net.Mail;
using RazorEngine;

namespace airbornefrs.Models
{
    public class ContactModels
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

    }

    public class OrderEmailModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Company Name")]
        public string Companyname { get; set; }

        [Required]
        [Display(Name = "How can we help?")]
        public string Howcanwehelp { get; set; }

        [Display(Name = "Reason for enquiry")]
        public string EnquiryReason { get; set; }
    }


    //html Helper
    public static class HtmlHelpersUtility
    {
        public static IHtmlString reCaptcha(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();
            string publickey = WebConfigurationManager.AppSettings["RecaptchaPublicKey"];
            sb.AppendLine("<div class=\"g-recaptcha\" data-sitekey =\"" + publickey + "\"></div>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static string GetDocumentText(string filename)
        {
            string template = string.Empty;
            string physicalPath = WebConfigurationManager.AppSettings["rootUrl"].ToString() + "Mailer/";
            using (StreamReader streamReader = new StreamReader(System.Net.WebRequest.Create(physicalPath + filename).GetResponse().GetResponseStream()))
            {
                template = streamReader.ReadToEnd();
            }
            return template;
        }

        #region Mail utility

        //Sending Order email to user while user purchased Product
        public static string SendOrderEmail(string OrderID, string SendTo)
        {
            try
            {
                ShoppingcartModel model = new ShoppingcartModel();
                MailMessage mail = new MailMessage();
                mail.To.Add(SendTo);
                mail.From = new MailAddress(WebConfigurationManager.AppSettings["ToAdmin"]);//Change ToAdmin from web.config (All the emails are sent from admin)
                mail.Subject = "Order placed successfully (Order ID:" + OrderID + ")";

                var viewModel = model.GetOrderDetailsForEmail(OrderID);
                string template = GetDocumentText("PurchaseOrderMail.html");
                var body = Razor.Parse(template, viewModel);
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = WebConfigurationManager.AppSettings["host"];//Change host from web.config
                smtp.Port = Convert.ToInt32(WebConfigurationManager.AppSettings["port"]); //Change port from web.config
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["userName"], WebConfigurationManager.AppSettings["password"]);// change senders User name and password from web.config
                smtp.EnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["enableSsl"]);//Change ssl setting from web.config
                smtp.Send(mail);
                return "Order email is sent successfully";
            }
            catch (Exception ex)
            {
                return "Critical error occured, please try later.";
            }
        }
        #endregion


    }


    //recaptch Filter
    public class recaptchfilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.RequestContext.HttpContext.Request["g-recaptcha-response"] != null)
            {

                string privatekey = WebConfigurationManager.AppSettings["RecaptchaPrivateKey"];
                string response = filterContext.RequestContext.HttpContext.Request["g-recaptcha-response"];
                filterContext.ActionParameters["CaptchaValid"] = Validate(response, privatekey);
            }
        }


        public static bool Validate(string mainresponse, string privatekey)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create
                ("https://www.google.com/recaptcha/api/siteverify?secret=" +
                privatekey + "&response=" + mainresponse);

                WebResponse response = req.GetResponse();

                using (StreamReader readStream = new StreamReader(response.GetResponseStream()))
                {
                    string jsonResponse = readStream.ReadToEnd();

                    JsonResponseObject jobj = JsonConvert.DeserializeObject<JsonResponseObject>(jsonResponse);

                    return jobj.success;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public class JsonResponseObject
        {
            public bool success { get; set; }
            [JsonProperty("error-codes")]
            public List<string> errorcodes { get; set; }
        }

    }

}