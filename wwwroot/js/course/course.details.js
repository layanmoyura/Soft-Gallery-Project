$(document).ready(function () {

    loadCourse();
});


function loadCourse() {
    var id = $('#CourseID').val();
    console.log(id);
    var url = 'https://localhost:44309/Courses/DetailsGet/'+id;

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        success: function (data) {

            $('#courseDetails').html(data);
        },
        error: function (error) {
            $('#errorModalBody').text('An error occurred while retrieving the course');
            $('#errorModal').modal('show');
            console.error('Error:', error);
        }
    });
}    