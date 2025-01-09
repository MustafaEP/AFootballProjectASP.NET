$(document).ready(function () {
    loadImage(); // Sayfa yüklenir yüklenmez çalıştır
    
});

function loadImage() {
    const playerName = $('#footballerImg').data('name'); // Oyuncunun adını buradan değiştirebilirsin.
    $.ajax({
        url: '/Manager/Image/GetPlayerImage',
        type: 'GET',
        data: { playerName }, // Sunucuya göndermek istediğimiz parametre
        success: function (response) {
            if (response && response.imageUrl) {
                $('#footballerImg').attr('src', response.imageUrl); // Resim URL'sini img alanına atıyoruz
            } else {
                alert('Resim Bulunamadı.');
            }
        },
        error: function (error) {
            console.error('Hata Mesajı:', error);
            alert('Resim Yüklenemedi.');
        }
    });
    
}
