(function () {
    // Initialize the date picker for from and to 
    $('.departure-date-picker').datepicker({
        inputs: $('.input-group.date'),
        autoclose: true,
        immediateUpdates: true,
        showOnFocus: false,
        format: 'dd/mm/yyyy'
    });
})(window);
