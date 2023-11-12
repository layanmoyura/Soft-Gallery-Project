$(document).ready(function () {
    setupStudentForm();
});



function setupStudentForm() {
    $('#btnUpdate').prop('disabled', true);
    $('#frmStudents .inputs').on('change', function () {
        $('#btnUpdate').prop('disabled', false);
    });

    $('#frmStudents').submit(function (e) {
        e.preventDefault();
        var id = $('#ID').val();
        console.log(id);
        var url = 'https://localhost:44309/Students/Edit/' + id;

        if ($(this).valid()) {
            var formData = $(this).serialize();
            $.ajax({
                type: 'POST',
                url: url,
                data: formData,
                success: function (response) {
                    if (response.success) {
                        $('#successModalBody').text('Student updated successfully');
                        $('#successModal').modal('show');

                        setTimeout(function () {
                            window.location.href = 'https://localhost:44309/Students/Index';
                        }, 2000);
                    } else {
                        $('#errorModalBody').text('An error occurred while updating the student');
                        $('#errorModal').modal('show');
                    }
                },
                error: function (error) {
                    console.error('Error:', error);
                }
            });
        }
    });
}

