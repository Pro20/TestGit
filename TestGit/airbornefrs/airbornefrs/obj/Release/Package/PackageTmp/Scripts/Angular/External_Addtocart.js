
var ItemCountExist = 0;
//This method is used to add items to cart

ext_AddItemIncart = function (id) {
    jQuery.support.cors = true;
    ItemCountExist = 0;

    var GetItem = $.ajax({
        url: apiUrl + "api/Ext_CartController/GetItem/" + id,
        crossDomain: true,
        type: 'GET',
        //data: JSON.stringify(id),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var parseData = JSON.parse(data)
            addToCart(parseData.Item_ID, parseData.Item_code, parseData.Item_Name, parseData.Item_Price, parseData.Image_Name, parseData.Shipping_Price, parseData.Tax_Amount);
        },
        error: function (x, y, z) {
            //alert(x + '\n' + y + '\n' + z);
        }
    });
}


ext_BuynowCart = function (id) {
    jQuery.support.cors = true;
    ItemCountExist = 0;
    var GetItem = $.ajax({
        url: apiUrl + "api/Ext_CartController/GetItem/" + id,
        crossDomain: true,
        type: 'GET',
        //data: JSON.stringify(id),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var parseData = JSON.parse(data)
            addToCart(parseData.Item_ID, parseData.Item_code, parseData.Item_Name, parseData.Item_Price, parseData.Image_Name, parseData.Shipping_Price, parseData.Tax_Amount);
            window.location = "/Shopping/Product/Cart";
        },
        error: function (x, y, z) {
            //alert(x + '\n' + y + '\n' + z);
        }
    });
}



addToCart = function (id, ItemCode, name, price, imagename, Shippingcost, tax) {
    var items = (localStorage.getItem("cart") == null || localStorage.getItem("cart") == undefined) ? [] : JSON.parse(localStorage.getItem("cart"));
    if (items.length > 0) {
        for (count = 0; count < items.length; count++) {
            if (items[count].Item_ID == id) {
                ItemCountExist = items[count].ItemCounts + 1;
                items[count].ItemCounts = ItemCountExist;
                items[count].ItemTotalPrice = ItemCountExist * price;
                localStorage.setItem("cart", JSON.stringify(items));
                toastr.success(name + " successfully added to the cart")
            }
        }
    }
    if (ItemCountExist <= 0) {
        ItemCountExist = 1;
        var ItmDetails = {
            Item_ID: id,
            Item_code: ItemCode,
            Item_Name: name,
            //Description: description,
            Item_Price: price,
            Image_Name: imagename,
            ItemCounts: ItemCountExist,
            ItemTotalPrice: price,
            ItemShippingCost: Shippingcost,
            ItemTax: tax

        };
        items.push(ItmDetails);
        localStorage.setItem("cart", JSON.stringify(items));
        toastr.success(name + " successfully added to the cart")
    }
    checkcartcount();
}
