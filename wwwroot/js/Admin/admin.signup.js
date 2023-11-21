$(document).ready(function () {
    
    setupAdminForm();
});

function setupAdminForm() {
    $('#frmAdmin #btnSignUp').prop('disabled', true);
    $('#frmAdmin .inputs').on('change', function () {
        $('#frmAdmin #btnSignUp').prop('disabled', false);
    });

    $('#frmAdmin').submit(function (e) {
        e.preventDefault();
        var formData = $(this).serialize();
        $.ajax({
            type: 'POST',
            url: 'https://localhost:44309/Admins/SignUp',
            data: formData,
            success: function (response) {
                console.log(response);
                if (response.success) {
                    $('#successModalBody').text('Signed Up successfully');
                    $('#successModal').modal('show');

                    setTimeout(function () {
                        window.location.href = 'https://localhost:44309/Admins/Login';
                    }, 2000);
                }
                else {
                    $('#errorModalBody').text('An error occurred while signing up');
                    $('#errorModal').modal('show');
                    console.log(error);
                }
            },
            error: function (error) {
                $('#errorModalBody').text('An error occurred while signing up');
                $('#errorModal').modal('show');
                console.error('Error:', error);
            }
        });
    });
}
