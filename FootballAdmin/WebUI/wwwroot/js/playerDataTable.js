
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
            { data: 'strongFoot' }
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
                                                    dataRow. playerPosition === 'Second Striker' ? 'İkinci Forvet' : ''}
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
                                                    dataRow. playerSecondPositions === 'Second Striker' ? 'İkinci Forvet' : ''}
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
                                                                    <input type="text" id="name" class="form-control" placeholder="Ad" value="${dataRow.name}" maxlength="15" pattern="^[A-Za-z]" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="surname" style="color: #fff;">Soyad:</label>
                                                                    <input type="text" id="surname" class="form-control" placeholder="Soyad" value="${dataRow.surName}" maxlength="15" pattern="^[A-Za-z]" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="email" style="color: #fff;">Email:</label>
                                                                    <input type="email" id="email" class="form-control" placeholder="Email" value="${dataRow.email}" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="phone" style="color: #fff;">Telefon:</label>
                                                                    <input type="text" id="phone" class="form-control" placeholder="Telefon" value="${dataRow.phone}" maxlength="10" pattern="\d*" required>
                                                                </div>
                                                                <div class="form-group">
                                        <label for="country" style="color: #fff;">Ülke:</label>
                                        <select id="country" class="form-control" required>
                                            <option value="" disabled selected>Ülke Seçin</option>
                                            <option value="Turkey" ${dataRow.county === "Turkey" ? "selected" : ""}>Türkiye</option>
                                            <option value="United States" ${dataRow.county === "United States" ? "selected" : ""}>Amerika Birleşik Devletleri</option>
                                            <option value="United Kingdom" ${dataRow.county === "United Kingdom" ? "selected" : ""}>Birleşik Krallık</option>
                                            <option value="Germany" ${dataRow.county === "Germany" ? "selected" : ""}>Almanya</option>
                                            <option value="France" ${dataRow.county === "France" ? "selected" : ""}>Fransa</option>
                                            <option value="Canada" ${dataRow.county === "Canada" ? "selected" : ""}>Kanada</option>
                                            <option value="Australia" ${dataRow.county === "Australia" ? "selected" : ""}>Avustralya</option>
                                            <option value="Italy" ${dataRow.county === "Italy" ? "selected" : ""}>İtalya</option>
                                            <option value="Spain" ${dataRow.county === "Spain" ? "selected" : ""}>İspanya</option>
                                            <option value="Netherlands" ${dataRow.county === "Netherlands" ? "selected" : ""}>Hollanda</option>
                                            <option value="Brazil" ${dataRow.county === "Brazil" ? "selected" : ""}>Brezilya</option>
                                            <option value="Japan" ${dataRow.county === "Japan" ? "selected" : ""}>Japonya</option>
                                            <option value="South Korea" ${dataRow.county === "South Korea" ? "selected" : ""}>Güney Kore</option>
                                            <option value="India" ${dataRow.county === "India" ? "selected" : ""}>Hindistan</option>
                                            <option value="China" ${dataRow.county === "China" ? "selected" : ""}>Çin</option>
                                            <option value="Russia" ${dataRow.county === "Russia" ? "selected" : ""}>Rusya</option>
                                            <option value="Mexico" ${dataRow.county === "Mexico" ? "selected" : ""}>Meksika</option>
                                            <option value="Sweden" ${dataRow.county === "Sweden" ? "selected" : ""}>İsveç</option>
                                            <option value="Switzerland" ${dataRow.county === "Switzerland" ? "selected" : ""}>İsviçre</option>
                                            <option value="Argentina" ${dataRow.county === "Argentina" ? "selected" : ""}>Arjantin</option>
                                        </select>
                                        </div>
                                                                <div class="form-group">
                                                                    <label for="position" style="color: #fff;">Pozisyon:</label>
                                                                    <select id="position" class="form-control" required>
                                                                        <option value="" disabled ${!dataRow.playerPosition ? 'selected' : ''}>Pozisyon Seçin</option>
                                                                        <option value="Goalkeeper" ${dataRow.playerPosition === "Goalkeeper" ? "selected" : ""}>Kaleci</option>
                                                                        <option value="Right Back" ${dataRow.playerPosition === "Right Back" ? "selected" : ""}>Sağ Bek</option>
                                                                        <option value="Center Back" ${dataRow.playerPosition === "Center Back" ? "selected" : ""}>Stoper</option>
                                                                        <option value="Left Back" ${dataRow.playerPosition === "Left Back" ? "selected" : ""}>Sol Bek</option>
                                                                        <option value="Defensive Midfielder" ${dataRow.playerPosition === "Defensive Midfielder" ? "selected" : ""}>Defansif Orta Saha</option>
                                                                        <option value="Central Midfielder" ${dataRow.playerPosition === "Central Midfielder" ? "selected" : ""}>Merkez Orta Saha</option>
                                                                        <option value="Attacking Midfielder" ${dataRow.playerPosition === "Attacking Midfielder" ? "selected" : ""}>Ofansif Orta Saha</option>
                                                                        <option value="Right Winger" ${dataRow.playerPosition === "Right Winger" ? "selected" : ""}>Sağ Kanat</option>
                                                                        <option value="Left Winger" ${dataRow.playerPosition === "Left Winger" ? "selected" : ""}>Sol Kanat</option>
                                                                        <option value="Striker" ${dataRow.playerPosition === "Striker" ? "selected" : ""}>Forvet</option>
                                                                        <option value="Second Striker" ${dataRow.playerPosition === "Second Striker" ? "selected" : ""}>İkinci Forvet</option>
                                                                    </select>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="secondPosition" style="color: #fff;"> İkinci Pozisyon:</label>
                                                                    <select id="secondPosition" class="form-control" required>
                                                                        <option value="" disabled ${!dataRow.playerSecondPositions ? 'selected' : ''}>Pozisyon Seçin</option>
                                                                        <option value="Goalkeeper" ${dataRow.playerSecondPositions === "Goalkeeper" ? "selected" : ""}>Kaleci</option>
                                                                        <option value="Right Back" ${dataRow.playerSecondPositions === "Right Back" ? "selected" : ""}>Sağ Bek</option>
                                                                        <option value="Center Back" ${dataRow.playerSecondPositions === "Center Back" ? "selected" : ""}>Stoper</option>
                                                                        <option value="Left Back" ${dataRow.playerSecondPositions === "Left Back" ? "selected" : ""}>Sol Bek</option>
                                                                        <option value="Defensive Midfielder" ${dataRow.playerSecondPositions === "Defensive Midfielder" ? "selected" : ""}>Defansif Orta Saha</option>
                                                                        <option value="Central Midfielder" ${dataRow.playerSecondPositions === "Central Midfielder" ? "selected" : ""}>Merkez Orta Saha</option>
                                                                        <option value="Attacking Midfielder" ${dataRow.playerSecondPositions === "Attacking Midfielder" ? "selected" : ""}>Ofansif Orta Saha</option>
                                                                        <option value="Right Winger" ${dataRow.playerSecondPositions === "Right Winger" ? "selected" : ""}>Sağ Kanat</option>
                                                                        <option value="Left Winger" ${dataRow.playerSecondPositions === "Left Winger" ? "selected" : ""}>Sol Kanat</option>
                                                                        <option value="Striker" ${dataRow.playerSecondPositions === "Striker" ? "selected" : ""}>Forvet</option>
                                                                        <option value="Second Striker" ${dataRow.playerSecondPositions === "Second Striker" ? "selected" : ""}>İkinci Forvet</option>
                                                                    </select>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label for="strongFoot" style="color: #fff;">Güçlü Ayak:</label>
                                                                    <select id="strongFoot" class="form-control" required>
                                                                        <option value="" disabled ${!dataRow.strongFoot ? 'selected' : ''}>Güçlü Ayak Seçin</option>
                                                                        <option value="Right" ${dataRow.strongFoot === "Right" ? "selected" : ""}>Sağ Ayak</option>
                                                                        <option value="Left" ${dataRow.strongFoot === "Left" ? "selected" : ""}>Sol Ayak</option>
                                                                        <option value="Both" ${dataRow.strongFoot === "Both" ? "selected" : ""}>İkisi</option>
                                                                    </select>
                                                                </div>



                                                            </form>
                                                        </div>
                                                    </div>

                                            `,
                    preConfirm: () => {
                        const updatedData = {
                            id: dataRow.id,
                            name: document.getElementById('name').value,
                            surname: document.getElementById('surname').value,
                            email: document.getElementById('email').value,
                            phone: document.getElementById('phone').value,
                            country: document.getElementById('country').value,
                            team: dataRow.teamId,
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
                                    // Tabloyu güncelleme
                                    updateTableRow(updatedData);
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
        });

    });
    function GetTables() {
        $.ajax({
            url: '/Admin/Player/GetPlayers',
            method: 'GET',
            success: function (data) {
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
                            title: "Ekleme İşlemi Gerçekleştirilemedi",
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





function updateTableRow(data) {
    // data-id'yi kullanarak doğru tablo satırını seçiyoruz
    const row = document.querySelector(`tr[data-id='${data.id}']`);

    if (row) {
        row.querySelector("td:nth-child(1)").textContent = data.name;
        row.querySelector("td:nth-child(2)").textContent = data.surname;
        row.querySelector("td:nth-child(3)").textContent = data.email;
        row.querySelector("td:nth-child(4)").textContent = data.phone;
        row.querySelector("td:nth-child(5)").textContent = data.country;
        row.querySelector("td:nth-child(6)").textContent = data.team;
        row.querySelector("td:nth-child(7)").textContent = data.position;
        row.querySelector("td:nth-child(8)").textContent = data.secondPosition;
        row.querySelector("td:nth-child(9)").textContent = data.strongFoot;
    }
}



