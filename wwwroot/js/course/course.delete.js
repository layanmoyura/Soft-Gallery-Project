$(document).ready(function () {
    $('#deleteConfirmation').on('click', function (e) {
        $('#deleteConfirmationModal').modal('show');
    });

    $('#confirmDelete').on('click', function () {
        deleteCourse();
    });
});


function deleteCourse() {
    var CourseID = $('#CourseID').val();
    

    console.log(id);

    var jwtToken = localStorage.getItem("jwt");
    console.log('JWT Token:', jwtToken);

    var headers = { Authorization: `Bearer ${jwtToken}` };
    console.log('Headers:', headers);

    $.ajax({
        type: 'POST',
        url: 'https://localhost:44309/Courses/Delete/' + id,
        headers:headers,
        success: function (response) {
            if (response.success) {
                $('#successModalBody').text('Course deleted successfully');
                $('#successModal').modal('show');

                setTimeout(function () {
                    window.location.href = 'https://localhost:44309/Courses/Index';
                }, 2000);
            } else {
                $('#errorModalBody').text('An error occurred while deleting the course. Make sure theres no enrollment for this course');
                $('#errorModal').modal('show');
            }
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}
