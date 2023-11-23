$(document).ready(function () {

    loadCoursesList();    
});


function loadCoursesList() {
    var url = 'https://localhost:44309/Courses/IndexGet';

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        headers: { Authorization: `Bearer ${localStorage.getItem('jwt')}` },
        success: function (data) {

            $('#courseList').html(data);
        },
        error: function (error) {
            $('#errorModalBody').text('An error occurred while retrieving the courses');
            $('#errorModal').modal('show');
            console.error('Error:', error);

        }
    });
}    

