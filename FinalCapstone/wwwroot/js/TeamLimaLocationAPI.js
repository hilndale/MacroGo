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
    $.ajax({
        url: "../../ajax/account/handlegeolocation",
        type: "post",
        dataType: "json",
        data: { "longitude": longitude, "latitude": latitude },
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

function errorHandler(error) {
    console.log('Geolocation error : code ' + error.code + ' - ' + error.message);
}

