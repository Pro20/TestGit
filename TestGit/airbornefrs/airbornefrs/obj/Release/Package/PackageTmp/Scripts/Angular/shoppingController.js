
btApp.controller("ShoppingController", function ($scope, $http, $timeout, $rootScope, $window, Orionvis_shoppingService) {
    
    var vm = this;
    //This variable will be used to Increment the item Quantity by every click.
    var ItemCountExist = 0;

    // This public method will be called initially and load all the item Details.
    GetItemDetails();

    function GetItemDetails() {
        var promiseGet = Orionvis_shoppingService.GetItemDetails();
        promiseGet.then(function (pl) {
            vm.getItemDetailsDisp = JSON.parse(pl.data)
        },
             function (errorPl) {
             });
    }

    //This method is used to see details of an items
    vm.redirectToDetail = function (k) {
        window.location = "/Shopping/Product/Detail?id=" + k;
    }

    //This method is used to add items to cart
    vm.AddToCart = function (item) {
        var items = (localStorage.getItem("cart") == null || localStorage.getItem("cart") == undefined) ? [] : JSON.parse(localStorage.getItem("cart"));
        if (items.length > 0) {
            for (count = 0; count < items.length; count++) {
                if (items[count].Item_ID == item.Item_ID) {
                    ItemCountExist = items[count].ItemCounts + 1;
                    items[count].ItemCounts = ItemCountExist;
                    items[count].ItemTotalPrice = ItemCountExist * item.Item_Price;
                    localStorage.setItem("cart", JSON.stringify(items));
                    toastr.success(item.Item_Name + " successfully added to the cart")
                }
            }
        }
        if (ItemCountExist <= 0) {
            ItemCountExist = 1;
            var ItmDetails = {
                Item_ID: item.Item_ID,
                Item_code: item.Item_code,
                Item_Name: item.Item_Name,
                //Description: item.Description,
                Item_Price: item.Item_Price,
                Image_Name: item.Image_Name,
                ItemCounts: ItemCountExist,
                ItemTotalPrice: item.Item_Price,
                ItemShippingCost: item.Shipping_Price,
                ItemTax: item.Tax_Amount
            };
            items.push(ItmDetails);
            localStorage.setItem("cart", JSON.stringify(items));
            toastr.success(item.Item_Name + " successfully added to the cart")
        }
        ItemCountExist = 0;
        checkcartcount();
    }
});