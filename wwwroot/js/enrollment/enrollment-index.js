$(document).ready(function () {
    $.ajax({
        url: 'https://localhost:44309/Enrollments/Get', // Use the correct route to your API endpoint
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
                    row += "<td>" + item.grade + "</td>";
                    row += "<td>" + item.course.title + "</td>";
                    row += "<td>" + item.student.lastName + "</td>";
                    row += "<td>" + item.enrollmentDate + "</td>";
                    row += "<td><a asp-action='Edit' asp-route-id='" + item.enrollmentID + "'>Edit</a> | <a asp-action='Details' asp-route-id='" + item.enrollmentID + "'>Details</a> | <a asp-action='Delete' asp-route-id='" + item.enrollmentID + "'>Delete</a></td>";
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
