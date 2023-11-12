$(document).ready(function () {
    setupEnrollmentForm();
});



function setupEnrollmentForm() {
    $('#btnUpdate').prop('disabled', true);
    $('#frmCourses .inputs').on('change', function () {
        $('#btnUpdate').prop('disabled', false);
    });

    $('#frmCourses').submit(function (e) {
        e.preventDefault();
        var id = $('#CourseID').val();
        console.log(id);
        var url = 'https://localhost:44309/Courses/Edit/' + id;

        if ($(this).valid()) {
            var formData = $(this).serialize();
            $.ajax({
                type: 'POST',
                url: url,
                data: formData,
                success: function (response) {
                    if (response.success) {
                        $('#successModalBody').text('Courses updated successfully');
                        $('#successModal').modal('show');

                        setTimeout(function () {
                            window.location.href = 'https://localhost:44309/Courses/Index';
                        }, 2000);
                    } else {
                        $('#errorModalBody').text('An error occurred while updating the courses');
                        $('#errorModal').modal('show');
                    }
                },
                error: function (error) {
                    console.error('Error:', error);
                }
            });
        }
    });
}

