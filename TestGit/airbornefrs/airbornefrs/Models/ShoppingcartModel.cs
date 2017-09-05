using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using airbornefrs.Data;
using airbornefrs.Data.EcommerceEF;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web.Configuration;
using System.Drawing;

namespace airbornefrs.Models
{
    public class ShoppingcartModel
    {
        db_AirborneEntities OME = new db_AirborneEntities();

        public class itemDetailsDataContract
        {
            private string _Item_Price = "0";
            public long Item_ID { get; set; }
            public string Item_Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }

            [DataType(DataType.Currency)]
            public decimal Item_Price { get; set; }
            public string Image_Name { get; set; }
            public string AddedBy { get; set; }
            public string Status { get; set; }
            public decimal Shipping_Price { get; set; }
            public decimal Tax_Amount { get; set; }
            public string Item_code { get; set; }
            public string Short_Description { get; set; }
        }

        public class OrderItemModel
        {
            public long ID { get; set; }
            public string OrderID { get; set; }
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public Nullable<int> Quantity { get; set; }
            [DataType(DataType.Currency)]
            public Nullable<decimal> Price { get; set; }
            [DataType(DataType.Currency)]
            public Nullable<decimal> TotalPrice { get; set; }

            public Nullable<long> ProductID { get; set; }
            [DataType(DataType.Currency)]
            public Nullable<decimal> ShippingPerUnit { get; set; }
            [DataType(DataType.Currency)]
            public Nullable<decimal> TaxPerUnit { get; set; }

            public Nullable<System.DateTime> CreatedDate { get; set; }
            public string ProductImage { get; set; }
        }


        public class OrderDetailsModel
        {
            public long ID { get; set; }
            public string PaypalTransactionID { get; set; }
            public string OrderID { get; set; }
            public string OrderDescription { get; set; }
            [DataType(DataType.Currency)]
            public Nullable<decimal> OrderPrice { get; set; }
            [DataType(DataType.Currency)]
            public Nullable<decimal> CartSubtotal { get; set; }
            public Nullable<int> OrderItems { get; set; }
            public string Email { get; set; }
            public string ShippingAddress { get; set; }
            public string PhoneNumber { get; set; }
            public bool PhoneType { get; set; }
            public bool AcceptMssage { get; set; }
            public string RecipientName { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
            public string CountryCode { get; set; }

            public string Status { get; set; }
            public Nullable<System.DateTime> CreatedDate { get; set; }
            public Nullable<System.DateTime> ModifiedDate { get; set; }
            [DataType(DataType.Currency)]
            public Nullable<decimal> ShippingPrice { get; set; }
            [DataType(DataType.Currency)]
            public Nullable<decimal> Tax { get; set; }

            public IList<OrderItemModel> ItemModel { get; set; }
        }

        public enum ItemStatus
        {
            Instock = 0, OutOfStock = 1,
        }

        public static string GetItemStatus(string Input)
        {
            try
            {
                if ((ItemStatus)Convert.ToInt32(Input) == ItemStatus.Instock)
                { return "In stock"; }

                if ((ItemStatus)Convert.ToInt32(Input) == ItemStatus.OutOfStock)
                { return "Out of stock"; }
            }
            catch (Exception ex)
            {
                return "";
            }
            return "";
        }

        public enum OrderStatus
        {
            Created, Dispatched, Delivered, Delayed
        }

        // This method is get the Item details from Db and bind to list  using the Linq query
        public List<itemDetailsDataContract> GetItemDetails()
        {
            var query = (from a in OME.ItemDetails orderby a.Item_ID select a).Distinct();
            List<itemDetailsDataContract> ItemDetailsList = new List<itemDetailsDataContract>();
            query.ToList().ForEach(rec =>
            {
                ItemDetailsList.Add(new itemDetailsDataContract
                {
                    Item_ID = Convert.ToInt64(rec.Item_ID),
                    Item_Name = rec.Item_Name,
                    Description = rec.Description,
                    Item_Price = rec.Item_Price,
                    Image_Name = rec.Image_Name,
                    AddedBy = rec.AddedBy,
                    Status = rec.Status ?? "0",
                    Shipping_Price = rec.ShippingCost ?? 0,
                    Tax_Amount = rec.TaxAmount ?? 0,
                    Item_code = rec.Item_code ?? "N/A",
                    Short_Description = rec.Short_Description

                });
            });
            //ItemDetailsList.Reverse();
            return ItemDetailsList;
        }


