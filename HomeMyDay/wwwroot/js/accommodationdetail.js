/**
 * Function will be automatically called by the Google API once the API has finished loading.
 */
function initMap()
{
    var mapElement = document.getElementById('accommodation-map');
    var coordinates = { lat: mapElement.dataset.latitude, long: mapElement.dataset.longitude };

    var map = new google.maps.Map(mapElement, {
        zoom: 4,
        center: coordinates
    });

    var marker = new google.maps.Marker({
        position: coordinates,
        map: map
    });
}