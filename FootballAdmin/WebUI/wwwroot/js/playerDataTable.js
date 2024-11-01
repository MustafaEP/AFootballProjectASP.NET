

var table;
$(document).ready(function () {
    table = $('.dataTables-example').DataTable({
        pageLength: 25,
        responsive: true,
        paging: false,
        ordering: true,
        dom: '<"html5buttons"B>lTfgitp',
        columns: [
            { data: 'name' },
            { data: 'surname' },
            { data: 'email' },
            { data: 'phone' },
            { data: 'country' },
            { data: 'teamId' },
            { data: 'position' },
            { data: 'secondPosition' },
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
        },
        responsive: true
    });
});

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
                                                                <input type="text" id="userName" class="form-control" placeholder="Kullanıcı Adı" required>
                                                    </div>
                                            <div class="form-group">
                                                <label for="name" style="color: #fff;">Ad:</label>
                                                <input type="text" id="name" class="form-control" placeholder="Ad" required>
                                            </div>
                                            <div class="form-group">
                                                <label for="surname" style="color: #fff;">Soyad:</label>
                                                <input type="text" id="surname" class="form-control" placeholder="Soyad" required>
                                            </div>
                                            <div class="form-group">
                                                <label for="email" style="color: #fff;">Email:</label>
                                                <input type="email" id="email" class="form-control" placeholder="Email" required>
                                            </div>
                                            <div class="form-group">
                                                <label for="phone" style="color: #fff;">Telefon:</label>
                                                <input type="text" id="phone" class="form-control" placeholder="Telefon" required>
                                            </div>
                                            <div class="form-group">
                                                <label for="country" style="color: #fff;">Ülke:</label>
                                                <input type="text" id="country" class="form-control" placeholder="Ülke" required>
                                            </div>
                                            <div class="form-group">
                                                <label for="team" style="color: #fff;">Takım ID:</label>
                                                <input type="text" id="team" class="form-control" placeholder="Takım ID" required>
                                            </div>
                                            <div class="form-group">
                                                <label for="position" style="color: #fff;">Pozisyon:</label>
                                                <input type="text" id="position" class="form-control" placeholder="Pozisyon" required>
                                            </div>
                                            <div class="form-group">
                                                <label for="secondPosition" style="color: #fff;">İkinci Pozisyon:</label>
                                                <input type="text" id="secondPosition" class="form-control" placeholder="İkinci Pozisyon" required>
                                            </div>
                                            <div class="form-group">
                                                <label for="strongFoot" style="color: #fff;">Güçlü Ayak:</label>
                                                <input type="text" id="strongFoot" class="form-control" placeholder="Güçlü Ayak" required>
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
                team: document.getElementById('team').value,
                position: document.getElementById('position').value,
                secondPosition: document.getElementById('secondPosition').value,
                strongFoot: document.getElementById('strongFoot').value,
                temporaryPassword: document.getElementById('temporaryPassword').value
            };

            if (Object.values(updatedData).some(field => !field)) {
                Swal.showValidationMessage("Lütfen tüm alanları doldurun.");
            } else {
                return updatedData;
            }
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
                        becareful();
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
                        table.row.add(newRowData).draw(false); // Tablonun yeniden çizimini önleyin


                        swalInfo.fire({
                            background: "#111111",
                            icon: "success",
                            title: "Ekleme İşlemi Başarılı",
                        });

                        // Eklenecek yeni satırın verileri



                    }
                    else {
                        swalInfo.fire({
                            background: "#111111",
                            icon: "error",
                            title: "Ekleme İşlemi Gerçekleştirilemedi",
                        });
                    }
                },
                error: function () {
                    swalInfo.fire({
                        background: "#111111",
                        icon: "error",
                        title: "Sistemsel Bir Hata Oluştu",
                    });
                }
            });
        }
    });
}



