GetTables();
ManagerClub();
function GetTables() {
    $.ajax({
        url: '/Manager/Transfer/GetFootballers',
        method: 'GET',
        success: function (data) {
            const tbody = document.querySelector(".table tbody");
            tbody.innerHTML = ""; // Mevcut satırları temizle
            console.log(data);
            data.$values.forEach(item => {
                const trTable = document.createElement("tr");
                trTable.setAttribute("data-id", item.id);
                trTable.className = "cursor-pointer";
                // Tablo satırını oluştur
                trTable.innerHTML = `
                                        <td class="text-start align-middle">${item.Name} ${item.SurName}</td>
                                        <td class="text-start align-middle">${item.Country}</td>
                                        <td class="text-start align-middle">${item.FootballerPositions.$values[0].ShortPosition}</td>
                                        <td class="text-start align-middle">${item.Statistic.Power}</td>
                                        <td class="text-start align-middle">${item.Statistic.Pace}</td>
                                        <td class="text-start align-middle">${item.Statistic.Shooting}</td>
                                        <td class="text-start align-middle">${item.Statistic.Passing}</td>
                                        <td class="text-start align-middle">${item.Statistic.Dribbling}</td>
                                        <td class="text-start align-middle">${item.Statistic.Defending}</td>
                                        <td class="text-start align-middle">${item.Statistic.Physicality}</td>
                                        <td class="text-start align-middle">${item.MarketPrice}</td>
                `;

                // Satıra tıklama olayını ekle
                trTable.addEventListener("click", function () {
                    ShowPlayerDetails(item.Id);
                });

                tbody.appendChild(trTable);
            });



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





        },
        error: function () {
            Swal.fire({
                text: 'Veriler Gelmedi',
                icon: 'error',
                showConfirmButton: false
            });
        }
    });
}

function ShowPlayerDetails(footballerId) {
    $.ajax({
        url: '/Manager/Transfer/GetFootballer/' + footballerId,
        method: 'GET',
        success: (footballer) => {
            console.log(footballer);

            Swal.fire({
                showConfirmButton: false,
                width: 1000,
                html: GetPlayerHtml(footballer),

            });

            am5.ready(function () {
                var root = am5.Root.new("chartdiv");
                var theme = document.documentElement.getAttribute("data-bs-theme");

                if (theme == "dark") {
                    root.setThemes([
                        am5themes_Dark.new(root)
                        //thema kısmı ayarlanacak
                    ]);
                } else {
                    root.setThemes([
                        am5themes_Responsive.new(root)
                        //thema kısmı ayarlanacak
                    ]);
                }


                var chart = root.container.children.push(am5radar.RadarChart.new(root, {
                    panX: false,
                    panY: false,
                    wheelX: "panX",
                    wheelY: "zoomX"
                }));

                var cursor = chart.set("cursor", am5radar.RadarCursor.new(root, {
                    behavior: "zoomX"
                }));

                cursor.lineY.set("visible", false);


                // Create axes and their renderers
                // https://www.amcharts.com/docs/v5/charts/radar-chart/#Adding_axes
                var xRenderer = am5radar.AxisRendererCircular.new(root, {});
                xRenderer.labels.template.setAll({
                    radius: 10
                });

                var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
                    maxDeviation: 0,
                    categoryField: "property",
                    renderer: xRenderer,
                    tooltip: am5.Tooltip.new(root, {})
                }));

                var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
                    renderer: am5radar.AxisRendererRadial.new(root, {}),
                    min: 0, // Minimum sınır değeri
                    max: 100 // Maksimum sınır değeri
                }));


                // Create series
                // https://www.amcharts.com/docs/v5/charts/radar-chart/#Adding_series
                var series = chart.series.push(am5radar.RadarLineSeries.new(root, {
                    name: "Series",
                    xAxis: xAxis,
                    yAxis: yAxis,
                    valueYField: "power",
                    categoryXField: "property",
                    tooltip: am5.Tooltip.new(root, {
                        labelText: "{valueY}"
                    })
                }));

                series.strokes.template.setAll({
                    strokeWidth: 2
                });

                series.bullets.push(function () {
                    return am5.Bullet.new(root, {
                        sprite: am5.Circle.new(root, {
                            radius: 5,
                            fill: series.get("fill")
                        })
                    });
                });


                // Set data
                // https://www.amcharts.com/docs/v5/charts/radar-chart/#Setting_data
                var data = [{
                    "property": "Defans",
                    "power": footballer.statistic.defending
                }, {
                    "property": "Dribling",
                    "power": footballer.statistic.dribbling
                }, {
                    "property": "Hız",
                    "power": footballer.statistic.pace
                }, {
                    "property": "Fizik",
                    "power": footballer.statistic.physicality
                }, {
                    "property": "Şut",
                    "power": footballer.statistic.shooting
                }, {
                    "property": "Pas",
                    "power": footballer.statistic.passing
                }];
                series.data.setAll(data);
                xAxis.data.setAll(data);


                // Animate chart and series in
                // https://www.amcharts.com/docs/v5/concepts/animations/#Initial_animation
                series.appear(1000);
                chart.appear(1000, 100);

            }); // end am5.ready()

        }
    })
}

