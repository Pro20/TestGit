$(document).ready(function () {
    $("#btnSearch").click(function () { searchHubs(); });

});

function searchHubs() {
    var postData = {
        pickupAddress: $("#txtPickupAddress").val(),
        destinationAddress: $("#txtDestinationAddress").val(),
        distanceUnit: (($(".optDistanceUnit:checked").val() == undefined || $(".optDistanceUnit:checked").val() == "") ? "Mile" : $(".optDistanceUnit:checked").val()),
        findWithInKm: (($("#txtFindWithInKm").val() == undefined || $("#txtFindWithInKm").val() == "") ? "5" : $("#txtFindWithInKm").val()),
        availability: $("#cmbAvailable").val(),
        travelMode: (($(".optTravelMode:checked").val() == undefined || $(".optTravelMode:checked").val() == "") ? "Mile" : $(".optTravelMode:checked").val())
    }
    // console.log(postData);
    //return;
    //            console.log(x.pickupAddress);
    //            console.log(x.destinationAddress);
    //            console.log(x.optDistanceUnit);
    //            console.log(x.availability);
    //            console.log(x.travelMode);
    $.ajax({
        type: "POST", contentType: "application/json; charset=utf-8",
        url: "<%=URL%>/GetHub",
        data: JSON.stringify(postData),
        dataType: "json"
    }).success(function (data) {
        console.log(data);
        console.log("success");
    }).fail(function (data) {
        console.log(data);
        console.log("Fail");
    }).always(function (data) {
        console.log(data);
        console.log("Always");
    });
}

