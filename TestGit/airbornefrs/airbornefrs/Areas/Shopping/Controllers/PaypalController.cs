using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using log4net.Repository.Hierarchy;
using PayPal.Api;
using airbornefrs.Models;
using Newtonsoft.Json;
using System.Text;
using airbornefrs.Data.EcommerceEF;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Razor.Parser;
using RazorEngine;

namespace airbornefrs.Areas.Shopping.Controllers
{
    public class PaypalController : Controller
    {
        string OrderId = "";

        // GET: Shopping/Paypal
        public ActionResult Index()
        {
            return View("SuccessView");
        }


        //Paypal payment
        public ActionResult PaymentWithPaypal(List<string> data)
        {
            OrderId = DateTime.Now.ToString("ddmmyyyyhhmmss");

            //getting the apiContext as earlier
            APIContext apiContext = PaymentConfiguration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    // So we have provided URL of this controller only
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Paypal/PaymentWithPayPal?";

                    //guid we are generating for storing the paymentID received in session
                    //after calling the create function and it is used in the payment execution
                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment
                    //var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, data);

                    //get links returned from paypal in response to Create function call
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);
                    return Json(new { data = paypalRedirectUrl }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // This section is executed when we have received all the payments parameters
                    // from the previous call to the function Create
                    // Executing a payment
                    var guid = Request.Params["guid"];
                    var orderid = Request.Params["OrionOrderId"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return RedirectToAction("FailureView", "Paypal", new { area = "Shopping" });
                    }

                    var payment = Payment.Get(apiContext, Session[guid].ToString());
                    string city = payment.transactions[0].item_list.shipping_address.city;
                    string postalcode = payment.transactions[0].item_list.shipping_address.postal_code;
                    string countryCode = payment.transactions[0].item_list.shipping_address.country_code;
                    string recipentName = payment.transactions[0].item_list.shipping_address.recipient_name;
                    string state = payment.transactions[0].item_list.shipping_address.state;
                    string street = payment.transactions[0].item_list.shipping_address.line1;

                    StringBuilder sb = new StringBuilder();
                    sb.Append("Recipient name: ");
                    sb.Append(recipentName);
                    sb.Append(Environment.NewLine);
                    sb.Append("Address: ");
                    sb.Append(street);
                    sb.Append(Environment.NewLine);
                    sb.Append("City: ");
                    sb.Append(city);
                    sb.Append(Environment.NewLine);
                    sb.Append("State: ");
                    sb.Append(state);
                    sb.Append(Environment.NewLine);
                    sb.Append("Postal code: ");
                    sb.Append(postalcode);
                    sb.Append(Environment.NewLine);
                    sb.Append("Country code: ");
                    sb.Append(countryCode);
                    sb.Append(Environment.NewLine);
                    string ShippingAddress = sb.ToString();

                    //update order after recievining parameters from paypal
                    db_AirborneEntities OEM = new db_AirborneEntities();
                    var updatePayment = OEM.Order_Payments.Where(x => x.OrderID != null && x.OrderID == orderid).FirstOrDefault();
                    if (updatePayment != null)
                    {
                        TempData["Status"] = updatePayment.OrderID;
                        updatePayment.ShippingAddress = ShippingAddress;
                        updatePayment.RecipientName = recipentName ?? "";
                        updatePayment.Street = street ?? "";
                        updatePayment.City = city ?? "";
                        updatePayment.State = state ?? "";
                        updatePayment.PostalCode = postalcode ?? "";
                        updatePayment.CountryCode = countryCode ?? "";

                        updatePayment.Status = "1";
                        updatePayment.ModifiedDate = DateTime.UtcNow;
                        updatePayment.PaypalTransactionID = Convert.ToString(Session[guid]);
                        OEM.SaveChanges();
                    }

                    var emailStatus = HtmlHelpersUtility.SendOrderEmail(updatePayment.OrderID, updatePayment.Email);

                }
            }
            catch (Exception ex)
            {
                log4net.ILog logger = log4net.LogManager.GetLogger("Errorlog");
                logger.Error("Error" + ex.Message);
                //Logger.log("Error" + ex.Message);
                return RedirectToAction("FailureView", "Paypal", new { area = "Shopping" });
            }
            return RedirectToAction("SuccessView", "Paypal", new { area = "Shopping" });
        }



        public ActionResult SuccessView()
        {
            return View();
        }


        public ActionResult FailureView()
        {
            return View();
        }

        #region Paypal Utility
        private PayPal.Api.Payment payment;

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl, List<string> data)
        {
            string SubTotal = data[1], ShippingCharge = data[2], GrandTotal = data[3], _tax = data[4], cartemail = data[5], cartphone = data[6], PhoneType = data[7], AcceptMessage = data[8];
            var Items = JsonConvert.DeserializeObject<List<ItemModel>>(data[0]);

            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            foreach (var Product in Items)
            {
                itemList.items.Add(new Item()
                {
                    name = Product.Item_Name,
                    currency = "USD",
                    price = Product.Item_Price.ToString(),
                    quantity = Product.ItemCounts.ToString(),
                    sku = Product.Item_code.ToString()
                });
            }

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&OrionOrderId=" + OrderId,
                return_url = redirectUrl + "&OrionOrderId=" + OrderId
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = _tax,
                shipping = ShippingCharge,
                subtotal = SubTotal
            };

            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDecimal(details.shipping) + Convert.ToDecimal(details.subtotal) + Convert.ToDecimal(details.tax)).ToString(), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction on " + DateTime.UtcNow.ToString(),
                invoice_number = OrderId,
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            SavePayments(data, ShippingCharge, _tax, amount.total, "Transaction on " + DateTime.UtcNow.ToString(), cartemail, cartphone, PhoneType, AcceptMessage);



            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }

        /// <summary>
        /// Save the order in database 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ShippingCharge"></param>
        /// <param name="_tax"></param>
        /// <param name="GrandTotal"></param>
        /// <param name="description"></param>
        private void SavePayments(List<string> data, string ShippingCharge, string _tax, string GrandTotal, string description, string _cartemail, string _cartphone,string _phoneType , string _acceptMessage)
        {
            try
            {
                string SubTotal = data[1];
                var Items = JsonConvert.DeserializeObject<List<ItemModel>>(data[0]);

                db_AirborneEntities OEM = new db_AirborneEntities();
                Order_Payments _objOrderPayment = new Order_Payments();
                _objOrderPayment.OrderID = OrderId;
                _objOrderPayment.OrderDescription = description;
                _objOrderPayment.OrderPrice = Convert.ToDecimal(GrandTotal);
                _objOrderPayment.OrderItems = Items.Count;
                _objOrderPayment.Email = _cartemail;
                _objOrderPayment.PhoneNumber = _cartphone;
                _objOrderPayment.ShippingAddress = "";
                _objOrderPayment.Status = "0";
                _objOrderPayment.ShippingPrice = Convert.ToDecimal(ShippingCharge);
                _objOrderPayment.Tax = Convert.ToDecimal(_tax);
                _objOrderPayment.CreatedDate = DateTime.UtcNow;
                _objOrderPayment.PhoneType = (string.IsNullOrEmpty(_phoneType)) ? false : ((_phoneType.ToString().ToLower() == "true") ? true : false); 
                _objOrderPayment.AcceptMessage = (string.IsNullOrEmpty(_acceptMessage)) ? false : ((_acceptMessage.ToString().ToLower() == "true") ? true : false);

                OEM.Order_Payments.Add(_objOrderPayment);

                foreach (var Product in Items)
                {
                    Order_Items _objOrderItem = new Order_Items();
                    _objOrderItem.OrderID = _objOrderPayment.OrderID;
                    _objOrderItem.Price = Convert.ToDecimal(Product.Item_Price);
                    _objOrderItem.ProductCode = Product.Item_code.ToString();
                    _objOrderItem.ProductName = Product.Item_Name;
                    _objOrderItem.Quantity = Product.ItemCounts;
                    _objOrderItem.TotalPrice = Product.ItemTotalPrice;
                    _objOrderItem.ProductID = Convert.ToInt64(Product.Item_ID);
                    _objOrderItem.ShippingPerUnit = Product.ItemShippingCost;
                    _objOrderItem.TaxPerUnit = Product.ItemTax;
                    _objOrderItem.CreatedDate = DateTime.UtcNow;
                    OEM.Order_Items.Add(_objOrderItem);

                }
                OEM.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
    }

    public class ItemModel
    {
        public string Item_ID { get; set; }
        public string Item_Name { get; set; }
        public string Description { get; set; }
        public string Item_Price { get; set; }
        public string Image_Name { get; set; }
        public string Item_code { get; set; }
        public decimal ItemShippingCost { get; set; }
        public decimal ItemTax { get; set; }

        public int ItemCounts { get; set; }
        public decimal ItemTotalPrice { get; set; }
    }

    public class PaymentModel
    {
        public string Item_ID { get; set; }
        public string Item_Name { get; set; }
        public string Description { get; set; }
        public string Item_Price { get; set; }
        public string Image_Name { get; set; }
        public int ItemCounts { get; set; }
        public decimal ItemTotalPrice { get; set; }
    }
}