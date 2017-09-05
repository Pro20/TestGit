
btApp.service("Orionvis_shoppingService", function ($http) {
    //Get Product list
    this.GetItemDetails = function () {
        return $http.get(apiUrl + "api/ManageProduct");
        //return $http.get("http://localhost:50360/api/ManageProduct");
    };

    //To Save the Product into the Database    
    this.post = function (ItemDetails) {
        var request = $http({
            method: "post",
            url: apiUrl + "api/ManageProduct",
            data: ItemDetails
        });
        return request;
    }

    //Get Product list
    this.GetOrderlist = function () {
        return $http.get(apiUrl + "api/Ordercontroller/GetOrdersList");
    };


});