        // This method is get the order details from Db and bind to list  using the Linq query
        public List<OrderDetailsModel> GetOrdersList()
        {
            var query = (from a in OME.Order_Payments select a).Distinct();
            List<OrderDetailsModel> OrdersList = new List<OrderDetailsModel>();
            query.ToList().ForEach(x =>
            {
                OrdersList.Add(new OrderDetailsModel
                {
                    ID = x.ID,
                    PaypalTransactionID = x.PaypalTransactionID ?? "N/A",
                    OrderID = x.OrderID,
                    OrderDescription = x.OrderDescription,
                    OrderPrice = x.OrderPrice,
                    OrderItems = x.OrderItems,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber ?? "N/A",
                    ShippingAddress = x.ShippingAddress,
                    Status = (x.Status == "1") ? "Successful" : "Failed",
                    CreatedDate = x.CreatedDate,
                    ShippingPrice = x.ShippingPrice,
                    Tax = x.Tax
                });
            });
            return OrdersList;
        }


        // This method is get the order details from Db and bind to list  using the Linq query
        public List<OrderDetailsModel> GetOrderDetails(string id)
        {
            var query = (from a in OME.Order_Payments where a.OrderID == id select a).Distinct();
            List<OrderDetailsModel> OrdersList = new List<OrderDetailsModel>();
            query.ToList().ForEach(x =>
            {
                OrdersList.Add(new OrderDetailsModel
                {
                    ID = x.ID,
                    PaypalTransactionID = x.PaypalTransactionID ?? "N/A",
                    OrderID = x.OrderID,
                    OrderDescription = x.OrderDescription,
                    OrderPrice = x.OrderPrice,
                    OrderItems = x.OrderItems,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber ?? "N/A",
                    ShippingAddress = x.ShippingAddress,
                    RecipientName = x.RecipientName,
                    Street = x.Street,
                    City = x.City,
                    State = x.State,
                    PostalCode = x.PostalCode,
                    CountryCode = x.CountryCode,
                    Status = (x.Status == "1") ? "Successful" : "Failed",
                    CreatedDate = x.CreatedDate,
                    ShippingPrice = x.ShippingPrice,
                    Tax = x.Tax,
                    CartSubtotal = x.OrderPrice - (x.ShippingPrice + x.Tax),
                    ItemModel = x.Order_Items.ToList().Select(rec => new OrderItemModel
                    {
                        ProductCode = Convert.ToString(rec.ProductCode),
                        ProductName = rec.ProductName,
                        Price = rec.Price ?? 0,
                        TotalPrice = rec.TotalPrice + rec.Quantity * (rec.ShippingPerUnit + rec.TaxPerUnit),
                        Quantity = rec.Quantity,
                        ProductID = rec.ProductID,
                        ShippingPerUnit = rec.ShippingPerUnit,
                        TaxPerUnit = rec.TaxPerUnit,
                        ProductImage = OME.ItemDetails.Where(y => y.Item_code == rec.ProductCode).Select(y => y.Image_Name).FirstOrDefault().ToString() ?? ""
                        //GetImagename(rec.ProductCode)
                        //Image_Name =OME.ItemDetails.tolist().Where(y=>y.Item_code==rec.ProductCode).FirstOrDefault().ToString() ?? ""
                    }).ToList()
                });
            });
            return OrdersList;
        }

        //Get order email details
        public OrderDetailsModel GetOrderDetailsForEmail(string id)
        {
            OrderDetailsModel OrdersList = new OrderDetailsModel();
            var query = OME.Order_Payments.ToList().Where(x => x.OrderID == id).Select(x => new OrderDetailsModel
            {
                ID = x.ID,
                PaypalTransactionID = x.PaypalTransactionID ?? "N/A",
                OrderID = x.OrderID,
                OrderDescription = x.OrderDescription,
                OrderPrice = x.OrderPrice,
                OrderItems = x.OrderItems,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber ?? "N/A",
                ShippingAddress = x.ShippingAddress,
                RecipientName = x.RecipientName,
                Street = x.Street,
                City = x.City,
                State = x.State,
                PostalCode = x.PostalCode,
                CountryCode = x.CountryCode,
                Status = (x.Status == "1") ? "Successful" : "Failed",
                CreatedDate = x.CreatedDate,
                ShippingPrice = x.ShippingPrice,
                Tax = x.Tax,
                CartSubtotal = x.OrderPrice - (x.ShippingPrice + x.Tax),
                ItemModel = x.Order_Items.ToList().Select(rec => new OrderItemModel
                {
                    ProductCode = Convert.ToString(rec.ProductCode),
                    ProductName = rec.ProductName,
                    Price = rec.Price ?? 0,
                    TotalPrice = rec.TotalPrice + rec.Quantity * (rec.ShippingPerUnit + rec.TaxPerUnit),
                    Quantity = rec.Quantity,
                    ProductID = rec.ProductID,
                    ShippingPerUnit = rec.ShippingPerUnit,
                    TaxPerUnit = rec.TaxPerUnit,
                    ProductImage = GetImagePath(rec.ProductCode),
                    //ProductImage = OME.ItemDetails.Where(y => y.Item_code == rec.ProductCode).Select(y => y.Image_Name).FirstOrDefault().ToString() ?? ""
                }).ToList()

            }).FirstOrDefault();

            return query;
        }

