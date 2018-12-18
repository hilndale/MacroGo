//Link to guide: https://developer.mozilla.org/en-US/docs/Web/API/Geolocation_API //
// A simple HTML approach to getCurrentPosition: https://www.tutorialspoint.com/html5/geolocation_getcurrentposition.htm //
// API Key: 7df79b15f14cd152cc6a2a366d1be686 //
// API model schema documentation: https://developers.zomato.com/documentation#!/restaurant/search //


$(document).ready(function () {
    initCoords();
});

function initCoords() {
    if (navigator.geolocation) {
        getGeoLocation();
    } else {
        alert('Wrong browser.');
    }
}
function getGeoLocation() {
    navigator.geolocation.getCurrentPosition(updateLocation, errorHandler, { enableHighAccuracy: false, maximumAge: 60000, timeout: 27000 });
}
function updateLocation(position) {
    var longitude = position.coords.longitude;
    var latitude = position.coords.latitude;
    var count = 10;
    var radius = 8000;
    var name = restaurantSelection;
    $.ajax({
        url: "https://developers.zomato.com/api/v2.1/search?count=" + count + "&lat=" + latitude + "&lon=" + longitude + "&radius=" + radius,
        type: "get",
        dataType: "json",
        data: {
            "longitude": longitude,
            "latitude": latitude,
            "results_shown": count,
            "apikey": "7df79b15f14cd152cc6a2a366d1be686",

        },
        success: function (response) {
            console.log(response.message);
        },
        error: function (xmlhttprequest, textstatus, message) {
            console.log(message);
        }
    }).then(function () {
        setTimeout(getGeoLocation, 30);
    });
}

$(document).ready(function () {

    $("#BillingPostalCode").blur(function () {
        let subtotalStr = $("#subtotal span").html();
        let subtotal = subtotalStr.substring(1);
        let billingZipCode = $("#BillingPostalCode").val();

        $.ajax({
            url: "http://localhost:60980/api/GetTax",
            type: "GET",
            dataType: "json",
            data: {
                billingZipCode: billingZipCode,
                subtotal: subtotal
            },
            success: function (result) {
                console.log(result);
                $("#tax span").html("$" + result);
                result = parseFloat(result);
                subtotal = parseFloat(subtotal);
                result += subtotal;
                $("#grandtotal span").html("$" + result);
            }
        });
    });
});

function errorHandler(error) {
    console.log('Geolocation error : code ' + error.code + ' - ' + error.message);
}

