document.querySelectorAll('.statusDropdown').forEach(function (dropdown) {
    dropdown.addEventListener('change', function () {

        // Get the selected option value
        var selectedStatus = this.value;

        // Set the rating input value based on the selected status
        var ratingInput = this.closest('.modal-body').querySelector('.ratingInput');
        if (selectedStatus == '3') { // Plan to Read
            ratingInput.value = '0'; // Set a default value for Plan to Read
        } else {
            ratingInput.value = ''; // Clear the input for other statuses
        }
    })

   
});