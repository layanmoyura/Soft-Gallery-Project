$(document).ready(function () {

    loadEnrollmentList();
});


function loadEnrollmentList() {
    var url = 'https://localhost:44309/Enrollments/IndexGet';

    var jwtToken = localStorage.getItem("jwt");
    console.log('JWT Token:', jwtToken);

    var headers = { Authorization: `Bearer ${jwtToken}` };
    console.log('Headers:', headers);

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        headers:headers,
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

