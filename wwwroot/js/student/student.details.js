$(document).ready(function () {

    loadCourse();
});


function loadCourse() {
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
            console.error('Error:', error);
        }
    });
}    