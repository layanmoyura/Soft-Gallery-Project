     
$(document).ready(function () {
    // Make an AJAX GET request to your controller action
    $.ajax({
        type: 'GET',
        url: 'https://localhost:44309/Enrollments/Index', // Replace 'YourController' with your actual controller name
        dataType: 'json',
        success: function (data) {
            if (data.success) {
                console.log(data.enrollmentModels)
                var enrollments = data.enrollmentModels;

                
                $('#enrollmentTable tbody').empty();

               
                enrollments.forEach(function (enrollment) {
                    $('#enrollmentTable tbody').append(
                        '<tr>' +
                        '<td>' + enrollment.EnrollmentID + '</td>' +
                        '<td>' + enrollment.Grade + '</td>' +
                        '<td>' + enrollment.Course.Title + '</td>' +
                        '<td>' + enrollment.Student.LastName + '</td>' +
                        '<td>' + enrollment.EnrollmentDate + '</td>' +
                        '</tr>'
                    );
                });

            }
        },
        error: function (error) {
            console.log(error);
            console.error('Error:', error);
        }
    });
});

