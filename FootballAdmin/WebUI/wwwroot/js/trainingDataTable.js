$(document).ready(() => {
    var table = $('.dataTables-example').DataTable({
        pageLength: 25,
        responsive: true,
        paging: false,
        ordering: true,
        dom: '<"html5buttons"B>lTfgitp',
        columns: [
            { data: 'trainingName' },
            { data: 'teamName' },
            { data: 'managerName' },
            { data: 'date' },
        ],
        buttons: [
            { extend: 'copy', text: 'Kopyala' },
            { extend: 'csv', text: 'CSV' },
            { extend: 'excel', text: 'Excel', title: 'Oyuncular' },
            { extend: 'pdf', text: 'PDF', title: 'Oyuncular' },
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
            sZeroRecords: "Menejerler Aranıyor...",
            sInfo: "Gösterilen: _START_ - _END_, Toplam Menejer: _TOTAL_",
            sInfoEmpty: "Gösterilen: 0 - 0, Toplam Menejer: 0",
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
    const GetTables = () => {
        $.ajax({
            url: '/Admin/Training/GetTrainings',
            method: 'GET',
            success: (data) => {
                console.log(data);
                allData = data;
                table.clear(); // DataTable'daki verileri temizle
                table.rows.add(data); // Yeni verileri ekle
                table.draw(); // DataTable'ı güncelle 
            },
            error: (err) => {
                console.log(err);
            }
        });
    }
    GetTables();

    $('.dataTables-example tbody').on('click', 'tr', () => {
        Swal.fire({
            background: '#02365e',
            width: 1000,
            html: `
                <div class="wrapper wrapper-content  animated fadeInRight">
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox">
                <div class="ibox-content">

                    <div class="table-responsive">
                        <table class="table table-hover issue-tracker">
                            <tbody id="players">
                                <tr class="trainTr">
                                    <td>
                                        Oyuncu Adı Soyadı
                                    </td>
                                    <td class="text-right">
                                        Antrenmanı Onayladımı
                                    </td>
                                </tr>
                                <tr class="trainTr">
                                    <td>
                                        Oyuncu Adı Soyadı
                                    </td>
                                    <td class="text-right">
                                        Antrenmanı Onayladımı
                                    </td>
                                </tr>
                                <tr class="trainTr">
                                    <td>
                                        Oyuncu Adı Soyadı
                                    </td>
                                    <td class="text-right">
                                        Antrenmanı Onayladımı
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

            </div>
        </div>
    </div>


</div>
            `
        });
    });

});  