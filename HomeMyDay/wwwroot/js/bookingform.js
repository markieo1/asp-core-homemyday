(function () {

    //Datepickers for birth date
    $('.birthdate input').datepicker({
        autoclose: true,
        immediateUpdates: true,
        format: 'dd/mm/yyyy'
    });

    $('.birthdate .datepickerbutton').click(function () {
        $(this).parent().find('input').datepicker('show');
    });


    //Get city from postal code
    $('#booking-form .housenumber, #booking-form .postalcode').blur(function (e) {
        let personform = $(this).closest('.bookingperson');

        let postalcode = personform.find('input.postalcode').val();

        //Get the city input field to autofill once the request completes
        let cityinput = personform.find('input.city')[0];

        if (postalcode) {
            cityinput.readOnly = true;
            getCityFromAddress(postalcode, cityinput);
        }
    });

    //Google API autocomplete
    function getCityFromAddress(postalcode, cityinput) {
        let searchString = postalcode;

        let geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'address': searchString }, function (results, status) {
            if (!status === 'OK') {
                //If autocomplete fails, unlock the city input.
                cityinput.readOnly = false;
                return;
            }

            let cityname = null;

            //Loop through the results until we find one in the Netherlands.
            for (i = 0; i < results.length; i++) {
                let result = results[i];

                //Loop through the address components and look for the Netherlands.
                for (j = 0; j < result.address_components.length; j++) {
                    if (result.address_components[j].short_name === 'NL') {
                        cityname = result.address_components[2].long_name;
                    }
                }
            }

            if (cityname !== null) {
                cityinput.value = cityname;
            }
            else {
                //If no city was found, unlock the city input.
                cityinput.readOnly = false;
            }
        });
    }

})();