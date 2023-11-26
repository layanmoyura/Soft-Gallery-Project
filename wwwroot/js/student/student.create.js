$(document).ready(function () {
    
    setupStudentForm();
});

function setupStudentForm() {
    $('#frmStudent #btnCreate').prop('disabled', true);
    $('#frmStudent .inputs').on('change', function () {
        $('#frmStudent #btnCreate').prop('disabled', false);
    });

    $('#frmStudent').submit(function (e) {
        e.preventDefault();

        var jwtToken = localStorage.getItem("jwt");
        console.log('JWT Token:', jwtToken);

        var headers = { Authorization: `Bearer ${jwtToken}` };
        console.log('Headers:', headers);

        var formData = $(this).serialize();
        $.ajax({
            type: 'POST',
            url: 'https://localhost:44309/Students/Create',
            data: formData,
            headers:headers,
            success: function (response) {
                if (response.success) {
                    $('#successModalBody').text('Student created successfully');
                    $('#successModal').modal('show');

                    setTimeout(function () {
                        window.location.href = 'https://localhost:44309/Students/Index';
                    }, 2000);
                } else {
                    $('#errorModalBody').text('An error occurred while creating the student');
                    $('#errorModal').modal('show');
                }
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });
    });
}
