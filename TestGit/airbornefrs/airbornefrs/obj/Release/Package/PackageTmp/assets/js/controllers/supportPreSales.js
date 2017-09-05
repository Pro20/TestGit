var objFormValidation;

btApp.controller('PreSalesCtrl', function ($scope, $http) {
    $scope.title = "sdg,bndskjhdf lkj hk";
    $scope.data = objPreSales;
    $scope.parent = { data: '' };

    $scope.resetform = function () {
    }
    $scope.submitForm = function () {
        if (!objFormValidation.isValid()) {
            return false;
        }
        objFormValidation.disableSubmitButtons(false);
        $scope.submitted = true;
        //console.log($("#g-recaptcha-response").val());
        $("#submit_btn").showSpinOnButton(1);
        $scope.data.reCaptchaResponse = $("#g-recaptcha-response").val();
        $http({
            url: '/Support/SavePreSalesDetails',
            method: 'POST',
            data: $scope.data,
            headers: { 'content-type': 'application/json' }
        }).then(function (responseData) {
            console.log(responseData);
            $("#submit_btn").showSpinOnButton(2);
            if (responseData.data == true || responseData.data == "true") {
                //$.btAlert({ title: 'Details Has Been Saved', content: 'We have received your details and will contact you as soon as possible.' });
                $("#PreSales").hide();
                $("#dvAfterSubmit").html("");
                $("#dvAfterSubmit").btSuccessMessage("<b>We have received your details and will contact you as soon as possible.<b>");
                $("#btNormalMessage").show();
            }
            else if (responseData.data == "3" || parseInt(responseData.data) == 3) {
                console.log("In Ok");
                $.btAlert({ title: 'Human Verification', content: 'Please verify that you are not a robot.' });
            }
            else {
                $.btAlert({ title: 'Unforeseen error occurred', content: 'unforeseen error occurred. Please try later.' });
            }
            return responseData;
        });
    }
});

$(document).ready(function () {
    $.btApplyCaptcha();
    $('#PreSales').formValidation({
        framework: 'bootstrap',
        icon: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        row: {
            valid: 'field-success',
            invalid: 'field-error'
        },
        fields: {
            Name: {
                validators: {
                    notEmpty: {
                        message: 'The Name is required'
                    }
                }
            },
            Email: {
                validators: {
                    notEmpty: {
                        message: 'Email address is required'
                    },
                    emailAddress: {
                        message: 'The input is not a valid email address'
                    }
                }
            },
            CompanyName: {
                validators: {
                    notEmpty: {
                        message: 'Company Name is required '
                    },
                }
            },
            
            PrimaryTypeOfBuisness: {
                // The group will be set as default (.form-group)
                validators: {
                    notEmpty: {
                        message: 'Primary Type Of Buisness is required'
                    }
                }
            },
           
            USe: {
                validators: {
                    notEmpty: {
                        message: 'Select a USe value'
                    }
                }
            },
            Location: {
                validators: {
                    notEmpty: {
                        message: 'Select a location value'
                    }
                }
            },
            AdditionalInfo: {
                // The group will be set as default (.form-group)
                validators: {
                    notEmpty: {
                        message: 'How can we help is required'
                    }
                }
            }

        }
    }).on('success.form.fv', function (e) {
        // Prevent form submission
        e.preventDefault();
    }).on('err.form.fv', function (e) {
        e.preventDefault();
    });
    objFormValidation = $('#PreSales').data('formValidation');

    $("body").mousemove(function () {
        $("#submit_btn").removeClass("disabled");
        $("#submit_btn").removeAttr("disabled");
    });

    //------------------------------------------
    $(document).ready(function () {
        staffSlider();
        scrollSlider();
    });
    (function () {
        var cx = '011230577157378110874:_crwdl6d2nu';
        var gcse = document.createElement('script');
        gcse.type = 'text/javascript';
        gcse.async = true;
        gcse.src = (document.location.protocol == 'https:' ? 'https:' : 'http:') +
            '//cse.google.com/cse.js?cx=' + cx;
        var s = document.getElementsByTagName('script')[0];
        s.parentNode.insertBefore(gcse, s);
    })();
});
