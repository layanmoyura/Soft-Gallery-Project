$(document).ready(function () {
    $('#btnLogOut').on('click', function () {
        logoutFunc();
    });
});

function logoutFunc() {
    var url = 'https://localhost:44309/Home';

   
    var token = localStorage.getItem('jwt');

    if (token) {
        
        localStorage.removeItem('jwt');

        $('#successModalBody').text('You are logged out');
        $('#successModal').modal('show');


        setTimeout(function () {
            window.location.href = url ;
        }, 2000);
    } else {
        
        $('#successModalBody').text('You are already logged out');
        $('#successModal').modal('show');

        setTimeout(function () {
            window.location.href = url;
        }, 2000);
    }
}