        public string GetImagePath(string Itemcode)
        {
            try
            {
                var Image_Name = OME.ItemDetails.Where(y => y.Item_code == Itemcode).Select(y => y.Image_Name).FirstOrDefault().ToString() ?? "";
                string path = HttpContext.Current.Server.MapPath("~/images/Prodcuts/" + Image_Name);
                if (!string.IsNullOrEmpty(path))
                {
                    string rooturl = WebConfigurationManager.AppSettings["rootUrl"];
                    string Imagepath = rooturl + "images/Products/" + Image_Name;
                    return Imagepath;
                }
                return Image_Name;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //public string GetImagename(string Itemcode)
        //{
        //    try
        //    {
        //        var image = OME.ItemDetails.Where(y => y.Item_code == Itemcode).Select(y => y.Image_Name).FirstOrDefault().ToString();
        //        return image;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}

        public itemDetailsDataContract GetItemDetail(int? itemID)
        {
            itemDetailsDataContract model = new itemDetailsDataContract();
            model = OME.ItemDetails.Where(x => x.Item_ID == itemID).ToList().Select(x => new itemDetailsDataContract
            {
                Item_ID = Convert.ToInt64(x.Item_ID),
                Item_Name = x.Item_Name,
                Item_Price = x.Item_Price,
                Description = x.Description,
                Image_Name = x.Image_Name,
                AddedBy = x.AddedBy,
                Status = x.Status ?? "0",
                Shipping_Price = x.ShippingCost ?? 0,
                Tax_Amount = x.TaxAmount ?? 0,
                Item_code = x.Item_code ?? "N/A",
                Short_Description = x.Short_Description

            }).SingleOrDefault();

            return model;
        }

        public itemDetailsDataContract GetItemForAddtocart(string itemCode)
        {
            long itemID;
            bool result = Int64.TryParse(itemCode, out itemID);
            if (!result)
            {
                itemID = 0;
            }

            itemDetailsDataContract model = new itemDetailsDataContract();
            model = OME.ItemDetails.Where(x => x.Item_ID == itemID || x.Item_code == itemCode).ToList().Select(x => new itemDetailsDataContract
            {
                Item_ID = Convert.ToInt64(x.Item_ID),
                Item_Name = x.Item_Name,
                Item_Price = x.Item_Price,
                Description = x.Description,
                Image_Name = x.Image_Name,
                AddedBy = x.AddedBy,
                Status = x.Status ?? "0",
                Shipping_Price = x.ShippingCost ?? 0,
                Tax_Amount = x.TaxAmount ?? 0,
                Item_code = x.Item_code ?? "N/A",
                Short_Description = x.Short_Description

            }).SingleOrDefault();

            return model;
        }

        public string GetItemDelete(string[] ItemList)
        {
            if (ItemList != null)
            {
                foreach (var item in ItemList)
                {
                    if (!string.IsNullOrEmpty(item) && Convert.ToInt64(item) > 0)
                    {
                        long ID = Convert.ToInt64(item);
                        var chk_Itemexist = OME.ItemDetails.Where(x => x.Item_ID == ID).FirstOrDefault();
                        if (chk_Itemexist != null)
                        {
                            OME.ItemDetails.Remove(chk_Itemexist);
                            OME.SaveChanges();
                        }
                    }
                }
            }
            return "OK";
        }

        public string addItemMaster(itemDetailsDataContract ItmDetails)
        {
            try
            {
                var checkIDExist = OME.ItemDetails.Where(x => x.Item_code == ItmDetails.Item_code).Any();
                if (checkIDExist)
                {
                    return "Duplicate";
                }

                ItemDetail EF = new ItemDetail();
                EF.Item_code = ItmDetails.Item_code;
                //EF.Item_ID = Convert.ToInt32(ItmDetails.Item_ID);
                EF.Item_Name = ItmDetails.Item_Name;
                EF.Description = ItmDetails.Description;
                EF.Short_Description = ItmDetails.Short_Description;
                EF.Item_Price = ItmDetails.Item_Price;
                EF.Image_Name = ItmDetails.Image_Name;
                EF.AddedBy = ItmDetails.AddedBy;
                EF.ShippingCost = ItmDetails.Shipping_Price;
                EF.Status = ItmDetails.Status;
                EF.TaxAmount = ItmDetails.Tax_Amount;
                EF.CreatedDate = DateTime.UtcNow;
                OME.ItemDetails.Add(EF);
                OME.SaveChanges();
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return "Error";
            }
            return "Ok";
        }

    }
}