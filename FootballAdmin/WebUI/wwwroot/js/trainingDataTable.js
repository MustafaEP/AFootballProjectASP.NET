$(document).ready(() => {
    var table = $('.dataTables-example').DataTable({
        pageLength: 25,
        responsive: true,
        paging: false,
        ordering: true,
        dom: '<"row"<"col-md-6 html5buttons"B><"col-md-6"f>>lTgitp',
        columns: [
            { data: 'trainingName' },
            { data: 'teamName' },
            { data: 'managerName' },
            { data: 'date' },
        ],
        buttons: [
            { extend: 'copy', text: 'Kopyala' },
            { extend: 'excel', text: 'Excel', title: 'Antrenmanlar' },
            { extend: 'pdf', text: 'PDF', title: 'Antrenmanlar' },
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
            }
        ],
        language: {
            sProcessing: "İşleniyor...",
            sLengthMenu: "Göster _MENU_ Oyuncu",
            sZeroRecords: "Antrenmanlar Aranıyor...",
            sInfo: "Gösterilen: _START_ - _END_, Toplam Antrenman: _TOTAL_",
            sInfoEmpty: "Gösterilen: 0 - 0, Toplam Antrenman: 0",
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

    var allData;

    GetTables();

    // Tablo satırına tıklandığında
    $('.dataTables-example tbody').on('click', 'tr', function () {
        // Tıklanan satırdaki verileri al
        var rowData = table.row(this).data();

        // Konsola yazdır
        console.log(rowData);

        if (rowData) {
            var tableRow = '';

            rowData.players.forEach((item, index) => {
                tableRow += `<tr class="trainTr">
                <td class="text-center">${item.name}</td>
                <td class="text-center">${item.accept ? "Onaylandı" : "Onaylanmadı"}</td>
            </tr>`;
            });

            Swal.fire({
                background: '#111111',
                width: 1000,
                customClass: {
                    denyButton: 'pop-up-button btn-success',
                    confirmButton: 'pop-up-button btn-primary',
                    cancelButton: 'pop-up-button btn-danger',
                },
                confirmButtonText: "Çık",
                showCancelButton: true,
                cancelButtonText: "Antrenmanı Sil",
                html: `
                    <div class="wrapper wrapper-content animated fadeInRight">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <div class="table-responsive">
                                            <table class="table table-hover issue-tracker">
                                            <thead>
                                                <th>
                                                    Oyuncu İsmi
                                                </th>
                                                <th>
                                                Durum
                                                </th>

                                            </thead>
                                                <tbody>
                                                    ${tableRow}
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                `
            }).then((result) => {
                if (result.dismiss === Swal.DismissReason.cancel) {
                    Swal.fire({
                        text: 'Bu İşlem Geri Alınamaz. Emin Misiniz?',
                        icon: 'warning',
                        background: '#111111',
                        showCancelButton: true,
                        confirmButtonText: "Çık",
                        cancelButtonText: "Antrenmanı Sil",
                        customClass: {
                            confirmButton: 'pop-up-button btn-primary',
                            cancelButton: 'pop-up-button btn-danger',
                        }
                    }).then(result => {
                        if (result.dismiss === Swal.DismissReason.cancel) {
                            $.ajax({
                                url: '/Admin/Training/DeleteTraining/' + rowData.id,
                                method: 'POST',
                                success: function (data) {
                                    if (data.success) {
                                        Swal.fire({
                                            text: data.message,
                                            icon: 'success',
                                            timer: 1500,
                                            showConfirmButton: false,
                                            background: '#111111'
                                        });
                                    }
                                    else {
                                        Swal.fire({
                                            text: data.message,
                                            icon: 'error',
                                            timer: 1500,
                                            showConfirmButton: false,
                                            background: '#111111'
                                        });
                                    }
                                },
                                error: function () {
                                    Swal.fire({
                                        text: 'Sistemsel Bir Hata Gerçekleşti',
                                        icon: 'error',
                                        timer: 1500,
                                        showConfirmButton: false,
                                        background: '#111111'
                                    });
                                }
                            });
                        }
                        else if (result.isConfirmed) {
                            Swal.fire({
                                text: 'Çıkış Gerçekleştiriliyor...',
                                icon: 'success',
                                background: '#111111',
                                showConfirmButton: false,
                                timer: 1500
                            });
                        }
                    });
                }
                else if (result.isConfirmed) {
                    Swal.fire({
                        text: 'Çıkış Gerçekleştiriliyor...',
                        icon: 'success',
                        background: '#111111',
                        showConfirmButton: false,
                        timer: 1500
                    });
                }
            })
        }
    });

    function GetTables() {
        $.ajax({
            url: '/Admin/Training/GetTrainings',
            method: 'GET',
            success: (data) => {
                
                allData = data;

                data.forEach(x => {
                    x.date = formatDateWithHours(x.date);
                });
                
                table.clear(); // DataTable'daki verileri temizle
                table.rows.add(data); // Yeni verileri ekle
                table.draw(); // DataTable'ı güncelle 
            },
            error: (err) => {
                console.log(err);
            }
        });
    }
});
