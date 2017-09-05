btApp.filter('startFrom', function () {
    return function (input, start) {
        if (input) {
            start = +start; //parse to int
            return input.slice(start);
        }
        return [];
    }
});

btApp.controller("Order_ManagerController", function ($scope, $timeout, $rootScope, $window, Orionvis_shoppingService) {

    var vm = this;
    vm.date = new Date();
    vm.currentPage = 1;
    vm.maxSize = 15;
    vm.itemsPerPage = 10;

    // This is publich method which will be called initially and load all the item Details. 
    GetItemDetails();
    //To Get All Records   
    function GetItemDetails() {
        var promiseGet = Orionvis_shoppingService.GetOrderlist();
        promiseGet.then(function (pl) {
            vm.getItemDetailsDisp = JSON.parse(pl.data);
            vm.totalItems = vm.getItemDetailsDisp.length;
        },
             function (errorPl) {
             });
        $timeout(GetItemDetails, 30000);
    }

    //This method is used to see Edit of an items
    vm.redirectToDetail = function (id) {
        window.location = "/Admin/OrderManager/Detail?id=" + id;
    }
    //Clear form 
    
});






