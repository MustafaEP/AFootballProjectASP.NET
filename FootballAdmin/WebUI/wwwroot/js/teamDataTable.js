

var _selectedTeamId = 1;

$(document).ready(function () {
    // DataTable'ı başlat
    var table = $('.dataTables-example').DataTable({
        pageLength: 25,
        responsive: true,
        paging: false,
        ordering: true,
        dom: '<"html5buttons"B>lTfgitp',
        columns: [
            { data: 'teamName' },
            { data: 'managerName' },
        ],
        buttons: [
            { extend: 'copy', text: 'Kopyala' },
            { extend: 'csv', text: 'CSV' },
            { extend: 'excel', text: 'Excel', title: 'Takımlar' },
            { extend: 'pdf', text: 'PDF', title: 'Takımlar' },
            {
                extend: 'print',
                className: 'buttons-html5',
                text: 'Yazdır',
                customize: function (win) {
                    $(win.document.body)
                        .css('font-size', '10px')
                        .css('margin', '2rem')
                        .css('background-color', '#000')
                        .css('color', '#f0f0f0');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit')
                        .css('margin', '2rem')
                        .css('color', '#000000')
                        .css('border-color', '#444');
                }
            },
            {
                text: 'Yeni Takım',
                className: 'ml-3 btn btn-success',
                action: function (e, dt, node, config) {
                    NewTeamFunc();
                }
            }
        ],
        language: {
            sProcessing: "İşleniyor...",
            sLengthMenu: "Göster _MENU_ Takım",
            sZeroRecords: "Takımlar Aranıyor...",
            sInfo: "Gösterilen: _START_ - _END_, Toplam Takım: _TOTAL_",
            sInfoEmpty: "Gösterilen: 0 - 0, Toplam Takım: 0",
            sInfoFiltered: "( _MAX_ kayıt içinde bulundu)",
            sSearch: "Ara:",
            oPaginate: {
                sFirst: "İlk",
                sPrevious: "<-",
                sNext: "->",
                sLast: "Son"
            }
        },
        responsive: true
    });

    //Verileri Tabloya Ekle
    GetTables();

    $('.dataTables-example tbody').on('click', 'tr', function () {
        var data = table.row(this).data(); // Tıklanan satırdaki veriyi al
        $.ajax({
            url: "/Admin/Team/GetTeamPlayers/" + data.id,
            method: "POST",
            success: function (players) {
                var playerTable = $('.dataTables-players').DataTable({
                    dom: '<"row"<"col-md-9 text-left"B><"col-md-3"f>>it<"row float-right"<"col-md-12"p>>',
                    columns: [
                        { data: 'name' },
                        { data: 'surName' },
                        { data: 'county' },
                        { data: 'phone' },
                        { data: 'playerPosition' },
                        { data: 'strongFoot' },
                    ],
                    buttons: [
                        { extend: 'csv', text: 'CSV' },
                        { extend: 'excel', text: 'Excel', title: data.teamName + '_Oyuncular' },
                        { extend: 'pdf', text: 'PDF', title: 'Oyuncular' },
                        {
                            text: 'Ekle', // Buton metni
                            className: 'btn btn-default buttons-pdf buttons-html5',
                            action: function (e, dt, node, config) {
                                AddAPlayerTeam(data.id);
                            }
                        }
                    ],
                    language: {
                        sProcessing: "İşleniyor...",
                        sLengthMenu: "Göster _MENU_ Oyuncular",
                        sZeroRecords: "Oyuncular Aranıyor...",
                        sInfo: "Menajer: " + data.managerName,
                        sInfoEmpty: "",
                        sInfoFiltered: "( _MAX_ kayıt içinde bulundu)",
                        sSearch: "Ara:",
                        oPaginate: {
                            sFirst: "<<",
                            sPrevious: "<",
                            sNext: ">",
                            sLast: ">>"
                        }
                    },
                    pageLength: 5,
                    paging: true,
                    responsive: true
                });
                playerTable.clear(); // DataTable'daki verileri temizle
                playerTable.rows.add(players); // Yeni verileri ekle
                playerTable.draw(); // DataTable'ı güncelle
            }
        });

        Swal.fire({
            title: `${data.teamName}`,
            html: `<div class="row">
        <div class="col-md-12">
            <div class="table-players m-2">
                <table class="table table-striped table-bordered table-hover dataTables-players">
                                <thead>
                                    <tr>
                                        <th>Adı</th>
                                        <th>Soyadı</th>
                                        <th>Ülkesi</th>
                                        <th>Telefon</th>
                                        <th>Mevki</th>
                                        <th>Güçlü Ayak</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
            </div>
        </div>
    </div>`,
            background: "#111111",
            width: 1000,
            showConfirmButton: false,
            showCancelButton: false,
            showDenyButton: true,
            denyButtonText: 'Çık',
            returnInputValueOnDeny: false,
            customClass: {
                denyButton: 'pop-up-button btn-success',
            }
        }).then((result) => {
           if (result.isDenied) {
                Swal.fire({
                    background: "#111111",
                    icon: "success",
                    title: "Çıkış Yapıldı",
                    showConfirmButton: false,
                    timer: 1000
                });
            }
        });
    });


    //Takım ve Takım İsimleri Çekiliyor
    function GetTables() {
        $.ajax({
            url: '/Admin/Team/GetTeams',
            method: 'GET',
            success: function (data) {
                const filteredData = data.map(item => {
                    return {
                        teamName: item.TeamName,
                        managerName: item.ManagerNameSurName
                    };
                });

                table.clear(); // DataTable'daki verileri temizle
                table.rows.add(data); // Yeni verileri ekle
                table.draw(); // DataTable'ı güncelle

            },
            error: function () {
                alert('Veriler alınırken hata oluştu');
            }
        });
    }


    function AddAPlayerTeam(teamId) {

        $.ajax({
            url: "/Admin/Team/GetAnotherPlayers/" + teamId,
            method: "POST",
            success: function (players) {
                var playerTeamTable = $('.dataTables-Playerteam').DataTable({
                    dom: '<"row"<"col-md-12"f>>it<"row float-right"<"col-md-12"p>>',
                    columns: [
                        { data: 'name' },
                        { data: 'surName' },
                        { data: 'teamName' },
                        { data: 'email' },
                        { data: 'county' },
                        

                        {
                            data: 'id',
                            render: function (data, type, row) {
                                return '<button class="btn btn-dark" onclick="AddPlayerTeamButton(' + row.id + "," + teamId + ')">Ekle</button>';
                            }
                        }


                    ],
                    language: {
                        sProcessing: "İşleniyor...",
                        sLengthMenu: "Göster _MENU_ Oyuncular",
                        sZeroRecords: "Oyuncular Aranıyor...",
                        sInfo: "Oyuncular",
                        sInfoEmpty: "",
                        sInfoFiltered: "( _MAX_ kayıt içinde bulundu)",
                        sSearch: "Ara:",
                        oPaginate: {
                            sFirst: "<<",
                            sPrevious: "<",
                            sNext: ">",
                            sLast: ">>"
                        }
                    },
                    pageLength: 10,
                    paging: true,
                    responsive: true
                });
                playerTeamTable.clear(); // DataTable'daki verileri temizle
                playerTeamTable.rows.add(players); // Yeni verileri ekle
                playerTeamTable.draw(); // DataTable'ı güncelle
            }
        });

        // Oyucuların Takıma Eklendiği Butonun Fonksiyonu
        // Fonksiyonu global hale getirin
        window.AddPlayerTeamButton = function (playerid, teamId) {
            $.ajax({
                url: "/Admin/Team/ChangeTeam/",
                method: "POST",
                data: {
                    playerid: playerid,
                    teamId: teamId
                },
                success: function (data) {
                    Swal.fire({
                        icon: 'success',
                        background: '#111111',
                        text: 'Güncelleme Başarılı',
                        timer: 1500
                    });
                },
                error: function () {
                    Swal.fire({
                        icon: 'error',
                        background: '#111111',
                        text: 'Güncelleme Başarısız',
                        timer: 1500
                    });
                }
            });
        };

        //Oyuncuların Takıma eklenebieceği DataTable
        Swal.fire({
            background: "#111111",
            html: `

                            <table class="table table-bordered dataTables-Playerteam">
                                <thead>
                                <tr>
                                    <th>Ad</th>
                                    <th>Soyad</th>
                                    <th>Takımı</th>
                                    <th>Email</th>
                                    <th>Ülke</th>
                                    <th>Ekle</th>

                                </tr>
                                </thead>
                                <tbody>
                                
                                </tbody>
                            </table>

                    `,
            showConfirmButton: false,
            width: 1000
        });
    }

   

    function NewTeamFunc() {
        Swal.fire({
            title: 'Yeni Takım Ekle',
            background: '#111111',
            html: `<div class="card">
                            <div class="card-body">
                                <form class="forms-sample" id="updateForm">
                                    <div class="form-group">
                                        <label for="name" style="color: #fff;">Takım Adı:</label>
                                        <input type="text" id="teamName" class="form-control" placeholder="Takım Adı" required>
                                    </div>
                                </form>
                            </div>
                        </div>`,
            showConfirmButton: true,
            confirmButtonText: "Ekle",
            showCancelButton: true,
            cancelButtonText: 'Çık',
            customClass: {
                confirmButton: 'pop-up-button btn-primary',
                cancelButton: 'pop-up-button btn-danger',
            },
            preConfirm: () => {
                const updatedData = {
                    teamName: document.getElementById('teamName').value,
                };

                if (Object.values(updatedData).some(field => !field)) {
                    Swal.showValidationMessage("Lütfen tüm alanları doldurun.");
                } else {
                    return updatedData;
                }
            }
        }).then((result) => {
            if (result.isConfirmed) {
                var updatedData = result.value;
                $.ajax({
                    url: '/Admin/Team/AddTeam',
                    method: 'POST',
                    data: JSON.stringify(updatedData),
                    contentType: "application/json; charset=utf-8",
                    success: function () {
                        Swal.fire({
                            icon: 'success',
                            background: '#111111',
                            text: 'Takım Eklendi',
                            showConfirmButton: false,
                            timer: 1000
                        });
                        GetTables();
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            background: '#111111',
                            title: 'Hata, Takım Ekelenemedi',
                            showConfirmButton: false,
                            timer: 1500
                        });
                    }
                });
            }
            else if (deleteResult.dismiss === Swal.DismissReason.cancel) {
                Swal.fire({
                    icon: 'success',
                    background: '#111111',
                    title: 'Çıkış Gerçekleştirildi',
                    showConfirmButton: false,
                    timer: 1000
                })
            }
        });
    }
});
