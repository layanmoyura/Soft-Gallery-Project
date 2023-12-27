$(document).ready(function () {
    
    setupStudentForm();

    $('#StudentNav').on('click', function () {
        studentFunc();
    });
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

function studentFunc() {
    var url = 'https://localhost:44309/Students/Index';

    var jwtToken = localStorage.getItem("jwt");
    console.log('JWT Token:', jwtToken);

    var headers = { Authorization: `Bearer ${jwtToken}` };
    console.log('Headers:', headers);

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        headers: headers,
        success: function (result) {
            console.log(result);
            $('#mainView').html(result);
        },
        error: function (error) {
            $('#errorModalBody').text('Please Log in first');
            $('#errorModal').modal('show');
            console.error('Error:', error);
        }
    });
}
