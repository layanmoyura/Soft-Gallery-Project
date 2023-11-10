$(document).ready(function () {
    $('#frmEnrollment').submit(function (e) {
        e.preventDefault(); 

        var url = 'https://localhost:44309/Enrollments/Create'

        
        if ($(this).valid()) {
            
            var formData = $(this).serialize();
            $.ajax({
                type: 'POST',
                url: url,
                data: formData,
                success: function (response) {
                    console.log(response)
                    if (response.success) {
                        console.log(response)
                        $('#successModalBody').text('Enrollment created successfully');
                        $('#successModal').modal('show');
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
