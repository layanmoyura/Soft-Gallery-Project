$(document).ready(function () {

    var id = $('#enrollmentID').text();
    console.log(id);

    var jwtToken = localStorage.getItem("jwt");
    console.log('JWT Token:', jwtToken);

    var headers = { Authorization: `Bearer ${jwtToken}` };
    console.log('Headers:', headers);

    $.ajax({
        
        
        url: 'https://localhost:44309/Enrollments/DetailsGet/' + id,
        type: 'GET',
        headers:headers,
       
        success: function (data) {
            console.log(data);
            if (data) {
                
                
                $('#enrollmentID').text(data.enrollmentID);
                $('#grade').text(data.displayGrade);

                $('#courseCredits').text(data.course.credits);
                $('#courseTitle').text(data.course.title);

                $('#studentFirstName').text(data.student.firstMidName);
                $('#studentLastName').text(data.student.lastName);
                $('#studentJoinedDate').text(data.student.joinedDate);
            }
        },
        error: function (error) {
            $('#errorModalBody').text('An error occurred while retrieving the course');
            $('#errorModal').modal('show');
            console.error('Error:', error);
        }
    });
});