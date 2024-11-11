function Logout() {
    Swal.fire({
        text: 'Çıkış Yapılıyor',
        timer: 1500,
        icon:'info',
        background: '#111111',
        showConfirmButton: false
    }).then(() => {
        $.ajax({
            url: '/Home/Logout',
            method: 'GET',
            success: function (data) {
                if (data.success) {
                    window.location.replace("/Home/Index");
                }
            },
            error: function (xhr, status, error) {
                Swal.fire({
                    title: 'Çıkış yaparken bir hata oluştu',
                    timer: 1000,
                    showConfirmButton: false,
                    background: '#111111'
                })
            }
        });
    });

}