
$(document).ready(function () {
    var table = $('.dataTables-example').DataTable({
        pageLength: 25,
        responsive: true,
        paging: false,
        ordering: true,
        dom: '<"row"<"col-md-9 html5buttons"B><"col-md-3"f>>lTgitp',
        columns: [
            { data: 'name' },
            { data: 'surName' },
            { data: 'email' },
            { data: 'phone' },
            { data: 'county' },
            { data: 'team.teamName' },
            { data: 'playerPosition' },
            { data: 'playerSecondPositions' },
            { data: 'strongFoot' },
            { data: 'createdTime' },
            { data: 'uptatedTime' }
        ],
        buttons: [
            { extend: 'copy', text: 'Kopyala', },
            { extend: 'csv', text: 'CSV' },
            { extend: 'excel', text: 'Excel', title: 'Oyuncular' },
            { extend: 'pdf', text: 'PDF', title: 'Oyuncular' },
            {
                extend: 'print',
                text: 'Yazdır',
                customize: function (win) {
                    $(win.document.body)
                        .css('font-size', '10px')
                        .css('margin', '2rem')
                        .css('background-color', '#000')
                        .css('color', '#f0f0f0');

                    $(win.document.body).find('table')
                        .addClass('compact buttons-html5')
                        .css('font-size', 'inherit')
                        .css('margin', '2rem')
                        .css('color', '#000000')
                        .css('border-color', '#444');
                }
            },
            {
                text: 'Yeni Oyuncu',
                className: 'ml-3 btn btn-success',
                action: function (e, dt, node, config) {
                    newPlayerFunc();
                }
            }
        ],
        language: {
            sProcessing: "İşleniyor...",
            sLengthMenu: "Göster _MENU_ Oyuncu",
            sZeroRecords: "Eşleşen oyuncu bulunamadı",
            sInfo: "Gösterilen: _START_ - _END_, Toplam Futbolcu: _TOTAL_",
            sInfoEmpty: "Gösterilen: 0 - 0, Toplam Futbolcu: 0",
            sInfoFiltered: "( _MAX_ kayıt içinde bulundu)",
            sInfoPostFix: "",
            sSearch: "Ara:",
            sUrl: "",
            oPaginate: {
                sFirst: "İlk",
                sPrevious: "<-",
                sNext: "->",
                sLast: "Son"
            }
        }
    });

    GetTables();

    $('.dataTables-example tbody').on('click', 'tr', function () {
        var dataRow = table.row(this).data();

        Swal.fire({//Listeleme PopUp
            title: `${dataRow.name} ${dataRow.surname}`,
            html: `
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <h5>Oyuncu Bilgileri</h5>
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>İsim:</strong></div>
                                                                        <div class="col-6">${dataRow.name}</div>
                                                                    </div>
                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>Soyisim:</strong></div>
                                                                        <div class="col-6">${dataRow.surName}</div>
                                                                    </div>
                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>Email:</strong></div>
                                                                        <div class="col-6">${dataRow.email}</div>
                                                                    </div>
                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>Telefon:</strong></div>
                                                                        <div class="col-6">${dataRow.phone}</div>
                                                                    </div>
                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                    <div class="col-6"><strong>Ülke:</strong></div>
                                                                    <div class="col-6">
                                                                        ${dataRow.conty === 'Turkey' ? 'Türkiye' :
                dataRow.county === 'United States' ? 'Amerika Birleşik Devletleri' :
                    dataRow.county === 'United Kingdom' ? 'Birleşik Krallık' :
                        dataRow.county === 'Germany' ? 'Almanya' :
                            dataRow.county === 'France' ? 'Fransa' :
                                dataRow.county === 'Canada' ? 'Kanada' :
                                    dataRow.county === 'Australia' ? 'Avustralya' :
                                        dataRow.county === 'Italy' ? 'İtalya' :
                                            dataRow.county === 'Spain' ? 'İspanya' :
                                                dataRow.county === 'Netherlands' ? 'Hollanda' :
                                                    dataRow.county === 'Brazil' ? 'Brezilya' :
                                                        dataRow.county === 'Japan' ? 'Japonya' :
                                                            dataRow.county === 'South Korea' ? 'Güney Kore' :
                                                                dataRow.county === 'India' ? 'Hindistan' :
                                                                    dataRow.county === 'China' ? 'Çin' :
                                                                        dataRow.county === 'Russia' ? 'Rusya' :
                                                                            dataRow.county === 'Mexico' ? 'Meksika' :
                                                                                dataRow.county === 'Sweden' ? 'İsveç' :
                                                                                    dataRow.county === 'Switzerland' ? 'İsviçre' :
                                                                                        dataRow.county === 'Argentina' ? 'Arjantin' : dataRow.county}
                                                                    </div>
                                                                </div>

                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>Takım:</strong></div>
                                                                        <div class="col-6">${dataRow.team.teamName}</div>
                                                                    </div>
                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>Pozisyon:</strong></div>
                                                                        <div class="col-6">
                                                                            ${dataRow.playerPosition === 'Goalkeeper' ? 'Kaleci' :
                dataRow.playerPosition === 'Right Back' ? 'Sağ Bek' :
                    dataRow.playerPosition === 'Center Back' ? 'Stoper' :
                        dataRow.playerPosition === 'Left Back' ? 'Sol Bek' :
                            dataRow.playerPosition === 'Defensive Midfielder' ? 'Defansif Orta Saha' :
                                dataRow.playerPosition === 'Central Midfielder' ? 'Merkez Orta Saha' :
                                    dataRow.playerPosition === 'Attacking Midfielder' ? 'Ofansif Orta Saha' :
                                        dataRow.playerPosition === 'Right Winger' ? 'Sağ Kanat' :
                                            dataRow.playerPosition === 'Left Winger' ? 'Sol Kanat' :
                                                dataRow.playerPosition === 'Striker' ? 'Forvet' :
                                                    dataRow.playerPosition === 'Second Striker' ? 'İkinci Forvet' : dataRow.playerPosition}
                                                                                </div>
                                                                            </div>

                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>2. Pozisyon:</strong></div>
                                                                        <div class="col-6">
                                                                            ${dataRow.playerSecondPositions === 'Goalkeeper' ? 'Kaleci' :
                dataRow.playerSecondPositions === 'Right Back' ? 'Sağ Bek' :
                    dataRow.playerSecondPositions === 'Center Back' ? 'Stoper' :
                        dataRow.playerSecondPositions === 'Left Back' ? 'Sol Bek' :
                            dataRow.playerSecondPositions === 'Defensive Midfielder' ? 'Defansif Orta Saha' :
                                dataRow.playerSecondPositions === 'Central Midfielder' ? 'Merkez Orta Saha' :
                                    dataRow.playerSecondPositions === 'Attacking Midfielder' ? 'Ofansif Orta Saha' :
                                        dataRow.playerSecondPositions === 'Right Winger' ? 'Sağ Kanat' :
                                            dataRow.playerSecondPositions === 'Left Winger' ? 'Sol Kanat' :
                                                dataRow.playerSecondPositions === 'Striker' ? 'Forvet' :
                                                    dataRow.playerSecondPositions === 'Second Striker' ? 'İkinci Forvet' : dataRow.playerSecondPositions}
                                                                            </div>
                                                                        </div>

                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                    <div class="col-6"><strong>Güçlü Ayak:</strong></div>
                                                                    <div class="col-6">
                                                                        <span id="strongFootDisplay">
                                                                            ${dataRow.strongFoot === 'Right' ? 'Sağ Ayak' : dataRow.strongFoot === 'Left' ? 'Sol Ayak' : 'İkisi'}
                                                                        </span>
                                                                    </div>
                                                                </div>

                                                                </div>
                                                            </div>
                                                    `,
            background: "#111111",
            width: 1000,
            confirmButtonText: 'Güncelle',
            showCancelButton: true,
            cancelButtonText: 'Sil',
            cancelButtonColor: "#d33",
            showDenyButton: true,
            denyButtonText: 'Çık',
            returnInputValueOnDeny: false,
            customClass: {
                denyButton: 'pop-up-button btn-success',
                confirmButton: 'pop-up-button btn-primary',
                cancelButton: 'pop-up-button btn-danger',
            }
        }).then((result) => {
            //Silme İşlemi
            if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire({
                    background: "#111111",
                    icon: "error",
                    text: "Silmek İstediğinizden Emin Misiniz?",
                    showCancelButton: true,
                    confirmButtonText: "Sil",
                    cancelButtonText: "Çık",
                    customClass: {
                        confirmButton: 'pop-up-button btn-danger',
                        cancelButton: 'pop-up-button btn-success'
                    }
                }).then((deleteResult) => {
                    //Silmeyi Onaylama
                    if (deleteResult.isConfirmed) {
                        $.ajax({
                            url: '/Admin/Player/DeletePlayer/' + dataRow.id,
                            type: 'POST',
                            success: function (response) {
                                if (response.success) {
                                    if (row) {
                                        row.style.display = 'none';
                                    }
                                    Swal.fire({
                                        background: "#111111",
                                        icon: "success",
                                        title: "Silme İşlemi Başarılı",
                                    });
                                    GetTables();
                                }
                                else {
                                    Swal.fire({
                                        background: "#111111",
                                        icon: "error",
                                        title: "Silme İşlemi Gerçekleştirilemedi",
                                    });
                                }
                            },
                            error: function () {
                                Swal.fire({
                                    background: "#111111",
                                    icon: "error",
                                    title: "Sistemsel Bir Hata Oluştu",
                                });
                            }
                        });


                    }
                    else if (deleteResult.dismiss === Swal.DismissReason.cancel) {
                        Swal.fire({
                            background: "#111111",
                            icon: "success",
                            title: "İşlem İptal Edildi",
                            showConfirmButton: false,
                            timer: 1000
                        });
                    }
                });
            }
            else if (result.isDenied) {
                Swal.fire({
                    background: "#111111",
                    icon: "success",
                    title: "İşlem İptal Edildi",
                    showConfirmButton: false,
                    timer: 1000
                });
            }
            //Güncelleme Sayfası
            else if (result.isConfirmed) {
                $.ajax({
                    url: '/Admin/Player/GetPlayer/' + dataRow.id,
                    method: 'GET',
                    success: function (data) {
                        if (data.success) {
                            var dataValue = data.value;
                            Swal.fire({
                                background: "#111111",
                                width: 1000,
                                title: "Güncelleme Sayfası",
                                showConfirmButton: true,
                                confirmButtonText: "Güncelle",
                                html: `
                                                    <div class="card">
                                                        <div class="card-body">
                                                            <form class="forms-sample" id="updateForm">
                                                                <div class="form-group">
                                                                    <label for="name" style="color: #fff;">Ad:</label>
                                                                    <input type="text" id="name" class="form-control" placeholder="Ad" value="${dataValue.name}" maxlength="15" pattern="^[A-Za-z]" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="surname" style="color: #fff;">Soyad:</label>
                                                                    <input type="text" id="surname" class="form-control" placeholder="Soyad" value="${dataValue.surName}" maxlength="15" pattern="^[A-Za-z]" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="email" style="color: #fff;">Email:</label>
                                                                    <input type="email" id="email" class="form-control" placeholder="Email" value="${dataValue.email}" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="phone" style="color: #fff;">Telefon:</label>
                                                                    <input type="text" id="phone" class="form-control" placeholder="Telefon" value="${dataValue.phone}" maxlength="10" pattern="\d*" required>
                                                                </div>
                                                                <div class="form-group">
                                        <label for="country" style="color: #fff;">Ülke:</label>
                                        <select id="country" class="form-control" required>
                                            <option value="" disabled selected>Ülke Seçin</option>
                                            <option value="Turkey" ${dataValue.county === "Turkey" ? "selected" : ""}>Türkiye</option>
                                            <option value="United States" ${dataValue.county === "United States" ? "selected" : ""}>Amerika Birleşik Devletleri</option>
                                            <option value="United Kingdom" ${dataValue.county === "United Kingdom" ? "selected" : ""}>Birleşik Krallık</option>
                                            <option value="Germany" ${dataValue.county === "Germany" ? "selected" : ""}>Almanya</option>
                                            <option value="France" ${dataValue.county === "France" ? "selected" : ""}>Fransa</option>
                                            <option value="Canada" ${dataValue.county === "Canada" ? "selected" : ""}>Kanada</option>
                                            <option value="Australia" ${dataValue.county === "Australia" ? "selected" : ""}>Avustralya</option>
                                            <option value="Italy" ${dataValue.county === "Italy" ? "selected" : ""}>İtalya</option>
                                            <option value="Spain" ${dataValue.county === "Spain" ? "selected" : ""}>İspanya</option>
                                            <option value="Netherlands" ${dataValue.county === "Netherlands" ? "selected" : ""}>Hollanda</option>
                                            <option value="Brazil" ${dataValue.county === "Brazil" ? "selected" : ""}>Brezilya</option>
                                            <option value="Japan" ${dataValue.county === "Japan" ? "selected" : ""}>Japonya</option>
                                            <option value="South Korea" ${dataValue.county === "South Korea" ? "selected" : ""}>Güney Kore</option>
                                            <option value="India" ${dataValue.county === "India" ? "selected" : ""}>Hindistan</option>
                                            <option value="China" ${dataValue.county === "China" ? "selected" : ""}>Çin</option>
                                            <option value="Russia" ${dataValue.county === "Russia" ? "selected" : ""}>Rusya</option>
                                            <option value="Mexico" ${dataValue.county === "Mexico" ? "selected" : ""}>Meksika</option>
                                            <option value="Sweden" ${dataValue.county === "Sweden" ? "selected" : ""}>İsveç</option>
                                            <option value="Switzerland" ${dataValue.county === "Switzerland" ? "selected" : ""}>İsviçre</option>
                                            <option value="Argentina" ${dataValue.county === "Argentina" ? "selected" : ""}>Arjantin</option>
                                        </select>
                                        </div>
                                                                <div class="form-group">
                                                                    <label for="position" style="color: #fff;">Pozisyon:</label>
                                                                    <select id="position" class="form-control" required>
                                                                        <option value="" disabled ${!dataValue.playerPosition ? 'selected' : ''}>Pozisyon Seçin</option>
                                                                        <option value="Goalkeeper" ${dataValue.playerPosition === "Goalkeeper" ? "selected" : ""}>Kaleci</option>
                                                                        <option value="Right Back" ${dataValue.playerPosition === "Right Back" ? "selected" : ""}>Sağ Bek</option>
                                                                        <option value="Center Back" ${dataValue.playerPosition === "Center Back" ? "selected" : ""}>Stoper</option>
                                                                        <option value="Left Back" ${dataValue.playerPosition === "Left Back" ? "selected" : ""}>Sol Bek</option>
                                                                        <option value="Defensive Midfielder" ${dataValue.playerPosition === "Defensive Midfielder" ? "selected" : ""}>Defansif Orta Saha</option>
                                                                        <option value="Central Midfielder" ${dataValue.playerPosition === "Central Midfielder" ? "selected" : ""}>Merkez Orta Saha</option>
                                                                        <option value="Attacking Midfielder" ${dataValue.playerPosition === "Attacking Midfielder" ? "selected" : ""}>Ofansif Orta Saha</option>
                                                                        <option value="Right Winger" ${dataValue.playerPosition === "Right Winger" ? "selected" : ""}>Sağ Kanat</option>
                                                                        <option value="Left Winger" ${dataValue.playerPosition === "Left Winger" ? "selected" : ""}>Sol Kanat</option>
                                                                        <option value="Striker" ${dataValue.playerPosition === "Striker" ? "selected" : ""}>Forvet</option>
                                                                        <option value="Second Striker" ${dataValue.playerPosition === "Second Striker" ? "selected" : ""}>İkinci Forvet</option>
                                                                    </select>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="secondPosition" style="color: #fff;"> İkinci Pozisyon:</label>
                                                                    <select id="secondPosition" class="form-control" required>
                                                                        <option value="" disabled ${!dataValue.playerSecondPositions ? 'selected' : ''}>Pozisyon Seçin</option>
                                                                        <option value="Goalkeeper" ${dataValue.playerSecondPositions === "Goalkeeper" ? "selected" : ""}>Kaleci</option>
                                                                        <option value="Right Back" ${dataValue.playerSecondPositions === "Right Back" ? "selected" : ""}>Sağ Bek</option>
                                                                        <option value="Center Back" ${dataValue.playerSecondPositions === "Center Back" ? "selected" : ""}>Stoper</option>
                                                                        <option value="Left Back" ${dataValue.playerSecondPositions === "Left Back" ? "selected" : ""}>Sol Bek</option>
                                                                        <option value="Defensive Midfielder" ${dataValue.playerSecondPositions === "Defensive Midfielder" ? "selected" : ""}>Defansif Orta Saha</option>
                                                                        <option value="Central Midfielder" ${dataValue.playerSecondPositions === "Central Midfielder" ? "selected" : ""}>Merkez Orta Saha</option>
                                                                        <option value="Attacking Midfielder" ${dataValue.playerSecondPositions === "Attacking Midfielder" ? "selected" : ""}>Ofansif Orta Saha</option>
                                                                        <option value="Right Winger" ${dataValue.playerSecondPositions === "Right Winger" ? "selected" : ""}>Sağ Kanat</option>
                                                                        <option value="Left Winger" ${dataValue.playerSecondPositions === "Left Winger" ? "selected" : ""}>Sol Kanat</option>
                                                                        <option value="Striker" ${dataValue.playerSecondPositions === "Striker" ? "selected" : ""}>Forvet</option>
                                                                        <option value="Second Striker" ${dataValue.playerSecondPositions === "Second Striker" ? "selected" : ""}>İkinci Forvet</option>
                                                                    </select>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label for="strongFoot" style="color: #fff;">Güçlü Ayak:</label>
                                                                    <select id="strongFoot" class="form-control" required>
                                                                        <option value="" disabled ${!dataValue.strongFoot ? 'selected' : ''}>Güçlü Ayak Seçin</option>
                                                                        <option value="Right" ${dataValue.strongFoot === "Right" ? "selected" : ""}>Sağ Ayak</option>
                                                                        <option value="Left" ${dataValue.strongFoot === "Left" ? "selected" : ""}>Sol Ayak</option>
                                                                        <option value="Both" ${dataValue.strongFoot === "Both" ? "selected" : ""}>İkisi</option>
                                                                    </select>
                                                                </div>



                                                            </form>
                                                        </div>
                                                    </div>

                                            `,
                                preConfirm: () => {
                                    const updatedData = {
                                        id: dataValue.id,
                                        name: document.getElementById('name').value,
                                        surname: document.getElementById('surname').value,
                                        email: document.getElementById('email').value,
                                        phone: document.getElementById('phone').value,
                                        country: document.getElementById('country').value,
                                        team: dataValue.teamId,
                                        position: document.getElementById('position').value,
                                        secondPosition: document.getElementById('secondPosition').value,
                                        strongFoot: document.getElementById('strongFoot').value
                                    };

                                    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                                    const phonePattern = /^\d{10,15}$/; // Telefon numarası için örnek uzunluk

                                    if (Object.values(updatedData).some(field => !field)) {
                                        Swal.showValidationMessage("Lütfen tüm alanları doldurun.");
                                        return false;
                                    }
                                    if (updatedData.name.length < 2) {
                                        Swal.showValidationMessage("İsim en az 2 karakter olmalıdır.");
                                        return false;
                                    }
                                    if (updatedData.surname.length < 2) {
                                        Swal.showValidationMessage("Soyisim en az 2 karakter olmalıdır.");
                                        return false;
                                    }
                                    if (updatedData.name.length > 15) {
                                        Swal.showValidationMessage("İsim en fazla 15 karakter olmalıdır.");
                                        return false;
                                    }
                                    if (updatedData.surname.length > 15) {
                                        Swal.showValidationMessage("Soyisim en fazla 15 karakter olmalıdır.");
                                        return false;
                                    }
                                    if (!updatedData.phone.startsWith('5') || updatedData.phone.length !== 10) {
                                        Swal.showValidationMessage("Telefon Numarası '5' ile başlamak zorundadır ve 10 haneli olmalıdır.");
                                        return false;
                                    }
                                    if (!emailPattern.test(updatedData.email)) {
                                        Swal.showValidationMessage("Geçerli bir email adresi girin.");
                                        return false;
                                    }
                                    if (!phonePattern.test(updatedData.phone)) {
                                        Swal.showValidationMessage("Geçerli bir telefon numarası girin.");
                                        return false;
                                    }


                                    return updatedData;
                                }
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    const updatedData = result.value;
                                    $.ajax({
                                        url: '/Admin/Player/UpdatePlayer/',
                                        data: JSON.stringify(updatedData),
                                        contentType: "application/json; charset=utf-8",
                                        type: 'POST',
                                        success: function (response) {
                                            if (response.success) {
                                                Swal.fire({
                                                    background: "#111111",
                                                    icon: "success",
                                                    title: "Güncelleme İşlemi Başarılı",
                                                });
                                                GetTables();
                                            }
                                            else {
                                                Swal.fire({
                                                    background: "#111111",
                                                    icon: "error",
                                                    title: "Güncelleme İşlemi Gerçekleştirilemedi",
                                                });
                                            }
                                        },
                                        error: function () {
                                            Swal.fire({
                                                background: "#111111",
                                                icon: "error",
                                                title: "Sistemsel Bir Hata Oluştu",
                                            });
                                        }
                                    });

                                }
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
                            text: 'Sistem Hatası!',
                            icon: 'error',
                            timer: 1500,
                            showConfirmButton: false
                        });
                    }
                });
                
            }
        });

    });
    function GetTables() {
        $.ajax({
            url: '/Admin/Player/GetPlayers',
            method: 'GET',
            success: function (data) {
                data.forEach(object => {
                    object.createdTime = formatDateWithHours(object.createdTime);
                    object.uptatedTime = formatDateWithHours(object.uptatedTime);
                });

                const translatedData = translateData(data); // Verileri çevir
                table.clear(); // DataTable'daki verileri temizle
                table.rows.add(translatedData); // Yeni verileri ekle
                table.draw(); // DataTable'ı güncelle
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    background: '#111111',
                    text: 'Veriler Getirilemedi!',
                    showConfirmButton: false
                });
            }
        });
    }



});




