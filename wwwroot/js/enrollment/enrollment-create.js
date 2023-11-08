$(document).ready(function () {
    $('#frmEnrollment').submit(function (e) {


        if ($(this).valid()) {

            var formData = $(this).serialize();
            var url = 'https://localhost:44309/Enrollments/Create/'
            $.ajax({
                type: 'POST',
                url: url,
                data: formData,
                success: function (response) {

                    if (response.success) {

                        $('#successModalBody').text('Enrollment created successfully');
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