document.querySelectorAll('.player-row').forEach(function (row) {
    row.addEventListener('click', function () {


        const data_id = this.dataset.id;

        // data-id'yi kullanarak doğru tablo satırını seçiyoruz
        const rowData = document.querySelector(`tr[data-id='${data_id}']`);
        console.log(rowData);
        var name = "", surname = "", email = "", phone = "", country = "", team = "", position = "", secondPosition = "", strongFoot = "";

        if (rowData) {
            name = rowData.querySelector("td:nth-child(1)").textContent;
            surname = rowData.querySelector("td:nth-child(2)").textContent;
            email = rowData.querySelector("td:nth-child(3)").textContent;
            phone = rowData.querySelector("td:nth-child(4)").textContent;
            country = rowData.querySelector("td:nth-child(5)").textContent;
            team = rowData.querySelector("td:nth-child(6)").textContent;
            position = rowData.querySelector("td:nth-child(7)").textContent;
            secondPosition = rowData.querySelector("td:nth-child(8)").textContent;
            strongFoot = rowData.querySelector("td:nth-child(9)").textContent;
        }


        const row = document.querySelector(`tr[data-id='${data_id}']`);

        const swalInfo = Swal.mixin({

        });

        swalInfo.fire({
            title: `${name} ${surname}`,
            html: `
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <h5>Oyuncu Bilgileri</h5>
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>İsim:</strong></div>
                                                                        <div class="col-6">${name}</div>
                                                                    </div>
                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>Soyisim:</strong></div>
                                                                        <div class="col-6">${surname}</div>
                                                                    </div>
                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>Email:</strong></div>
                                                                        <div class="col-6">${email}</div>
                                                                    </div>
                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>Telefon:</strong></div>
                                                                        <div class="col-6">${phone}</div>
                                                                    </div>
                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>Ülke:</strong></div>
                                                                        <div class="col-6">${country}</div>
                                                                    </div>
                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>Takım:</strong></div>
                                                                        <div class="col-6">${team}</div>
                                                                    </div>
                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>Pozisyon:</strong></div>
                                                                        <div class="col-6">${position}</div>
                                                                    </div>
                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>2. Pozisyon:</strong></div>
                                                                        <div class="col-6">${secondPosition}</div>
                                                                    </div>
                                                                    <hr class="border border-white border-2 opacity-25">
                                                                    <div class="row pb-2 pt-2">
                                                                        <div class="col-6"><strong>Güçlü Ayak:</strong></div>
                                                                        <div class="col-6">${strongFoot}</div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                    `,
            background: "#111111",
            width: 1000,
            icon: 'null',
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
                swalInfo.fire({
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
                            url: '/Admin/Player/DeletePlayer/' + data_id,
                            type: 'POST',
                            success: function (response) {
                                if (response.success) {
                                    if (row) {
                                        row.style.display = 'none';
                                    }
                                    swalInfo.fire({
                                        background: "#111111",
                                        icon: "success",
                                        title: "Silme İşlemi Başarılı",
                                    });
                                    becareful();
                                }
                                else {
                                    swalInfo.fire({
                                        background: "#111111",
                                        icon: "error",
                                        title: "Silme İşlemi Gerçekleştirilemedi",
                                    });
                                }
                            },
                            error: function () {
                                swalInfo.fire({
                                    background: "#111111",
                                    icon: "error",
                                    title: "Sistemsel Bir Hata Oluştu",
                                });
                            }
                        });


                    }
                    else if (deleteResult.dismiss === Swal.DismissReason.cancel) {
                        swalInfo.fire({
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
                swalInfo.fire({
                    background: "#111111",
                    icon: "success",
                    title: "İşlem İptal Edildi",
                    showConfirmButton: false,
                    timer: 1000
                });
            }
            //Güncelleme Sayfası
            else if (result.isConfirmed) {
                swalInfo.fire({
                    background: "#111111",
                    icon: "null",
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
                                                                    <input type="text" id="name" class="form-control" placeholder="Ad" value="${name}" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="surname" style="color: #fff;">Soyad:</label>
                                                                    <input type="text" id="surname" class="form-control" placeholder="Soyad" value="${surname}" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="email" style="color: #fff;">Email:</label>
                                                                    <input type="email" id="email" class="form-control" placeholder="Email" value="${email}" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="phone" style="color: #fff;">Telefon:</label>
                                                                    <input type="text" id="phone" class="form-control" placeholder="Telefon" value="${phone}" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="country" style="color: #fff;">Ülke:</label>
                                                                    <input type="text" id="country" class="form-control" placeholder="Ülke" value="${country}" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="team" style="color: #fff;">Takım ID:</label>
                                                                    <input type="text" id="team" class="form-control" placeholder="Takım ID" value="${team}" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="position" style="color: #fff;">Pozisyon:</label>
                                                                    <input type="text" id="position" class="form-control" placeholder="Pozisyon" value="${position}" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="secondPosition" style="color: #fff;">İkinci Pozisyon:</label>
                                                                    <input type="text" id="secondPosition" class="form-control" placeholder="İkinci Pozisyon" value="${secondPosition}" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="strongFoot" style="color: #fff;">Güçlü Ayak:</label>
                                                                    <input type="text" id="strongFoot" class="form-control" placeholder="Güçlü Ayak" value="${strongFoot}" required>
                                                                </div>

                                                            </form>
                                                        </div>
                                                    </div>

                                            `,
                    preConfirm: () => {
                        const updatedData = {
                            id: data_id,
                            name: document.getElementById('name').value,
                            surname: document.getElementById('surname').value,
                            email: document.getElementById('email').value,
                            phone: document.getElementById('phone').value,
                            country: document.getElementById('country').value,
                            team: document.getElementById('team').value,
                            position: document.getElementById('position').value,
                            secondPosition: document.getElementById('secondPosition').value,
                            strongFoot: document.getElementById('strongFoot').value
                        };

                        if (Object.values(updatedData).some(field => !field)) {
                            Swal.showValidationMessage("Lütfen tüm alanları doldurun.");
                        } else {
                            return updatedData;
                        }
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
                                    swalInfo.fire({
                                        background: "#111111",
                                        icon: "success",
                                        title: "Güncelleme İşlemi Başarılı",
                                    });
                                    becareful();
                                }
                                else {
                                    swalInfo.fire({
                                        background: "#111111",
                                        icon: "error",
                                        title: "Güncelleme İşlemi Gerçekleştirilemedi",
                                    });
                                }
                            },
                            error: function () {
                                swalInfo.fire({
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
});


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

function becareful() {
    document.getElementById('downloadDanger').innerHTML = "<small><a class='btn btn-danger mr-1 mb-2 bradius' href='/Admin/Player/ListPlayers'>Yenile</a><p class='mb-2'> Herhangi bir indirme işlemi yapmadan sayfayı yenileyiniz.</p></small> ";

}

