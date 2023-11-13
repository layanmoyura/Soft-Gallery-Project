$(document).ready(function () {

    loadStudent();
});


function loadStudent() {
    var id = $('#ID').val();
    console.log(id);
    var url = 'https://localhost:44309/Students/DetailsGet/'+id;

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
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