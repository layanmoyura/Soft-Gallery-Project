$(document).ready(function () {
    $.ajax({
        url: 'https://localhost:44309/Enrollments', // Replace ControllerName with your controller name
        type: 'GET',
        success: function (data) {
            if (data) {
                $('#enrollmentTable').html(data);
            } else {
                
                $('#errorModalBody').text('An error occurred. No data received.');
                $('#errorModal').modal('show');
            }
        }
    });
});