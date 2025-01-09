$(document).ready(function () {
    $("#avatar_add_id").click(() => {

        $.ajax({
            url: '/Manager/DataJson/GetAvatars', // API endpoint
            method: 'GET',
            success: function (response) {
                console.log(response);
                if (response.success) {
                    let images = response.value.$values;

                    // Resimleri içeren HTML oluştur
                    let imageGallery = '<div class="image-gallery">';
                    images.forEach((image, index) => {
                        imageGallery += `
                    <img 
                        src="${image}" 
                        class="img-thumbnail selectable-image" 
                        style="width: 100px; margin: 5px; cursor: pointer;" 
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
});
