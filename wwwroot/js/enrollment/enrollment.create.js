$(document).ready(function () {
    $('#btnCreate').prop('disabled', true);
    $('#frmEnrollment .inputs').on('change', function () {

        $('#btnCreate').prop('disabled', false);
    });

    $('#frmEnrollment').submit(function (e) {
        e.preventDefault();

        var url = 'https://localhost:44309/Enrollments/Create';

        if ($(this).valid()) {
            var formData = $(this).serialize();
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
