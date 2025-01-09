var role = "";

$(document).ready(function () {
    $('.table').DataTable({
        "paging": true, // Sayfalama
        "searching": true, // Arama aktif
        "ordering": true, // Sıralama
        "info": true, // Bilgi kısmı
        "responsive": true, // Mobil uyumluluk
        "dom": '<"top"f>rt<"bottom d-flex justify-content-sm-between"lp><"clear">', // Dom yapısını özelleştir
        "language": {
            "search": "Ara:",
            "lengthMenu": "Her sayfada _MENU_ oyuncu göster",
            "zeroRecords": "Eşleşen oyuncu bulunamadı",
            "info": "Toplam _TOTAL_ oyuncudan _START_ - _END_ arası gösteriliyor",
            "infoEmpty": "Gösterilecek oyuncu yok",
            "infoFiltered": "(toplam _MAX_ oyuncudan filtrelendi)",
            "loadingRecords": "Yükleniyor...",
            "processing": "İşleniyor...",
            "emptyTable": "Tabloda henüz veri yok",
        },
        "initComplete": function () {
            $(".dataTables_filter input").removeClass().addClass("form-control form-control-solid");
        }
    });

});
function DetailFootballer(id) {
    getUserRole();
    console.log(role);
    $.ajax({
        url: '/Footballer/FootballerJson/' + id,
        method: 'GET',
        success: (footballer) => {
            console.log(footballer);

            Swal.fire({
                showConfirmButton: false,
                width: 1000,
                html: GetPlayerHtml(footballer),
                didOpen: () => {
                    // Radar Chart Initialization
                    InitializeRadarChart(footballer);
                }
            });
        }
    });
}

function GetPlayerHtml(footballer) {
    var html = `
        <div style="display: flex; flex-direction: column; align-items: center;">
            <div>
                <h2>${footballer.name} ${footballer.surName}</h2>
                <div class="btn-group" role="group" aria-label="Action Buttons">
                    
    `
    var buyFootballer = ``;
    if (role == "Manager") {
        buyFootballer = `<button type="button" class="btn btn-success btn-sm" onclick = "buyFootballer(${footballer.id})">Satın Al</button>`
        html += buyFootballer;
    }
    html += `<button type="button" class="btn btn-primary btn-sm" onclick="window.location.href = '/Footballer/Footballer/${footballer.id}'">Detaylar</button>
                </div>
            </div>
            <div id="radarChart" style="width: 500px; height: 400px; margin-top: 20px;"></div>
        </div>`;

    return html;
}

function InitializeRadarChart(footballer) {
    console.log(footballer);
    const statistic = footballer.statistic;
    const chartDom = document.getElementById('radarChart');
    const myChart = echarts.init(chartDom);

    // Sabit belirgin renkler
    const primaryColor = '#FF4500'; // Grafik için ana renk (Turuncu-kırmızı tonu)
    const textColor = '#FF4500'; // Yazılar için renk
    const axisLineColor = '#888'; // Eksen çizgileri için renk

    const option = {
        tooltip: {
            textStyle: {
                color: textColor
            },
            backgroundColor: '#FFFFFF',
            borderColor: primaryColor
        },
        radar: {
            shape: 'polygon',
            splitLine: {
                lineStyle: {
                    color: axisLineColor
                }
            },
            splitArea: {
                areaStyle: {
                    color: 'transparent' // İç bölge şeffaf
                }
            },
            axisLine: {
                lineStyle: {
                    color: axisLineColor
                }
            },
            indicator: [
                { name: 'Hız', max: 100, color: textColor },
                { name: 'Pas', max: 100, color: textColor },
                { name: 'Şut', max: 100, color: textColor },
                { name: 'Dribling', max: 100, color: textColor },
                { name: 'Fizik', max: 100, color: textColor },
                { name: 'Defans', max: 100, color: textColor }
            ]
        },
        series: [{
            name: 'Player Skills',
            type: 'radar',
            areaStyle: {
                color: 'rgba(255, 69, 0, 0.3)' // Alan dolgu rengi (Turuncu-kırmızı tonu, opak)
            },
            lineStyle: {
                color: primaryColor, // Çizgi rengi
                width: 2 // Çizgi kalınlığı
            },
            itemStyle: {
                color: primaryColor // Veri noktası rengi
            },
            label: {
                show: true, // Etiketleri göster
                color: primaryColor, // Etiket rengi
                fontSize: 12, // Yazı boyutu
                formatter: (params) => params.value // Her bir noktanın değeri
            },
            data: [
                {
                    value: [
                        statistic.pace,
                        statistic.passing,
                        statistic.shooting,
                        statistic.dribbling,
                        statistic.physicality,
                        statistic.defending
                    ],
                    name: 'Skills'
                }
            ]
        }]
    };

    myChart.setOption(option);
}


function getUserRole() {
    $.ajax({
        url: '/Home/GetUserRole', // Controller endpoint URL
        method: 'GET',
        success: (res) => {
            role = res;
        },
        error: (error) => {
            console.error("Rol bilgisi alınamadı");
        }
    });
}


function buyFootballer(id) {
    $.ajax({
        url: '/Manager/Transfer/BuyFootballer/' + id,
        method: 'POST',
        success: (data) => {
            Message(data.success, data.message);
        },
        error: () => {
            ErrorMessage();
        }
    })
}
