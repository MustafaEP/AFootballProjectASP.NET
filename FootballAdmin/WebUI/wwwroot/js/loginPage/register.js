
function RegisterBtnClick() {
    var isCorrectUserName = true;
    var isCorrectName = true;
    var isCorrectSurName = true;
    var isCorrectPassword = true;
    var isCorrectEmail = true;

    var usernameValue = document.getElementById("username").value;
    var nameValue = document.getElementById("name").value;
    var surNameValue = document.getElementById("surName").value;
    var emailValue = document.getElementById("email").value;
    var passwordValue = document.getElementById("password").value;
    var passwordAgainValue = document.getElementById("passwordAgain").value;

    if (usernameValue.trim().length === 0) {
        document.getElementById("usernameSpan").innerText = "Kullanıcı Adı Boş Bırakılamaz";
        isCorrectUserName = false;
    }
    else if (usernameValue.trim().length < 2) {
        document.getElementById("usernameSpan").innerText = "Kullanıcı Adı 2 Karakterden Az Olamaz";
        isCorrectUserName = false;

    }
    else if (usernameValue.trim().length > 15) {
        document.getElementById("usernameSpan").innerText = "Kullanıcı Adı 15 Karakterden Fazla Olamaz";
        isCorrectUserName = false;
    }
    else {
        document.getElementById("usernameSpan").innerText = "";
        isCorrectUserName = true;
    }

    nameValue = nameValue.trim();
    if (nameValue.length === 0) {
        document.getElementById("nameSpan").innerText = "Ad Boş Bırakılamaz";
        isCorrectName = false;
    }
    else if (nameValue.length < 2) {
        document.getElementById("nameSpan").innerText = "Ad 2 Karakterden Az Olamaz";
        isCorrectName = false;
    }
    else {
        document.getElementById("nameSpan").innerText = "";
        isCorrectName = true;
    }

    surNameValue = surNameValue.trim();
    if (surNameValue.length === 0) {
        document.getElementById("surNameSpan").innerText = "Soyad Boş Bırakılamaz";
        isCorrectSurName = false;
    }
    else if (surNameValue.length < 2) {
        document.getElementById("surNameSpan").innerText = "Soyad 2 Karakterden Az Olamaz";
        isCorrectSurName = false;
    }
    else {
        document.getElementById("surNameSpan").innerText = "";
        isCorrectSurName = true;
    }

    var emailValue = document.getElementById("email").value;
    var emailSpan = document.getElementById("emailSpan");

    // E-posta regex pattern (sadece basit bir doğrulama için)
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

    if (emailValue.length === 0) {
        emailSpan.innerText = "E-posta boş bırakılamaz";
        isCorrectEmail = false;
    }
    else if (emailValue.length < 5) {
        emailSpan.innerText = "E-posta 5 karakterden az olamaz";
        isCorrectEmail = false;
    }
    else if (!emailPattern.test(emailValue)) {
        emailSpan.innerText = "Geçerli bir e-posta adresi girin";
        isCorrectEmail = false;
    }
    else {
        emailSpan.innerText = "";
        isCorrectEmail = true;
    }

    if (passwordValue.length === 0) {
        document.getElementById("passwordSpan").innerText = "Şifre Boş Bırakılamaz";
        isCorrectPassword = false;
    }
    else if (passwordValue.length < 2) {
        document.getElementById("passwordSpan").innerText = "Şifre 2 Karakterden Az Olamaz";
        isCorrectPassword = false;
    }
    else if (passwordValue != passwordAgainValue) {
        document.getElementById("passwordSpan").innerText = "Şifreler Aynı Olmalı";
        document.getElementById("passwordAgainSpan").innerText = "Şifreler Aynı Olmalı";
        isCorrectPassword = false;
    }
    else {
        document.getElementById("passwordSpan").innerText = "";
        isCorrectPassword = true;
    }

    if (isCorrectUserName && isCorrectName && isCorrectSurName && isCorrectEmail && isCorrectPassword) {
        Swal.fire({
            icon: 'info',
            text: "Kayıt Yapılıyor...",
            background: '#111111',
            showConfirmButton: false,
            timerProgressBar: true,
            willOpen: () => {
                Swal.showLoading(); // Yükleme animasyonunu göster
            }

        });
        datas = {
            username: usernameValue,
            name: nameValue,
            surName: surNameValue,
            email: emailValue,
            password: passwordValue
        };
        $.ajax({
            url: '/Home/Register',
            method: 'POST',
            data: JSON.stringify(datas),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log(data);
                if (data.success) {
                    Swal.fire({
                        icon: 'success',
                        text: data.message,
                        background: '#111111',
                        timer: 2000,
                        showConfirmButton: false,
                        timerProgressBar: true,
                    }).then(() => {
                        window.location.assign('/Home/Login');
                    });
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



   




}