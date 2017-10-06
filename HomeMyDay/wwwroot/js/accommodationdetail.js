/**
 * Function will be automatically called by the Google API once the API has finished loading.
 */
function initMap()
{
    var mapElement = document.getElementById('accommodation-map');

    //Get latitude and longitude
    //Google API expects a number.
    var lat = parseFloat(mapElement.dataset.latitude.replace(',', '.'));
    var lng = parseFloat(mapElement.dataset.longitude.replace(',', '.'));
    var coordinates = { lat: lat, lng: lng };

    var map = new google.maps.Map(mapElement, {
        zoom: 12,
        center: coordinates
    });

    var marker = new google.maps.Marker({
        position: coordinates,
        map: map
    });
}