function GetPlayerHtml(data) {
    var colors = ["bg-danger text-inverse-danger", "bg-primary text-inverse-primary", "bg-success text-inverse-success"];
    function getRandomColor() {
        // Diziden rastgele bir indeks seç
        var randomIndex = Math.floor(Math.random() * colors.length);
        return colors[randomIndex];
    }

    let htmlPositions = `<p class="text-gray-800 text-hover-primary fs-6 fw-bold">`;
    data.footballerPositions.forEach(position => {
        htmlPositions += ` | ${position.position}`;
    });
    htmlPositions += `</p>`;

    var html = `
    <div class="col-xl-12">
        <div class="card mb-5 mb-xl-8">
            <div class="card-body pt-6">
                <div class="d-flex flex-stack">
                    <div class="symbol symbol-40px me-4">
                        <div class="symbol-label fs-2 fw-semibold ${getRandomColor()}">${data.name[0]}</div>
                    </div>
                    <div class="d-flex align-items-center flex-row-fluid flex-wrap">
                        <div class="flex-grow-1 me-2">
                            <p class="text-gray-800 text-hover-primary fs-6 fw-bold">${data.name} ${data.surName}</p>
                            <span class="text-muted fw-semibold d-block fs-7">${translateCountry(data.country)}</span>
                        </div>
                        <button onclick="FootballDetailPage(${data.id})" class="btn btn-sm btn-bg-light btn-active-color-primary h-40px">
                            Detay
                        </button>
                        <button onclick="BuyFootboller(${data.id})" class="btn btn-sm btn-bg-light btn-active-color-primary h-40px">
                            Satın Al
                        </button>
                    </div>
                </div>
                <div class="separator separator-dashed my-4"></div>
                        <div class="flex-grow-1 me-2">
                            <p class="text-gray-800 text-hover-primary fs-6 fw-bold">${htmlPositions}</p>
                            
                        </div>
                <div class="separator separator-dashed my-4"></div>
                    <div id="chartdiv"></div>
            </div>
        </div>
    </div>
`;
    return html;
}

function FootballDetailPage(footballerId) {
    console.log(footballerId);
    window.location.href = `/Manager/Transfer/Footballer/${footballerId}`;
}

//<span class="text-muted fw-semibold d-block fs-7">${data.footballerPositions[0].description}</span>

function ManagerClub() {
    $.ajax({
        url: '/Manager/Club/ClubInfo',
        method: 'GET',
        success: function (data) {
            console.log(data);
            if (data.success) {
                document.getElementById("clubBudget").innerText = "₺" + data.values.budget;
            }
            else {
                DontCreateClub();
            }
        }
    });
}

function AddPlayerClub(id) {
    $.ajax({
        url: '/Manager/Transfer/AddFootballerForClub/' + id,
        method: 'POST',
        data: id,
        success: function (data) {
            if (data.success) {
                Swal.fire({
                    icon: 'success',
                    text: data.message,
                    showConfirmButton: false,
                    timer: 2000
                });

                ManagerClub();
            }
            else {
                Swal.fire({
                    icon: 'error',
                    text: data.message,
                    showConfirmButton: false,
                    timer: 2000
                });
            }
        },
        error: function () {
            Swal.fire({
                text: "Hata",
                showConfirmButton: false,
                timer: 2000
            });
        }
    })
}