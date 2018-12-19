//Link to guide: https://developer.mozilla.org/en-US/docs/Web/API/Geolocation_API //
// A simple HTML approach to getCurrentPosition: https://www.tutorialspoint.com/html5/geolocation_getcurrentposition.htm //
// API Key: 7df79b15f14cd152cc6a2a366d1be686 //
// API model schema documentation: https://developers.zomato.com/documentation#!/restaurant/search //

$(document).ready(function () {
    
    $("#locationbutton").on("click", function (event) {

        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showLocation);
        } else {
            alert("Sorry, browser does not support geolocation!");
        }

        function showLocation(position) {
            var latitude = position.coords.latitude;
            var longitude = position.coords.longitude;
            //alert("Latitude : " + latitude + " Longitude: " + longitude);

            var count = 10;
            var radius = 8000;
            //var name = restaurantSelection;
            var url = "https://developers.zomato.com/api/v2.1/search?count=" + count + "&lat=" + latitude + "&lon=" + longitude + "&radius=" + radius
            $.ajax({
                url: url,
                type: "GET",
                dataType: "json",
                data: {
                    "longitude": longitude,
                    "latitude": latitude,
                    "results_shown": count,
                    "apikey": "7df79b15f14cd152cc6a2a366d1be686",
                },
            }).done(function (result) {
                console.log(result);
                if (result.current_observation != undefined) {
                    var address = result.current_observation.address;
                    var response = " " + address + "< br />"
                    response += address;
                    $("#conditions").html(" " + response);
                    alert(response.toString());
                }
                else {
                    alert("dang");
                }
            });
        }       
    });    
});