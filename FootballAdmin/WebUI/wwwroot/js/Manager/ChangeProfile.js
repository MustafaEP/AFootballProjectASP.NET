



document.getElementById("profile_details_submit").addEventListener("click", () => {
    var nameSettings = document.getElementById("nameSettingsId").value;
    var surnameSettings = document.getElementById("surnameSettingsId").value;
    var emailSettings = document.getElementById("emailSettingsId").value;
    var phone_number = document.getElementById("phone_number_id").value;
    var countrySettings = document.getElementById("countrySettingsId").value;
    var clubName = document.getElementById("clubNameId").value;

    var avatarImageElement = document.getElementById("avatar_image_id");

    var imageUrl = avatarImageElement.dataset.imageUrl;

    var WillChangeData = {
        "avatar": imageUrl,
        "name": nameSettings,
        "surName": surnameSettings,
        "email": emailSettings,
        "phone": phone_number,
        "county": countrySettings,
        "ManagerClub": {
            "Name": clubName
        }
    };

    $.ajax({
        url: '/Manager/Home/ProfileSettings',
        method: 'POST',
        data: WillChangeData,
        success: function (data) {
            if (data.success) {
                Swal.fire({
                    text: data.message,
                    icon: 'success',
                    timer: 1500,
                    showConfirmButton: false
                });
            }
            else {
                Swal.fire({
                    text: data.message,
                    icon: 'error',
                    timer: 1500,
                    showConfirmButton: false
                });
            }
        },
        error: function () {
            Swal.fire({
                text: "Sistem Hatası",
                icon: 'error',
                timer: 1500,
                showConfirmButton: false
            });
        }
    });
});

document.getElementById("change_div_username_button").addEventListener("click", () => {
    const element = document.getElementById('change_username_edit');
    element.classList.toggle('d-none');

    const element2 = document.getElementById('username_label');
    element2.classList.add('d-none');

    const element3 = document.getElementById('change_div_username_button');
    element3.classList.add('d-none');

});
function CloseChangeUsernameForm() {
    const element = document.getElementById('change_username_edit');
    element.classList.add('d-none');

    const element2 = document.getElementById('username_label');
    element2.classList.toggle('d-none');

    const element3 = document.getElementById('change_div_username_button');
    element3.classList.toggle('d-none');
}
document.getElementById("kt_signin_cancel").addEventListener("click", () => {
    CloseChangeUsernameForm();
});
$(document).ready(function () {
    $("#update_username_button").click(function () {
        const newUsername = $("#new_username_id").val().trim();
        const password = $("#confirm_password_id").val().trim();

        $("#error_message_for_username").text("");
        $("#error_message_for_password").text("");

        // Doğrulama
        let isValid = true;

        if (!newUsername) {
            $("#error_message_for_username").text("Kullanıcı adı boş bırakılamaz.");
            isValid = false;
        } else if (newUsername.length <= 5) {
            $("error_message_for_username").text("Kullanıcı adı en az 5 karakter olmalıdır.");
            isValid = false;
        }


        if (!password) {
            $("#error_message_for_password").text("Şifre boş bırakılamaz.");
            isValid = false;
        }

        if (!isValid) return;

        // CSRF Token'ı al
        const csrfToken = $('input[name="__RequestVerificationToken"]').val();

        // AJAX isteği
        $.ajax({
            url: '/Manager/Home/ChangeUsername', // Endpoint
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'RequestVerificationToken': csrfToken // CSRF Token gönderimi
            },
            data: JSON.stringify({
                username: newUsername,
                password: password
            }),
            success: function (data) {
                if (data.success) {
                    Swal.fire({
                        text: data.message,
                        icon: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                    CloseChangeUsernameForm();
                }
                else {
                    Swal.fire({
                        text: data.message,
                        icon: 'error',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }
            },
            error: function (xhr, status, error) {
                Swal.fire({
                    text: "Bir Hata Oluştu! Lütfen Tekrar Deneyin.",
                    icon: 'error',
                    timer: 1500,
                    showConfirmButton: false
                });
            }
        });
    });
});




document.getElementById('kt_signin_password_button').addEventListener("click", () => {
    const element = document.getElementById('kt_signin_password');
    element.classList.add('d-none');

    const element2 = document.getElementById('kt_signin_password_edit');
    element2.classList.toggle('d-none');

    const element3 = document.getElementById('kt_signin_password_button');
    element3.classList.add('d-none');

});
function ClosePasswordForm() {
    const element = document.getElementById('kt_signin_password');
    element.classList.toggle('d-none');

    const element2 = document.getElementById('kt_signin_password_edit');
    element2.classList.add('d-none');

    const element3 = document.getElementById('kt_signin_password_button');
    element3.classList.toggle('d-none');
}
document.getElementById('kt_password_cancel').addEventListener("click", () => {
    ClosePasswordForm();
});
