$(document).ready(function () {
    $('#deleteConfirmation').on('click', function (e) {

        $('#deleteConfirmationModal').modal('show');
    });

    $('#confirmDelete').on('click', function () {

        var enrollmentID = $('#EnrollmentID').text();
        var value = enrollmentID.trim();
        var id = parseInt(value, 10);

        console.log(id);

        var jwtToken = localStorage.getItem("jwt");
        console.log('JWT Token:', jwtToken);

        var headers = { Authorization: `Bearer ${jwtToken}` };
        console.log('Headers:', headers);



        $.ajax({
            type: 'POST',
            url: 'https://localhost:44309/Enrollments/Delete/' + id,
            headers:headers,
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
