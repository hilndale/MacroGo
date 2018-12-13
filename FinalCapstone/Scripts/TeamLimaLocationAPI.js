//Link to guide: https://developer.mozilla.org/en-US/docs/Web/API/Geolocation_API //
// A simple HTML approach to getCurrentPosition: https://www.tutorialspoint.com/html5/geolocation_getcurrentposition.htm //

// Native function that quickly returns geolocation (lat and long), but with low accuracy //
navigator.geolocation.getCurrentPosition(function(position) {
  do_something(position.coords.latitude, position.coords.longitude);
});

// Native function that is slower than getCurrentPosition, but more accurate (multiple calls for geolocation)//
var watchID = navigator.geolocation.watchPosition(function(position) {
  do_something(position.coords.latitude, position.coords.longitude);
});

// Stop making calls to find geolocation of user //
navigator.geolocation.clearWatch(watchID);

// Full call of output from getCurrentPosition and/or watchPosition, //
//including geo_options: params on geolocation accuracy, timeout, and maximumAge (maximum milliseconds since last call, where geolocation value is used) //
function geo_success(position) {
  do_something(position.coords.latitude, position.coords.longitude);
}

function geo_error() {
  alert("Sorry, no position available.");
}

var geo_options = {
  enableHighAccuracy: true, 
  maximumAge        : 30000, 
  timeout           : 27000
};

var wpid = navigator.geolocation.watchPosition(geo_success, geo_error, geo_options);

//Full JS call of function//

function geoFindMe() {
  var output = document.getElementById("out");

  if (!navigator.geolocation){
    output.innerHTML = "<p>Geolocation is not supported by your browser</p>";
    return;
  }

  function success(position) {
    var latitude  = position.coords.latitude;
    var longitude = position.coords.longitude;

    output.innerHTML = '<p>Latitude is ' + latitude + '° <br>Longitude is ' + longitude + '°</p>';

  }

  function error() {
    output.innerHTML = "Unable to retrieve your location";
  }

  output.innerHTML = "<p>Locating…</p>";

  navigator.geolocation.getCurrentPosition(success, error);
}

// The website goes on to create a custom alert window asking for permission to use location //
// But I expect we could make a click event that is simpler, for our use //