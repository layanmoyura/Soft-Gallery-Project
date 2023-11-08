$(document).ready(function () {


    $(document).on('click', '#btnUpdate', function (e) {
        var id = $('#EnrollmentID').val();
        console.log(id);
        var url = 'https://localhost:44309/Enrollments/Edit/' + id;
        var formData = $('#frmEnrollement').serialize();

        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
            success: function (response) {
                console.log(response);
                if (response.success) {
                    
                    $('#successModalBody').text(response.message);
                    $('#successModal').modal('show');

                    
                    setTimeout(function () {
                        window.location.href = 'https://localhost:44309/Enrollments/Index';
                    }, 2000);
                } else {
                    
                    $('#errorModalBody').text(response.message);
                    $('#errorModal').modal('show');
                }
            },
            error: function (error) {
                
                console.error('Error:', error);
            }
        });
    });

});