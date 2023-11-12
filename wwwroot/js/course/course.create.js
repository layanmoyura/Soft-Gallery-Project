$(document).ready(function () {
    
    setupCourseForm();
});

function setupCourseForm() {
    $('#frmCourse #btnCreate').prop('disabled', true);
    $('#frmCourse .inputs').on('change', function () {
        $('#frmCourse #btnCreate').prop('disabled', false);
    });

    $('#frmCourse').submit(function (e) {
        e.preventDefault();
        var formData = $(this).serialize();
        $.ajax({
            type: 'POST',
            url: 'https://localhost:44309/Courses/Create',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $('#successModalBody').text('Course created successfully');
                    $('#successModal').modal('show');

                    setTimeout(function () {
                        window.location.href = 'https://localhost:44309/Courses/Index';
                    }, 2000);
                } else {
                    $('#errorModalBody').text('An error occurred while creating the course');
                    $('#errorModal').modal('show');
                }
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });
    });
}
