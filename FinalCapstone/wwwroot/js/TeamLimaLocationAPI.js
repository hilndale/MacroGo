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

            var restaurantChoice = $("#locationbutton").attr("name").toString();
            if (restaurantChoice.includes(" ")) {

                var restaurantChoice = restaurantChoice.replace(" ", "");

            }

            var url = "https://developers.zomato.com/api/v2.1/search?q=" + restaurantChoice + "&count=20&lat=" + latitude + "&lon=" + longitude + "&radius=16000&sort=real_distance&order=asc";

            $.ajax({
                url: url,
                type: "GET",
                dataType: "json",
                data: {
                    "longitude": longitude,
                    "latitude": latitude,
                    "results_shown": 5,
                    "apikey": "7df79b15f14cd152cc6a2a366d1be686",
                    "results_start": "0",
                    "start": "0",
                    "count": 10,
                    "q": restaurantChoice
                },
            }).done(function (result) {
                console.log(result);
                if (result != undefined) {

                    var response = [];

                    for (var i = 0; i < 5; i++) {
                        var restaurantName = result.restaurants[i].restaurant.name;
                        restaurantName = restaurantName.replace(" ", "");
                        restaurantName = restaurantName.replace("'", "");
                        var restaurantFilter = $("#locationbutton").attr("name");
                        restaurantFilter = restaurantFilter.replace(" ", "");
                        restaurantFilter = restaurantFilter.replace("'", "");
                        if (restaurantName == restaurantFilter && restaurantName.length == restaurantFilter.length) {

                            var restaurantAddress = result.restaurants[i].restaurant.location.address;
                            response += restaurantAddress + "<br />";
                        }
                    }

                    $("#conditions").html(response);

                }
            });
        }
    });
});
