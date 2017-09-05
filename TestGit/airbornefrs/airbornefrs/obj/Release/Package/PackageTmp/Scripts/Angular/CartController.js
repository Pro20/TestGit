
btApp.directive("phoneNumberValidator", function () {
    return {
        restrict: 'A',
        link: function (scope, elem, attrs, ctrl, ngModel) {
            elem.add(phonenumber).on('keyup', function () {
                var input = elem.val();
                // Strip all characters from the input except digits
                input = input.replace(/\D/g, '');

                // Trim the remaining input to ten characters, to preserve phone number format
                input = input.substring(0, 10);

                // Based upon the length of the string, we add formatting as necessary
                var size = input.length;
                if (size == 0) {
                    input = input;
                } else if (size < 4) {
                    input = '(' + input;
                } else if (size < 7) {
                    input = '(' + input.substring(0, 3) + ') ' + input.substring(3, 6);
                } else {
                    input = '(' + input.substring(0, 3) + ') ' + input.substring(3, 6) + ' - ' + input.substring(6, 10);
                }
                jQuery("#uPhone").val(input);
            });
        }
    }
});

btApp.controller("CartController", function ($scope, $http, $timeout, $rootScope, $window, Orionvis_shoppingService, $mdDialog, $mdMedia) {
    //This variable will be used to calculate and display the Cat Total Price,Total Qty and GrandTotal result in Cart
    var vm = this;
    vm.getItemDetailsDisp = [];
    //vm.Shippingcharge = 6.94;
    vm.Email = "";
    vm.Phone = "";

    $scope.ph_numbr = /^(\+?(\d{1}|\d{2}|\d{3})[- ]?)?\d{3}[- ]?\d{3}[- ]?\d{4}$/;

    // This is publich method which will be called initially and load all the item Details.
    GetItemDetails();

    function GetItemDetails() {
        vm.showItem = false;
        vm.showDetails = true;
        vm.showCartDetails = false;
        var Getitems = (localStorage.getItem("cart") == null || localStorage.getItem("cart") == undefined) ? [] : JSON.parse(localStorage.getItem("cart"));
        if (Getitems.length > 0) {
            vm.getItemDetailsDisp = JSON.parse(JSON.stringify(Getitems));
            
        }
    }

    //This method is used to to see the item details.
    vm.redirectToDetail = function (k) {
        window.location = "/Shopping/Product/Detail?id=" + k;
    }

    //This method is to remove the Item from the cart.Each Item inside the Cart can be removed.
    vm.removeFromCart = function (index) {
        vm.getItemDetailsDisp.splice(index, 1);
        localStorage.setItem("cart", JSON.stringify(vm.getItemDetailsDisp));
        checkcartcount();
    }

    //This method calculate the Subtotal price of cart.
    vm.subTotal = function () {
        var subTotal = 0;
        angular.forEach(vm.getItemDetailsDisp, function (x) {
            subTotal += (x.ItemCounts * x.Item_Price);
            x.ItemTotalPrice = (x.ItemCounts * x.Item_Price);
        });
        localStorage.setItem("cart", JSON.stringify(vm.getItemDetailsDisp))
        return subTotal;
    };

    //This method calculate the Grandtotal price of cart.
    vm.GrandtotalPrice = function () {
        checkcartcount();
        return vm.TotalShippingCost() + vm.subTotal() + vm.tax();
    };

    vm.tax = function () {
        var _tax = 0;
        angular.forEach(vm.getItemDetailsDisp, function (x) {
            _tax += (x.ItemCounts * x.ItemTax);
        });
        return _tax;
    };

    vm.TotalShippingCost = function () {
        var _shipping = 0;
        angular.forEach(vm.getItemDetailsDisp, function (x) {
            _shipping += (x.ItemCounts * x.ItemShippingCost);
        });
        return _shipping;
    };

    vm.submit = function (data, subTotal, shippingCharge, grandTotal, tax) {
        var purchaseDetails = [
            JSON.stringify(data),
            subTotal.toString(),
            shippingCharge.toString(),
        grandTotal.toString(),
        tax.toString(),
          vm.Email.toString(),
          vm.Phone.toString()
        ];

        $http.post("/Shopping/Paypal/PaymentWithPaypal",
                   {
                       data: purchaseDetails
                   }).success(function (data, status) {
                       window.location = data.data;
                   }).error(function () {
                       window.location = "/Shopping/Paypal/FailureView";
                   })
    };



});

