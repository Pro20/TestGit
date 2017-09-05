(function ($) {
    fakewaffle.responsiveTabs(['xs', 'sm']);
})(jQuery);
$(document).ready(function () {
    var url = window.location;
    // Will only work if string in href matches with location
    $('ul.nav a[href="' + url + '"]').parent().addClass('active');

    // Will also work for relative and absolute hrefs
    $('ul.nav a').filter(function () {
        return this.href == url;
    }).parent().addClass('active').parent().parent().addClass('active');
});
$(function () {
    jQuery('a[href*=#][class*="smooth"]:not([href=#])').click(function () {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var target = $(this.hash);
            target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
            if (target.length) {
                $('html,body').animate({
                    scrollTop: target.offset().top
                }, 1000);
                return false;
            }
        }
    });
});
$('.flaktek-menu li').not('.dropdown').click(function () {
    $('#flaktek-menu').removeClass('in');
});
$("#search-btn").click(function () {
    $("body").toggleClass("search-open");
    $('#flaktek-menu').removeClass('in');
    return false;
});
$("body *:not(#search-btn, header, header *)").click(function () {
    if ($("body.search-open").length > 0) {
        $("body").toggleClass("search-open");
        return false;
    }
});

$(function () {
    $(window).resize(function () {
        if (window.innerWidth > 1199) {
            $('.dropdown-toggle').removeAttr('data-toggle');
        }
    }).resize();
});

/* BOOTSTRAP MESSAGE */
(function ($) {
    $.fn.btSuccessMessage = function (html) {
        $(this).find("#btNormalMessage").remove();
        $(this).prepend('<div id="btNormalMessage" class="alert alert-success" role="alert">' + html + '</div>');
    }
    $.fn.btErrorMessage = function (html) {
        $(this).find("#btNormalMessage").remove();
        $(this).prepend('<div id="btNormalMessage" class="alert alert-danger" role="alert">' + html + '</div>');
    }
    $.fn.btInfoMessage = function (html) {
        $(this).find("#btNormalMessage").remove();
        $(this).prepend('<div id="btNormalMessage" class="alert alert-info" role="alert">' + html + '</div>');
    }
})($);
/* //BOOTSTRAP MESSAGE */

/* BT Alert/Confirm */
(function ($) {
    "use strict";
    $.btAlert = function (options) {
        $.alert({
            title: (options.title == "" || options.title === undefined) ? false : options.title,
            content: (options.content == "" || options.content === undefined) ? false : options.content,
            keyboardEnabled: (options.keyboardEnabled == "" || options.keyboardEnabled === undefined) ? true : options.keyboardEnabled,
            confirmButton: (options.okbuttontext == "" || options.okbuttontext === undefined) ? "Ok" : options.okbuttontext,
            cancelButton: false, //We don't required cancel button in simple alert
            confirm: function () {
                if (typeof options.yescallback === "function") {
                    options.yescallback();
                }
            },
            theme: 'white', //'supervan'
            animationBounce: 2.5, animationSpeed: 250, confirmButtonClass: 'btn-primary'
        });
    }
})($);
/* BT Alert/Confirm */

/*Captcha*/
(function ($) {
    "use strict";
    $.btApplyCaptcha = function () {
        if (!window['___grecaptcha_cfg']) { window['___grecaptcha_cfg'] = {}; };
        if (!window['___grecaptcha_cfg']['render']) { window['___grecaptcha_cfg']['render'] = 'onload'; };
        window['__google_recaptcha_client'] = true;
        var po = document.createElement('script');
        po.type = 'text/javascript';
        po.async = true;
        po.src = 'https://www.gstatic.com/recaptcha/api2/r20151013164303/recaptcha__en.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
        //console.log("rr" + s);
    }
})($);
/* /Catpcha */


//Show Loading Spinner and then Revert Back to the Orignal. You can set Right Tick sign as well
jQuery.fn.extend({
    showSpinOnButton: function (ShowRemoveDone) { //@ShowRemoveDone=> 1=Show Animation, 2=Remove Element, 3=Show Done
        console.log("OK");
        if (ShowRemoveDone === 1) $(this).prepend("<i class='fa fa-spinner fa-spin'>");
        if (ShowRemoveDone === 2) $(this).find("i").remove();
        if (ShowRemoveDone === 3) {
            $(this).find("i").remove();
            $(this).prepend("<i class=\"fa fa-check \"></i>");
            $(this).toggleClass("btn-primary btn-success");
        }
    }
});
(function ($) {
    $.fn.Spinner = function () {
        var initialITag = '';
        var self = this;
        this.Init = function () {
            initialITag = $(this).find("i")
        } //
        this.ShowLoading = function () {
            $(this).find("i").remove();
            $(this).prepend("<i class='fa fa-spinner fa-spin'>");
        }//
        this.SetBackToOrignal = function () {
            $(this).find("i").remove();
            $(this).prepend(initialITag);
        }
        this.ShowRightTick = function () {
            $(this).find("i").remove();
            $(this).prepend("<i class=\"fa fa-check \"></i>");
        }

        this.Init();
        return this;
    }
})($);