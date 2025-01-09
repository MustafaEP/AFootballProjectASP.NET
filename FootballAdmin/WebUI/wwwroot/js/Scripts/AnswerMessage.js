function Message(success , message){
    Swal.fire({
        icon: success == true ? "success" : "error",
        text: message,
        timer: 2000,
        showConfirmButton: false
    });
}

function ErrorMessage() {
    Swal.fire({
        icon: 'error',
        text: "Bir Hata Gerçekleşti. Lütfen Daha Sonra Yeniden Deneyiniz!",
        timer: 2000,
        showConfirmButton: false
    });
}

function ExitMessage() {
    Swal.fire({
        icon: 'success',
        text: "Çıkış İşlemi Gerçekleşti",
        timer: 2000,
        showConfirmButton: false
    });
}

function LoadingMessage() {
    Swal.fire({
        icon: 'info',
        text: "Bu İşlem Zaman Alabilir. Lütfen Sayfadan Çıkmayınız. İsteğinizi Hızlıca Gerçekleştirmeye Çalışıyoruz!",
        showConfirmButton: false,
        allowOutsideClick: false,
        didOpen: () => {
            Swal.showLoading(); // Yüklenme simgesi
        }
    });
}

function AreYouSure(onConfirmCallback) {
    Swal.fire({
        icon: 'warning',
        title: 'Emin misiniz?',
        text: 'Bu işlem geri alınamaz!',
        showCancelButton: true,
        confirmButtonText: 'Eminim',
        cancelButtonText: 'Çık',
        allowOutsideClick: false,
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    }).then((result) => {
        if (result.isConfirmed) {
            if (typeof onConfirmCallback === "function") {
                onConfirmCallback();
            }
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            ExitMessage();
        }
    });
}
