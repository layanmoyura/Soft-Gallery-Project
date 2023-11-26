$(document).ready(function () {
    $('#deleteConfirmation').on('click', function (e) {
        $('#deleteConfirmationModal').modal('show');
    });

    $('#confirmDelete').on('click', function () {
        deleteStudent();
    });
});


function deleteStudent() {
    var id = $('#StudentID').val();

    var jwtToken = localStorage.getItem("jwt");
    console.log('JWT Token:', jwtToken);

    var headers = { Authorization: `Bearer ${jwtToken}` };
    console.log('Headers:', headers);

    console.log(id);

    $.ajax({
        type: 'POST',
        url: 'https://localhost:44309/Students/Delete/' + id,
        headers:headers,
        success: function (response) {
            if (response.success) {
                $('#successModalBody').text('Student deleted successfully');
                $('#successModal').modal('show');

                setTimeout(function () {
                    window.location.href = 'https://localhost:44309/Students/Index';
                }, 2000);
            } else {
                $('#errorModalBody').text('An error occurred while deleting the Student. Make sure theres no enrollment for this student');
                $('#errorModal').modal('show');
            }
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}
