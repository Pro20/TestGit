var appFun = appFun ? appFun : {};
appFun.global = appFun.global ? appFun.global : {};

appFun.global.messageOnTopError = function (heading, message) {
    $(".messageOnTop").remove();
    $("body").prepend('<div class="errorTopMessage messageOnTop"> <h3>' + heading + '</h3> <p>' + message + '</p> </div>');
    $(".messageOnTop").animate({ top: "0" }, 500);
    $('.messageOnTop').click(function () {
        $(this).animate({ top: -$(this).outerHeight() }, 500);
    });
}
appFun.global.messageOnTopSuccess = function (heading, message) {
    $(".messageOnTop").remove();
    $("body").prepend('<div class="successTopMessage messageOnTop"> <h3>' + heading + '</h3> <p>' + message + '</p> </div>');
    $(".messageOnTop").animate({ top: "0" }, 500);
    $('.messageOnTop').click(function () {
        $(this).animate({ top: -$(this).outerHeight() }, 500);
    });
}

$(document).ready(function () {
    setInterval(function () {
        //date = moment(new Date()).utc().add(new Date(), 'hours');
        //$("#currentDateTime").html(date.format('MM/DD/YYYY, h:mm:ss A')); date = null;
        var d = new Date(),
        seconds = d.getSeconds().toString().length == 1 ? '0' + d.getSeconds() : d.getSeconds(),
        minutes = d.getMinutes().toString().length == 1 ? '0' + d.getMinutes() : d.getMinutes(),
        hours = d.getHours().toString().length == 1 ? '0' + d.getHours() : d.getHours(),
        ampm = d.getHours() >= 12 ? 'pm' : 'am',
        months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        days = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
        $("#currentDateTime").html(days[d.getDay()] + ' ' + months[d.getMonth()] + ' ' + d.getDate() + ' ' + d.getFullYear() + ' ' + hours + ':' + minutes + ':' + seconds + ':' + ampm);
    }, 1000);

    //Note: Check JQuery Version
    //if (typeof jQuery != 'undefined') {
    //    // jQuery is loaded => print the version
    //    alert(jQuery.fn.jquery);
    //}

    if ($.isFunction($.fn.multiSelect)) {
        $('.cmbMultipleSelection').multiSelect({ selectableOptgroup: true });
    }
    if ($.isFunction($.fn.datepicker)) {
        $('.dtPicker').datepicker({ autoclose: true, todayHighlight: true });
    }
});

$(window).load(function () {
    $(".chkItem").on("click", function () {ShowHidePageActionBar() });

});


function ShowHidePageActionBar() {
    if ($(".chkItem:checked").length > 0) {
        if ($("#PageActionBar").is(':visible') === false) {
            $("#PageActionBar").show("slide", { direction: "right" }, 500);
        }
    } else {
        $("#PageActionBar").hide("slide", { direction: "right" }, 500);
    }
}

function ShowHidePageActionBar2() {
    if ($(".chkItem:checked").length > 0) {
        console.log("2");
        if ($("#pageActionBar").is(':visible') === false) {
            $("body").addClass("full-content") //To Add PageActionBar
            $("#pageActionBar").removeClass("hidden");
            $("#pageActionBar").show();
        } 
    } else {
        $("body").removeClass("full-content") //To Remove PageActionBar
        $("#pageActionBar").addClass("hidden");
        $("#pageActionBar").hide();
    }
}

/* Extended Function */
(function ($) {
    $.fn.ActivateLeftMenu = function () { //USAGE: $("#mnuListAllContacts").ActivateLeftMenu();
        console.log(this);
        $(this).addClass("active").parent().addClass("active expanded").parent().parent().addClass("active expanded");
    }
    //********************************
    $.fn.CheckUncheckAll = function (chkItemClassName) { //USAGE: $("#chkAll").CheckUncheckAll("chkItem");
        $(this).change(chkItemClassName,function () {
            var table = $(this).closest('table');
            $('td input.' + chkItemClassName + ':checkbox', table).prop('checked', this.checked);
        })
    }
    //********************************
})(jQuery);
/* /Extended Function */

/* Global Function */
var btMessage = {
    success: function ( ) {
        toastr.success('Added to catalog', 'This is xklhfslfh');
    }
}

/* /Global Function */