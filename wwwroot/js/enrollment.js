$(document).ready(function () {

    var courseId = $('.course').val();
    console.log('course id: ', courseId);

    $(document).on('change', '.course', function () {
        console.log($(this).val());
    });

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


    $(document).ready(function () {
        $('#frmEnrollment').submit(function (e) {
            
            
            if ($(this).valid()) {
                
                var formData = $(this).serialize();
                var url = 'https://localhost:44309/Enrollments/Create/'
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


    $(document).ready(function () {
        $('#deleteConfirmation').on('click', function (e) {
            
            $('#deleteConfirmationModal').modal('show');
        });

        $('#confirmDelete').on('click', function () {
            
            var enrollmentID = $('#EnrollmentID').text();
            var value = enrollmentID.trim();
            var id = parseInt(value, 10); 

            console.log(id);

            
            
            $.ajax({
                type: 'POST',
                url: 'https://localhost:44309/Enrollments/Delete/' + id,
                success: function (response) {
                   
                    if (response.success) {
                        
                        $('#successModalBody').text('Enrollment deleted successfully');
                        $('#successModal').modal('show');

                       
                        setTimeout(function () {
                            window.location.href = 'https://localhost:44309/Enrollments/Index';
                        }, 2000);
                    } else {
                       
                        $('#errorModalBody').text('An error occurred while deleting the enrollment');
                        $('#errorModal').modal('show');
                    }
                },
                error: function (error) {
                    
                    console.error('Error:', error);
                }
            });
        });
    });




});