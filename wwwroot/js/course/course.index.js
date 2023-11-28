$(document).ready(function () {

    loadCoursesList();
});

function loadCoursesList() {
    var url = 'https://localhost:44309/Courses/IndexGet';

    var jwtToken = localStorage.getItem("jwt");
    console.log('JWT Token:', jwtToken);

    var headers = { Authorization: `Bearer ${jwtToken}` };
    console.log('Headers:', headers);

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        headers: headers,
        success: function (data) {
            $('#courseList').html(data);
        },
        error: function (error) {
            $('#errorModalBody').text('An error occurred while retrieving the courses');
            $('#errorModal').modal('show');
            console.error('Error:', error);

            setTimeout(function () {
                window.location.href = 'https://localhost:44309/Home';
            }, 2000);
        }
    });
}
