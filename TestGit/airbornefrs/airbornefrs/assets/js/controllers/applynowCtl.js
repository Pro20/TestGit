var objFormValidation;

btApp.controller('applynowCtl', function ($scope, $http) {
    $scope.title = "";
    $scope.data = objApplynow;
    $scope.parent = { data: '' };
    $scope.resetform = function () {        
    }

    $scope.submitForm = function () {
        if (!objFormValidation.isValid()) {
            return false;
        }
        objFormValidation.disableSubmitButtons(false);
        
        $("#submit_btn").removeClass();
        if ($scope.data.applynowData.Allowforcontact == false) {
            $.btAlert({ title: 'Allow for contact', content: 'You must have to check allow contact option.' });
            return false;
        }

        $scope.submitted = true;
       // $("#submit_btn").add.("<i class='fa fa-spinner fa-spin'></i>");
        console.log($("#g-recaptcha-response").val());
        $("#submit_btn").showSpinOnButton(1);
        $scope.data.reCaptchaResponse = $("#g-recaptcha-response").val();
        $http({
            url: '/Applynow/SaveDetails',
            method: 'POST',
            data: $scope.data,
            headers: { 'content-type': 'application/json' }
        }).then(function (responseData) {
            console.log(responseData);
            $("#submit_btn").showSpinOnButton(2);
            if (responseData.data == true || responseData.data == "true") {
                $.btAlert({ title: 'Details Has Been Saved', content: 'We have received your details and will contact you as soon as possible.' });
                $scope.data = objApplynow.applynowData;
            }
            else if (responseData.data == "3" || parseInt(responseData.data) == 3) {
                $.btAlert({ title: 'Human Verification', content: 'Please verify that you are not a robot.' });
            }
            else {
                $.btAlert({ title: 'Unforeseen error occurred', content: 'unforeseen error occurred. Please try later.' });
            }
            return responseData;
        });
    }
});



( function () {
    if (!window['___grecaptcha_cfg']) { window['___grecaptcha_cfg'] = {}; };
    if (!window['___grecaptcha_cfg']['render']) { window['___grecaptcha_cfg']['render'] = 'onload'; };
    window['__google_recaptcha_client'] = true;
    var po = document.createElement('script');
    po.type = 'text/javascript';
    po.async = true;
    po.src = 'https://www.gstatic.com/recaptcha/api2/r20151013164303/recaptcha__en.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
    console.log("rr"+s);
})();



$(document).ready(function () {
    //$.btApplyCaptcha();
     $('#applynowForm').formValidation({
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
            fname: {
                validators: {
                    notEmpty: {
                        message: 'The first name is required'
                    }
                    //,
                    //regexp: {
                    //    regexp: /^[a-zA-Z0-9_\.]+$/,
                    //    message: 'First name can only consist of alphabetical, number, dot and underscore'
                    //}
                }
            },
            lname: {
                validators: {
                    notEmpty: {
                        message: 'Last name is required'
                    }
                    //,
                    ////stringLength: {
                    ////    min: 6,
                    ////    max: 30,
                    ////    message: 'The last name must be more than 6 and less than 30 characters long'
                    ////},
                    //regexp: {
                    //    regexp: /^[a-zA-Z0-9_\.]+$/,
                    //    message: 'Last can only consist of alphabetical, number, dot and underscore'
                    //}
                }
            }
                ,
            email: {
                validators: {
                    notEmpty: {
                        message: 'email address is required'
                    },
                    emailAddress: {
                        message: 'The input is not a valid email address'
                    }
                }
            },
            telephone: {
                validators: {
                    notEmpty: {
                        message: 'Telephone is required'
                    },
                    regexp: {
                        regexp: /^[0-9_\.]+$/,
                        message: 'The telephone can only consist of number'
                    }
                }
            },
            year: {
                validators: {
                    notEmpty: {
                        message: 'Please select an option for Years of CDL Experience'
                    },
                }
            },
            capable: {
                // The group will be set as default (.form-group)
                validators: {
                    notEmpty: {
                        message: 'Please select yes/no for HAZMAT capable'
                    }
                }
            },
            state: {
                validators: {
                    notEmpty: {
                        message: 'Your home base state is required '
                    },
                }
            },
            allow: {
                validators: {
                    notEmpty: {
                        message: 'Please tick the above checkbox'
                    },
                }
            },
            landline: {
                // The group will be set as default (.form-group)
                validators: {
                    notEmpty: {
                        message: 'Please check the telephone type'
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
     objFormValidation = $('#applynowForm').data('formValidation');
    
     $("body").mousemove(function () {
         $("#submit_btn").removeClass("disabled");
         $("#submit_btn").removeAttr("disabled");
     });
});


