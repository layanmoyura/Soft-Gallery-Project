$(document).ready(function () {
    
    setupLogInForm();
});

function setupLogInForm() {
    $('#frmAdmin #btnLogIn').prop('disabled', true);
    $('#frmAdmin .inputs').on('change', function () {
        $('#frmAdmin #btnLogIn').prop('disabled', false);
    });

    $('#frmAdmin').submit(function (e) {
        e.preventDefault();
        var formData = $(this).serialize();
        $.ajax({
            type: 'POST',
            url: 'https://localhost:44309/Admins/LogIn',
            data: formData,
            success: function (response) {
                if (response.success) {
                    console.log(response.token);
                    localStorage.setItem('jwt', response.token);
                    $('#successModalBody').text('Login successfully');
                    $('#successModal').modal('show');

                    setTimeout(function () {
                        window.location.href = 'https://localhost:44309/Home';
                    }, 2000);
                }
                else {
                    $('#errorModalBody').text('An error occurred while creating the course');
                    $('#errorModal').modal('show');
                    console.error('Error:', response.error);
                    
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
