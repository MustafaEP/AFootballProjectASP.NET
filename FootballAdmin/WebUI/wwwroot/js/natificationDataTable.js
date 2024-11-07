$(document).ready(function () {
    var table = $('.dataTables-example').DataTable({
        pageLength: 25,
        responsive: true,
        paging: false,
        ordering: true,
        dom: '<"html5buttons"B>lTfgitp',
        columns: [
            { data: 'type' },
            { data: 'message' },
            { data: 'createdTime' }
        ],
        buttons: [
            { extend: 'copy', text: 'Kopyala' },
            { extend: 'csv', text: 'CSV' },
            { extend: 'excel', text: 'Excel', title: 'Bildirimler' },
            { extend: 'pdf', text: 'PDF', title: 'Bildirimler' },
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
            sZeroRecords: "Bildirimler Aranıyor...",
            sInfo: "Gösterilen: _START_ - _END_, Toplam Bildirim: _TOTAL_",
            sInfoEmpty: "Gösterilen: 0 - 0, Toplam Bildirim: 0",
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
    GetTables();

    setInterval(GetTables, 10000);
    function GetTables() {
        $.ajax({
            url: '/Admin/Notification/GetNotifications',
            method: 'GET',
            success: (data) => {
                table.clear();
                table.rows.add(data);
                table.draw();
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    text: 'Veriler Getirilemedi',
                    background: '#111111'
                })
            }
        });
    }

});