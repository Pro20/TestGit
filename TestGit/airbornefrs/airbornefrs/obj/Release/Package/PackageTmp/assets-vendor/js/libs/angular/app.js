var controllerProvider = null;
var btApp = angular.module('btApp', [], function ($controllerProvider) {
    controllerProvider = $controllerProvider;
});
btApp.controller('appCtrl', ['$scope', '$http', function ($scope, $http) {// here $scope is used for share data between view and controller
    $scope.heading = "Aplication Heading";
}]);

function registerController(moduleName, controllerName, template, container) {
    // Load html file with content that uses Ctrl controller
    $(template).appendTo(container);
    // Here I cannot get the controller function directly so I
    // need to loop through the module's _invokeQueue to get it
    var queue = angular.module(moduleName)._invokeQueue;
    for (var i = 0; i < queue.length; i++) {
        var call = queue[i];
        if (call[0] == "$controllerProvider" && call[1] == "register" && call[2][0] == controllerName) {
            controllerProvider.register(controllerName, call[2][1]);
        }
    }

    angular.injector(['ng', 'Foo']).invoke(function ($compile, $rootScope) {
        $compile($('#ctrl' + controllerName))($rootScope);
        $rootScope.$apply();
    });
}

/* Pagging */
btApp.controller('paggingCtrl', function ($scope, paggingFactory) {
    $scope.onPageChange = function ($e, direction) {
        //console.log($e);
        var ele = $($e.target).parents(".paggingPanel");
        //console.log(ele);
        //return;

        var pageNo = paggingFactory.GetRequestedPageNo(ele, direction);
        if (pageNo == undefined) return false;
        //$scope.$parent.RetriveListData(pageNo, $(ele).find(".txtSortByColName").val(), $(ele).find(".txtSortDirection").val());
        paggingFactory.RefreshListData(ele);
    }


});
btApp.factory('paggingFactory', function () {
    var fac = {};
    var FUN;

    fac.RefreshList = function (fun) { // Call this function in ListCtrScope to set FUNCTION
        FUN = fun;
    }
    fac.RefreshListData = function (ele) { // this will call custom FUNCTION declared in ListCtrScope
        console.log(ele);
        console.log("paggingFactory.RefreshListData()");
        FUN($(ele).find(".txtCurrentPageNo").val(), $(ele).find(".txtSortByColName").val(), $(ele).find(".txtSortDirection").val());
    }
    fac.GetRequestedPageNo = function (ele, direction) {
        var currentPageNo = $(ele).find(".txtCurrentPageNo").val();
        var totalPages = $(ele).find(".txtTotalPages").val();

        if (currentPageNo == "") currentPageNo = 0;
        if (totalPages == "") totalPages = 0;

        if (direction == 'first') {
            currentPageNo = 1;
        } else if (direction == 'previous') {
            currentPageNo = parseInt(currentPageNo) - 1;
        } else if (direction == 'next') {
            currentPageNo = parseInt(currentPageNo) + 1;
        } else if (direction == 'last') {
            currentPageNo = totalPages;
        } else if (direction == 'pageno') {
            currentPageNo = $(ele).find(".txtCurrentPageNo").val();
        }
        if (currentPageNo <= 0) {
            alert("Invalid page number");
            return;
        } else if (parseInt(currentPageNo) > parseInt(totalPages)) {
            alert("You have total " + totalPages + " page(s)");
            return;
        }
        $(ele).find(".txtCurrentPageNo").val(currentPageNo)
        return currentPageNo;

    };
    fac.updateSortingFields = function ($e, fieldname, listPanelIdentifier) {

        var ele = $($e.target).parents(listPanelIdentifier).find(".paggingPanel");
        //var ele = $($e.target).find(".paggingPanel");
        //var ele = $($e.target).parentsUntil("div", ".card").find(".paggingPanel");
        //var ele = $($e.target).closest(".paggingPanel");
        //console.log(ele);
        //return;
        //var ele = $($e.target).find(".paggingPanel");
        var direction = $(ele).find(".txtSortDirection");
        if (fieldname == $(ele).find(".txtSortByColName").val()) {
            if ($(direction).val() == "1") {
                $(direction).val("0"); //Asc
            } else {
                $(direction).val("1"); //desc
            }
        } else {
            $(direction).val("0"); //Asc
        }
        $(ele).find(".txtSortByColName").val(fieldname);
        fac.RefreshListData(ele);
    }
    return fac;
});
/* /Pagging */