

function LoginBtnClick() {
    var usernameValue = document.getElementById("username").value;
    var passwordValue = document.getElementById("password").value;
    Swal.fire({
        icon: 'info',
        text: "Giriş Yapılıyor...",

        showConfirmButton: false,
        timerProgressBar: true,
        willOpen: () => {
            Swal.showLoading(); // Yükleme animasyonunu göster
        }

    });

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
                
                        timer: 2000,
                        showConfirmButton: false,
                        timerProgressBar: true,
                    }).then(() => {
                        window.location.replace('/Manager/Home/Index');
                    });
                } else if (data.role = "Admin") {
                    Swal.fire({
                        icon: 'success',
                        text: data.message,
                
                        timer: 2000,
                        showConfirmButton: false
                    }).then(() => {
                        window.location.replace('/Admin/Home/Index');
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        text: 'Kullanıcının Rolü Tanımlanamadı',
                
                        timer: 2000,
                        showConfirmButton: false
                    });
                }
            }
            else {
                Swal.fire({
                    icon: 'error',
                    text: data.message,
            
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