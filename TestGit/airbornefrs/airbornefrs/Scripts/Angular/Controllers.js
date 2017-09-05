btApp.filter('startFrom', function () {
    return function (input, start) {
        if (input) {
            start = +start; //parse to int
            return input.slice(start);
        }
        return [];
    }
});

btApp.controller("Store_ManagerController", function ($scope, $timeout, $rootScope, $window, Orionvis_shoppingService, FileUploadService) {

    var vm = this;
    vm.date = new Date();
    //  To set and get the Item Details values
    var firstbool = true;
    vm.Imagename = "";
    vm.Item_ID = 0;
    vm.Item_Code = "";
    vm.Item_Name = "";
    vm.Description = "";
    vm.ShortDescription = "";
    vm.Item_Price = 0;
    vm.txtAddedBy = "";
    vm.Item_Status = "";
    vm.Shipping_Price = 0;
    vm.Tax_Amount = 0;
    vm.statusList = [
      { Value: '0', Text: 'In stock' },
      { Value: '1', Text: 'Out of stock' }
    ],

    vm.myCheckbx = {
    selected:{}};
    //vm.totalItems = 0;
    vm.currentPage = 1;
    vm.maxSize = 15;
    vm.itemsPerPage = 10;
    // This is publich method which will be called initially and load all the item Details. 
    GetItemDetails();
    //To Get All Records   
    function GetItemDetails() {
        var promiseGet = Orionvis_shoppingService.GetItemDetails();
        promiseGet.then(function (pl) {
            vm.getItemDetailsDisp = JSON.parse(pl.data);
            angular.forEach(vm.getItemDetailsDisp, function (item) {
                item.Checked = false;
            });

            vm.totalItems = vm.getItemDetailsDisp.length;
        },
             function (errorPl) {
             });
    }
  
    //Declarationa and Function for Image Upload and Save Data
    //--------------------------------------------
    // Variables
    vm.Message = "";
    vm.FileInvalidMessage = "";
    vm.SelectedFileForUpload = null;
    vm.FileDescription_TR = "";
    vm.IsFormSubmitted = false;
    vm.IsFileValid = false;
    vm.IsFormValid = false;

    //Form Validation
    $scope.$watch("f1.$valid", function (isValid) {
        vm.IsFormValid = isValid;
    });


    // THIS IS REQUIRED AS File Control is not supported 2 way binding features of Angular
    // ------------------------------------------------------------------------------------
    //File Validation
    vm.ChechFileValid = function (file) {
        var isValid = false;
        if (vm.SelectedFileForUpload != null) {
            if ((file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/gif') && file.size <= (800 * 800)) {
                vm.FileInvalidMessage = "";
                isValid = true;
            }
            else {
                if (file.size > (800 * 800)) {
                    vm.FileInvalidMessage = "Image size should be less than 625Kb";
                }
                else {
                    vm.FileInvalidMessage = "Only JPEG/PNG/Gif Image can be upload";
                }
            }
        }
        else {
            vm.FileInvalidMessage = "Image required!";
        }
        vm.IsFileValid = isValid;
    };

    //File Select event 
    vm.selectFileforUpload = function (file) {

        var files = file[0];
        if ((files.type == 'image/png' || files.type == 'image/jpeg' || files.type == 'image/gif')) {

            var reader = new FileReader();
            reader.readAsDataURL(files);
            reader.onload = function (e) {
                var image = new Image();
                image.src = e.target.result;
                image.onload = function () {
                    var height = this.height;
                    var width = this.width;
                    if (height < 500 || width < 500) {
                        toastr.warning("Image height and width must be equal to or greater than 500x500 px, please choose appropriate Image.");
                        $('input[type=file]').val(null);
                        //vm.FileInvalidMessage = "Image Height and Width must be 500px.";
                        return false;
                    }
                    vm.Imagename = files.name;
                    vm.SelectedFileForUpload = file[0];
                    return true;
                };
            }

        } else {
            toastr.warning("Please select a valid Image file.");
            //vm.FileInvalidMessage = "Please select a valid Image file.";
            return false;
        }
    }

    //----------------------------------------------------------------------------------------

    //This method is used to see Edit of an items
    vm.redirectToEdit = function (id) {
       window.location = "/Admin/StoreManager/Edit?id=" + id;
    }
    vm.deleteItems = function (id) {
        var data = [];
       // alert(JSON.stringify(vm.myCheckbx));
        angular.forEach(vm.getItemDetailsDisp, function (item) {
            if (item.Checked == true)
                data.push(item.Item_ID);
        });

        var DeltePost = Orionvis_shoppingService.deleteItems(data);
        DeltePost.then(function (res) {
            if (JSON.parse(res.data) == "OK")
                toastr.success("Deleted successfully")

            if (JSON.parse(res.data) == "Error")
                toastr.error("Some error occurd, please try later")
        
            GetItemDetails();
        }, function (err) {
            toastr.error("Some error occurd, please try later")
        });
    }

    //Save File
    vm.SaveFile = function () {
        vm.IsFormSubmitted = true;
        vm.Message = "";
        vm.ChechFileValid(vm.SelectedFileForUpload);
        if (vm.IsFormValid && vm.IsFileValid) {
            FileUploadService.UploadFile(vm.SelectedFileForUpload).then(function (d) {

                var ItmDetails = {
                    Item_ID: vm.Item_ID,
                    Item_Name: vm.Item_Name,
                    Description: vm.Description,

                    Item_Price: vm.Item_Price,
                    Image_Name: vm.Imagename,
                    AddedBy: vm.txtAddedBy,
                    Shipping_Price: vm.Shipping_Price,
                    Tax_Amount: vm.Tax_Amount,
                    Item_code: vm.Item_Code,
                    Status: vm.Item_Status.Value,
                    Short_Description: vm.ShortDescription
                };

                var promisePost = Orionvis_shoppingService.post(ItmDetails);
                promisePost.then(function (res) {
                    if (JSON.parse(res.data) == "Ok")
                        toastr.success(ItmDetails.Item_Name + " added successfully")

                    if (JSON.parse(res.data) == "Error")
                        toastr.error("Some error occurd, please try later")

                    if (JSON.parse(res.data) == "Duplicate")
                        toastr.warning("Item already exist")

                    GetItemDetails();
                }, function (err) {
                    toastr.error("Some error occurd, please try later")
                    // alert("Data Insert Error " + err.Message);
                });
                //alert(d.Message + " Item Saved!");
                vm.IsFormSubmitted = false;
                ClearForm();

            }, function (e) {
                alert(e);
            });
        }
        else {
            vm.Message = "All the fields are required.";
        }
    };
    //Clear form 
    function ClearForm() {
        vm.Imagename = "";
        vm.Item_ID = 0;
        vm.Item_Name = "";
        vm.Description = "";
        vm.Item_Price = "0";
        vm.txtAddedBy = "";

        vm.Item_Code = "";
        vm.Item_Name = "";
        vm.ShortDescription = "";
        vm.Item_Status = "";
        vm.Shipping_Price = 0;
        vm.Tax_Amount = 0;


        angular.forEach(angular.element("input[type='file']"), function (inputElem) {
            angular.element(inputElem).val(null);
        });
        $scope.f1.$setPristine();
        $scope.IsFormSubmitted = false;
    }

})
.factory('FileUploadService', function ($http, $q) {

    var fac = {};
    fac.UploadFile = function (file) {
        var formData = new FormData();
        formData.append("file", file);

        var defer = $q.defer();
        $http.post("/Admin/StoreManager/UploadFile", formData,
            {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            })
        .success(function (d) {
            defer.resolve(d);
        })
        .error(function () {
            defer.reject("File Upload Failed!");
        });
        return defer.promise;
    }
    return fac;

    //---------------------------------------------
    //End of Image Upload and Insert record
});






