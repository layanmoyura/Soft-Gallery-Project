$(document).ready(function () {
    $('#deleteConfirmation').on('click', function (e) {

        $('#deleteConfirmationModal').modal('show');
    });

    $('#confirmDelete').on('click', function () {

        var enrollmentID = $('#EnrollmentID').text();
        var value = enrollmentID.trim();
        var id = parseInt(value, 10);

        console.log(id);



        $.ajax({
            type: 'POST',
            url: 'https://localhost:44309/Enrollments/Delete/' + id,
            success: function (response) {

                if (response.success) {

                    $('#successModalBody').text('Enrollment deleted successfully');
                    $('#successModal').modal('show');


                    setTimeout(function () {
                        window.location.href = 'https://localhost:44309/Enrollments/Index';
                    }, 2000);
                } else {

                    $('#errorModalBody').text('An error occurred while deleting the enrollment');
                    $('#errorModal').modal('show');
                }
            },
            error: function (error) {

                console.error('Error:', error);
            }
        });
    });
});
