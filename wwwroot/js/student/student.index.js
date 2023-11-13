$(document).ready(function () {

    loadStudentsList();
});


function loadStudentsList() {
    var url = 'https://localhost:44309/Students/IndexGet';

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        success: function (data) {

            $('#studentList').html(data);
        },
        error: function (error) {
            $('#errorModalBody').text('An error occurred while retrieving the students');
            $('#errorModal').modal('show');
            console.error('Error:', error);
        }
    });
}    