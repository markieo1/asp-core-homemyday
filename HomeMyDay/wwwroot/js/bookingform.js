(function () {

    //Disable all hidden input fields
    $('.bookingperson.hidden input, .bookingperson.hidden select').prop('disabled', true);

    //Disable hidden baggage inputs
    $('.person-baggage.hidden input').prop('disabled', true);

    //Datepicker for birth date
    $('.birthdate input').datepicker({
        autoclose: true,
        immediateUpdates: true,
        format: 'dd/mm/yyyy'
    });

    //Add booker to form
    $('#add-booker-button').click(function() {
        //Get the first hidden booker section and re-enable it
        let bookerSection = $('.bookingperson.hidden').first();
        bookerSection.removeClass('hidden');
        bookerSection.find('input, select').prop('disabled', false);

        let personIndex = bookerSection.data('person-index');

        //Get the correct baggage section and re-enable it
        let baggageSection = $('.person-baggage[data-person-index="'+ personIndex +'"]');
        baggageSection.removeClass('hidden');
        baggageSection.find('input, select').prop('disabled', false);

        checkButtonVisibility();
    });

    $('.remove-booker').click(function() {
        //Hide the last person section and disable inputs
        let bookerSection = $(this).closest('.bookingperson');
        bookerSection.addClass('hidden');
        bookerSection.find('input, select').prop('disabled', true);

        let personIndex = bookerSection.data('person-index');

        //Hide the correct baggage section and disable inputs
        let baggageSection = $('.person-baggage[data-person-index="' + personIndex + '"]');
        baggageSection.addClass('hidden');
        baggageSection.find('input, select').prop('disabled', true);

        checkButtonVisibility();
    });

    /**
     * Checks if bookers can still be added. If so, enables the "add booker" button. Otherwise, hides it.
     * Also hides all "remove booker" buttons except the last.
     */
    function checkButtonVisibility()
    {
        //Show/hide "add booker" button
        if($('.bookingperson.hidden').length > 0)
        {
            $('#add-booker-button').removeClass('hidden');
        }
        else
        {
            $('#add-booker-button').addClass('hidden');
        }

        //Show only the last "remove booker" button
        $('.remove-booker').addClass('hidden');
        $('.bookingperson:not(.hidden)').last().find('.remove-booker').removeClass('hidden');
    }

    //Get city from postal code
    $('#booking-form .housenumber, #booking-form .postalcode').blur(function(e) {
        let personform = $(this).closest('.bookingperson');

        let postalcode = personform.find('input.postalcode').val();

        //Get the city input field to autofill once the request completes
        let cityinput = personform.find('input.city')[0];

        if(postalcode)
        {
            cityinput.readOnly = true;
            getCityFromAddress(postalcode, cityinput);
        }
    });

    //Google API autocomplete
    function getCityFromAddress(postalcode, cityinput)
    {
        let searchString = postalcode;

        let geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'address': searchString }, function(results, status) {
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

    checkButtonVisibility();

})();