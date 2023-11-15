$(document).ready(function () {

    loadEnrollmentList();
});


function loadEnrollmentList() {
    var url = 'https://localhost:44309/Enrollments/IndexGet';

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        success: function (data) {

            $('#enrollmentList').html(data);
        },
        error: function (error) {
            $('#errorModalBody').text('An error occurred while retrieving the enrollments');
            $('#errorModal').modal('show');
            console.error('Error:', error);

        }
    });
}

