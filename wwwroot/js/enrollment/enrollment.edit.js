$(document).ready(function () {

    $('#btnUpdate').prop('disabled', true);
    $('#frmEnrollment .inputs').on('change', function () {

        $('#btnUpdate').prop('disabled', false);
    });

    $('#frmEnrollment').submit(function (e) {
        e.preventDefault();
        var id = $('#EnrollmentID').val();
        console.log(id);
        var url = 'https://localhost:44309/Enrollments/Edit/' + id;

        var jwtToken = localStorage.getItem("jwt");
        console.log('JWT Token:', jwtToken);

        var headers = { Authorization: `Bearer ${jwtToken}` };
        console.log('Headers:', headers);

        if ($(this).valid()) {
            var formData = $(this).serialize();
            $.ajax({
                type: 'POST',
                url: url,
                data: formData,
                headers:headers,
                success: function (response) {
                    if (response.success) {
                        $('#successModalBody').text('Enrollment updated successfully');
                        $('#successModal').modal('show');

                        setTimeout(function () {
                            window.location.href = 'https://localhost:44309/Enrollments/Index';
                        }, 2000);
                    } else {
                        $('#errorModalBody').text('An error occurred while creating the enrollment');
                        $('#errorModal').modal('show');
                    }
                },
                error: function (error) {
                    console.error('Error:', error);
                }
            });
        }
    });

});