$(document).ready(function () {

    loadStudent();
});


function loadStudent() {
    var id = $('#ID').val();
    console.log(id);
    var url = 'https://localhost:44309/Students/DetailsGet/' + id;

    var jwtToken = localStorage.getItem("jwt");
    console.log('JWT Token:', jwtToken);

    var headers = { Authorization: `Bearer ${jwtToken}` };
    console.log('Headers:', headers);

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        headers:headers,
        success: function (data) {

            $('#studentDetails').html(data);
        },
        error: function (error) {
            $('#errorModalBody').text('An error occurred while retrieving the student');
            $('#errorModal').modal('show');
            console.error('Error:', error);
        }
    });
}    