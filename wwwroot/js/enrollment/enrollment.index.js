$(document).ready(function () {
    $.ajax({
        url: 'https://localhost:44309/Enrollments/IndexGet', // Use the correct route to your API endpoint
        type: 'GET',
        success: function (data) {
            if (data) {
               
                $('#enrollmentTable tbody').empty();
                
                console.log(data);

                var length = data.$values.length;

                for (var i = 0; i < length; i++) {
                    var item = data.$values[i];
                    console.log('item :',item);
                    var row = "<tr>";
                    row += "<td>" + item.enrollmentID + "</td>";
                    row += "<td>" + item.displayGrade+ "</td>";
                    row += "<td>" + item.course.title + "</td>";
                    row += "<td>" + item.student.lastName + "</td>";
                    row += "<td>" + item.enrollmentDate + "</td>";
                    row += "<td><a href='https://localhost:44309/Enrollments/Edit/" + item.enrollmentID + "'>Edit</a> | <a href='https://localhost:44309/Enrollments/Details/" + item.enrollmentID + "'>Details</a> | <a href='https://localhost:44309/Enrollments/Delete/" + item.enrollmentID + "'>Delete</a></td>";
                    row += "</tr>";
                    $('#enrollmentTable tbody').append(row);
                }

            } else {
                $('#errorModalBody').text('An error occurred. No data received.');
                $('#errorModal').modal('show');
            }
        }
    });
});
