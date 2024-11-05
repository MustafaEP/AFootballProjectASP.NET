$(document).ready(function () {
    $.ajax({
        url: '/Admin/Match/GetMatches',
        method: 'GET',
        success: function (data) {
            console.log(data);
        }
    });
});