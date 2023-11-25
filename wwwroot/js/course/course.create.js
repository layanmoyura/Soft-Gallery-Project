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

        var jwtToken = localStorage.getItem("jwt");
        console.log('JWT Token:', jwtToken);

        var headers = { Authorization: `Bearer ${jwtToken}` };
        console.log('Headers:', headers);

        var formData = $(this).serialize();
        $.ajax({
            type: 'POST',
            url: 'https://localhost:44309/Courses/Create',
            data: formData,
            headers:headers,
            success: function (response) {
                if (response.success) {
                    $('#successModalBody').text('Course created successfully');
                    $('#successModal').modal('show');

                    setTimeout(function () {
                        window.location.href = 'https://localhost:44309/Courses/Index';
                    }, 2000);
                } 
            },
            error: function (error) {
                $('#errorModalBody').text('An error occurred while creating the course');
                $('#errorModal').modal('show');
                console.error('Error:', error);
            }
        });
    });
}
