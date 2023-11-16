document.getElementById('statusDropdown').addEventListener('change', function () {

    // Get the selected option value
    var selectedStatus = this.value;

    // Set the rating input value based on the selected status
    var ratingInput = document.getElementById('ratingInput');
    if (selectedStatus == '3') { // Plan to Read
        ratingInput.value = '1'; // Set a default value for Plan to Read
    } else {
        ratingInput.value = ''; // Clear the input for other statuses
    }
});