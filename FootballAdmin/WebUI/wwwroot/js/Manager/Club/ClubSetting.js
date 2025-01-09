$(document).ready(function () {
    $("#avatar_add_id").click(() => {

        $.ajax({
            url: '/Manager/DataJson/GetLogos', // API endpoint
            method: 'GET',
            success: function (response) {
                // Gelen JSON'daki "success" durumunu kontrol et
                if (response.success) {
                    let images = response.value.$values;

                    // Resimleri içeren HTML oluştur
                    let imageGallery = '<div class="image-gallery">';
                    images.forEach((image, index) => {
                        imageGallery += `
                    <img 
                        src="${image}" 
                        class="img-thumbnail selectable-image" 
                        style="width: 50px; margin: 5px; cursor: pointer;" 
                        alt="Avatar ${index + 1}" 
                        data-image="${image}" />
                `;
                    });
                    imageGallery += '</div>';

                    // Bootbox dialog içinde resimleri göster
                    var selectAvatar = bootbox.dialog({
                        title: "Bir Resim Seçin",
                        message: imageGallery,
                        buttons: {
                            cancel: {
                                label: 'İptal',
                                className: 'btn-danger'
                            }
                        }
                    });

                    // Resimlere tıklama özelliği ekle
                    $(document).on('click', '.selectable-image', function () {
                        // 'this' ile tıklanan öğeyi alıyoruz ve 'data-image' özelliğini alıyoruz.
                        let selectedImage = $(this).data('image');

                        // avatar_image_id elementini seçiyoruz
                        var avatarImageElement = document.getElementById("avatar_image_id");

                        // 'data-imageUrl' özelliğini güncelliyoruz
                        avatarImageElement.dataset.imageUrl = selectedImage;

                        // CSS ile arka plan resmini değiştiriyoruz
                        $("#avatar_image_id").css({
                            "background-image": `url('${selectedImage}')`
                        });
                        bootbox.hideAll();
                    });

                } else {
                    alert(response.message || "Avatarlar bulunamadı.");
                }
            },
            error: function () {
                alert("Resim verileri alınırken bir hata oluştu.");
            }
        });


    });

    $("#profile_details_submit").click(() => {

        var avatarImageElement = document.getElementById("avatar_image_id");
        var newClubName = document.getElementById("club_name_id").value;

        var imageUrl = avatarImageElement.dataset.imageUrl;

        var changedDatas = {
            "avatar": imageUrl,
            "name": newClubName
        }

        $.ajax({
            url: '/Manager/Club/Club',
            method: 'POST',
            data: changedDatas,
            success: function (data) {
                if (data.success) {
                    Swal.fire({
                        text: data.message,
                        icon: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                    document.getElementById("logo_id").src = imageUrl;
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
});