//Ekleme Fonksiyonu
function newPlayerFunc() {
    Swal.fire({
        background: "#111111",
        width: 1000,
        title: "Ekleme Sayfası",
        showConfirmButton: true,
        confirmButtonText: "Ekle",
        html: `
                            <div class="card">
                                    <div class="card-body">
                                        <form class="forms-sample" id="updateForm">
                                                <div class="form-group">
                                                        <label for="userName" style="color: #fff;">Kullanıcı Adı:</label>
                                                                <input type="text" id="userName" class="form-control" placeholder="Kullanıcı Adı" maxlength="15" pattern="^[A-Za-z]+$" required>
                                                    </div>
                                            <div class="form-group">
                                                <label for="name" style="color: #fff;">Ad:</label>
                                                <input type="text" id="name" class="form-control" placeholder="Ad" maxlength="15" pattern="^[A-Za-z]" required>
                                            </div>
                                            <div class="form-group">
                                                <label for="surname" style="color: #fff;">Soyad:</label>
                                                <input type="text" id="surname" class="form-control" placeholder="Soyad" maxlength="15" pattern="^[A-Za-z]" required>
                                            </div>
                                            <div class="form-group">
                                                <label for="email" style="color: #fff;">Email:</label>
                                                <input type="email" id="email" class="form-control" placeholder="Email" required>
                                            </div>
                                            <div class="form-group">
                                                <label for="phone" style="color: #fff;">Telefon:</label>
                                                <input type="text" id="phone" class="form-control" placeholder="Telefon" maxlength="10" pattern="\d*" required>
                                            </div>
                                            <div class="form-group">
                                                <label for="country" style="color: #fff;">Ülke:</label>
                                                <select id="country" class="form-control" required>
                                                    <option value="" disabled selected>Ülke Seçin</option>
                                                    <option value="Turkey">Türkiye</option>
                                                    <option value="United States">Amerika Birleşik Devletleri</option>
                                                    <option value="United Kingdom">Birleşik Krallık</option>
                                                    <option value="Germany">Almanya</option>
                                                    <option value="France">Fransa</option>
                                                    <option value="Canada">Kanada</option>
                                                    <option value="Australia">Avustralya</option>
                                                    <option value="Italy">İtalya</option>
                                                    <option value="Spain">İspanya</option>
                                                    <option value="Netherlands">Hollanda</option>
                                                    <option value="Brazil">Brezilya</option>
                                                    <option value="Japan">Japonya</option>
                                                    <option value="South Korea">Güney Kore</option>
                                                    <option value="India">Hindistan</option>
                                                    <option value="China">Çin</option>
                                                    <option value="Russia">Rusya</option>
                                                    <option value="Mexico">Meksika</option>
                                                    <option value="Sweden">İsveç</option>
                                                    <option value="Switzerland">İsviçre</option>
                                                    <option value="Argentina">Arjantin</option>
                                                </select>
                                            </div>
                                            <div class="form-group">
                                                <label for="position" style="color: #fff;">Pozisyon:</label>
                                                <select id="position" class="form-control" required>
                                                    <option value="" disabled selected>Pozisyon Seçin</option>
                                                    <option value="Goalkeeper">Kaleci</option>
                                                    <option value="Right Back">Sağ Bek</option>
                                                    <option value="Center Back">Stoper</option>
                                                    <option value="Left Back">Sol Bek</option>
                                                    <option value="Defensive Midfielder">Defansif Orta Saha</option>
                                                    <option value="Central Midfielder">Merkez Orta Saha</option>
                                                    <option value="Attacking Midfielder">Ofansif Orta Saha</option>
                                                    <option value="Right Winger">Sağ Kanat</option>
                                                    <option value="Left Winger">Sol Kanat</option>
                                                    <option value="Striker">Forvet</option>
                                                    <option value="Second Striker">İkinci Forvet</option>
                                                </select>
                                            </div>
                                            <div class="form-group">
                                                <label for="secondPosition" style="color: #fff;">İkinci Pozisyon:</label>
                                                <select id="secondPosition" class="form-control" required>
                                                    <option value="" disabled selected>Pozisyon Seçin</option>
                                                    <option value="Goalkeeper">Kaleci</option>
                                                    <option value="Right Back">Sağ Bek</option>
                                                    <option value="Center Back">Stoper</option>
                                                    <option value="Left Back">Sol Bek</option>
                                                    <option value="Defensive Midfielder">Defansif Orta Saha</option>
                                                    <option value="Central Midfielder">Merkez Orta Saha</option>
                                                    <option value="Attacking Midfielder">Ofansif Orta Saha</option>
                                                    <option value="Right Winger">Sağ Kanat</option>
                                                    <option value="Left Winger">Sol Kanat</option>
                                                    <option value="Striker">Forvet</option>
                                                    <option value="Second Striker">İkinci Forvet</option>
                                                </select>
                                            </div>

                                           
                                            <div class="form-group">
                                                <label for="strongFoot" style="color: #fff;">Güçlü Ayak:</label>
                                                <select id="strongFoot" class="form-control" required>
                                                    <option value="" disabled selected>Güçlü Ayak Seçin</option>
                                                    <option value="Right">Sağ Ayak</option>
                                                    <option value="Left">Sol Ayak</option>
                                                    <option value="Both">İkisi</option>
                                                </select>
                                            </div>
                                            <div class="form-group">
                                                <label for="temporaryPassword" style="color: #fff;">Geçici Şifre:</label>
                                                <input type="text" id="temporaryPassword" class="form-control" placeholder="Geçici Şifre" required>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                `,
        preConfirm: () => {
            const updatedData = {
                userName: document.getElementById('userName').value,
                name: document.getElementById('name').value,
                surname: document.getElementById('surname').value,
                email: document.getElementById('email').value,
                phone: document.getElementById('phone').value,
                country: document.getElementById('country').value,
                team: 1,
                position: document.getElementById('position').value,
                secondPosition: document.getElementById('secondPosition').value,
                strongFoot: document.getElementById('strongFoot').value,
                temporaryPassword: document.getElementById('temporaryPassword').value
            };

            if (Object.values(updatedData).some(field => !field)) {
                Swal.showValidationMessage("Lütfen tüm alanları doldurun.");
                return false;
            }
            if (updatedData.userName.length < 2) {
                Swal.showValidationMessage("Kullanıcı Adı en az 5 karakter olmalıdır.");
                return false;
            }
            if (updatedData.name.length < 2) {
                Swal.showValidationMessage("İsim en az 2 karakter olmalıdır.");
                return false;
            }
            if (updatedData.surname.length < 2) {
                Swal.showValidationMessage("Soyisim en az 2 karakter olmalıdır.");
                return false;
            }
            if (updatedData.name.length > 15) {
                Swal.showValidationMessage("İsim en fazla 15 karakter olmalıdır.");
                return false;
            }
            if (updatedData.surname.length > 15) {
                Swal.showValidationMessage("Soyisim en falza 15 karakter olmalıdır.");
                return false;
            }
            if (Object.values(updatedData).some(field => !field)) {
                Swal.showValidationMessage("Lütfen tüm alanları doldurun.");
                return false;
            }

            if (!updatedData.phone.startsWith('5') || updatedData.phone.length !== 10) {
                Swal.showValidationMessage("Telefon Numarası '5' ile başlamak zorundadır ve 10 haneli olmalıdır.");
                return false;
            }

            if (!emailPattern.test(updatedData.email)) {
                Swal.showValidationMessage("Geçerli bir email adresi girin.");
                return false;
            }
            if (!phonePattern.test(updatedData.phone)) {
                Swal.showValidationMessage("Geçerli bir telefon numarası girin.");
                return false;
            }


            return updatedData;
        }
    }).then((result) => {
        if (result.isConfirmed) {
            const updatedData = result.value;
            $.ajax({
                url: '/Admin/Player/AddPlayer/',
                data: JSON.stringify(updatedData),
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                success: function (response) {
                    if (response.success) {

                        var newRowData = {
                            name: updatedData.name,
                            surname: updatedData.surname,
                            email: updatedData.email,
                            phone: updatedData.phone,
                            country: updatedData.country,
                            teamId: updatedData.team,
                            position: updatedData.position,
                            secondPosition: updatedData.secondPosition,
                            strongFoot: updatedData.strongFoot
                        };


                        Swal.fire({
                            background: "#111111",
                            icon: "success",
                            title: "Ekleme İşlemi Başarılı",
                        });

                        GetTables();



                    }
                    else {
                        Swal.fire({
                            background: "#111111",
                            icon: "error",
                            title: response.message,
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        background: "#111111",
                        icon: "error",
                        title: "Sistemsel Bir Hata Oluştu",
                    });
                }
            });
        }
    });
}




