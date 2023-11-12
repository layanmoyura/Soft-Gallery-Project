$(document).ready(function () {

    var id = $('#enrollmentID').text();
    console.log(id);
    $.ajax({
        
        
        url: 'https://localhost:44309/Enrollments/DetailsGet/' + id,
        type: 'GET',
       
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
            console.error('Error:', error);
        }
    });
});