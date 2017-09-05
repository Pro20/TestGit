using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace airbornefrs.Framework
{
    public class Utility
    {
        public static string GetXMLFromObject(object obj)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                StringWriter sw = new StringWriter();
                XmlTextWriter tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, obj);
                return sw.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static BoolResponse ValidateCaptcha(string grecaptcharesponse)
        {
            BoolResponse bResponse = new BoolResponse();
            //secret that was generated in key value pair
            string secret = System.Configuration.ConfigurationManager.AppSettings["reCaptSecretkey"];

            var client = new WebClient();
            var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, grecaptcharesponse));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count <= 0)
                {
                    return bResponse;
                }
                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        bResponse.message = "The secret parameter is missing.";
                        break;
                    case ("invalid-input-secret"):
                        bResponse.message = "The secret parameter is invalid or malformed.";
                        break;
                    case ("missing-input-response"):
                        bResponse.message = "The response parameter is missing.";
                        break;
                    case ("invalid-input-response"):
                        bResponse.message = "The response parameter is invalid or malformed.";
                        break;
                    default:
                        bResponse.message = "Error occured. Please try again";
                        break;
                }
                bResponse.status = 0;
            }
            else
            {
                bResponse.message = "Valid";
                bResponse.status = 1;
            }

            return bResponse;
        }


    }

    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }

    }
}