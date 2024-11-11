

function LoginBtnClick() {
    var usernameValue = document.getElementById("username").value;
    var passwordValue = document.getElementById("password").value;

    $.ajax({
        url: '/Home/Login',
        method: 'POST',
        data: { username: usernameValue, password: passwordValue },
        success: function (data) {
            console.log(data);
            if (data.success) {
                if (data.role == "Manager") {
                    Swal.fire({
                        icon: 'success',
                        text: data.message,
                        background: '#111111',
                        timer: 2000,
                        showConfirmButton: false
                    });
                } else if (data.role = "Admin") {
                    Swal.fire({
                        icon: 'success',
                        text: data.message,
                        background: '#111111',
                        timer: 2000,
                        showConfirmButton: false
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        text: 'Kullanıcının Rolü Tanımlanamadı',
                        background: '#111111',
                        timer: 2000,
                        showConfirmButton: false
                    });
                }
            }
            else {
                Swal.fire({
                    icon: 'error',
                    text: data.message,
                    background: '#111111',
                    timer: 2000,
                    showConfirmButton: false
                });
            }
        },
        error: function (data) {
            console.log(data);
        }
    });

}