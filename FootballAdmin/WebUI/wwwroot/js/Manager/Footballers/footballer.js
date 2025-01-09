
var root;
am5.ready(function () {

    // Create root element
    // https://www.amcharts.com/docs/v5/getting-started/#Root_element
    var root = am5.Root.new("chartdiv");

    var theme = document.documentElement.getAttribute("data-bs-theme");
    // Set themes
    // https://www.amcharts.com/docs/v5/concepts/themes/
    if (theme === "dark") {
        root.setThemes([
            am5themes_Dark.new(root)
        ]);
    }
    else {
        root.setThemes([
            am5themes_Frozen.new(root)
        ]);
    }


    // Create chart
    // https://www.amcharts.com/docs/v5/charts/xy-chart/
    var chart = root.container.children.push(am5xy.XYChart.new(root, {
        panX: true,
        panY: true,
        wheelX: "panX",
        wheelY: "zoomX",
        scrollbarX: am5.Scrollbar.new(root, { orientation: "horizontal" }),
        scrollbarY: am5.Scrollbar.new(root, { orientation: "vertical" }),
        pinchZoomX: true,
        paddingLeft: 0
    }));


    // Add cursor
    // https://www.amcharts.com/docs/v5/charts/xy-chart/cursor/
    var cursor = chart.set("cursor", am5xy.XYCursor.new(root, {}));
    cursor.lineY.set("visible", false);


    // Create axes
    // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
    var xRenderer = am5xy.AxisRendererX.new(root, {
        minGridDistance: 15,
        minorGridEnabled: true
    });

    xRenderer.labels.template.setAll({
        rotation: -90,
        centerY: am5.p50,
        centerX: 0
    });

    xRenderer.grid.template.setAll({
        visible: false
    });

    var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
        maxDeviation: 0.3,
        categoryField: "category",
        renderer: xRenderer,
        tooltip: am5.Tooltip.new(root, {})
    }));

    var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
        maxDeviation: 0.3,
        renderer: am5xy.AxisRendererY.new(root, {})
    }));


    // Create series
    // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
    var series = chart.series.push(am5xy.ColumnSeries.new(root, {
        xAxis: xAxis,
        yAxis: yAxis,
        valueYField: "value",
        categoryXField: "category",
        adjustBulletPosition: false,
        tooltip: am5.Tooltip.new(root, {
            labelText: "{valueY}"
        })
    }));
    series.columns.template.setAll({
        width: 0.5
    });

    series.bullets.push(function () {
        return am5.Bullet.new(root, {
            locationY: 1,
            sprite: am5.Circle.new(root, {
                radius: 5,
                fill: series.get("fill")
            })
        })
    })


    // Set data
    var data = [];
    var value = 120;

    var names = ["Forvet",
        "Orta Saha",
        "Defans",
        "Kaleci",
    ];

    data.push({ category: names[0], value: 1 });
    data.push({ category: names[1], value: 2 });
    data.push({ category: names[2], value: 3 });
    data.push({ category: names[3], value: 4 });

    xAxis.data.setAll(data);
    series.data.setAll(data);


    // Make stuff animate on load
    // https://www.amcharts.com/docs/v5/concepts/animations/
    series.appear(1000);
    chart.appear(1000, 100);



    const selectElement = document.getElementById('preferredPlayer');
    selectElement.addEventListener('change', function () {

        const chartUpDiv = document.getElementById('chart_up_div');
        root.dispose();
        chartUpDiv.innerHTML = '';

        const newDiv = document.createElement('div');
        newDiv.id = 'chartdiv';

        chartUpDiv.appendChild(newDiv);

        const selectedValue = selectElement.value;
        console.log('Seçilen Oyuncu ID: ', selectedValue);

        $.ajax({
            url: '/Manager/Club/ClubPlayersJSON/' + selectedValue,
            method: 'GET',
            success: (dataValue) => {
                // Create root element
                // https://www.amcharts.com/docs/v5/getting-started/#Root_element
                var root = am5.Root.new("chartdiv");


                // Set themes
                // https://www.amcharts.com/docs/v5/concepts/themes/
                if (theme === "dark") {
                    root.setThemes([
                        am5themes_Dark.new(root)
                    ]);
                }
                else {
                    root.setThemes([
                        am5themes_Frozen.new(root)
                    ]);
                }


                // Create chart
                // https://www.amcharts.com/docs/v5/charts/xy-chart/
                var chart = root.container.children.push(am5xy.XYChart.new(root, {
                    panX: true,
                    panY: true,
                    wheelX: "panX",
                    wheelY: "zoomX",
                    scrollbarX: am5.Scrollbar.new(root, { orientation: "horizontal" }),
                    scrollbarY: am5.Scrollbar.new(root, { orientation: "vertical" }),
                    pinchZoomX: true,
                    paddingLeft: 0
                }));


                // Add cursor
                // https://www.amcharts.com/docs/v5/charts/xy-chart/cursor/
                var cursor = chart.set("cursor", am5xy.XYCursor.new(root, {}));
                cursor.lineY.set("visible", false);


                // Create axes
                // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
                var xRenderer = am5xy.AxisRendererX.new(root, {
                    minGridDistance: 15,
                    minorGridEnabled: true
                });

                xRenderer.labels.template.setAll({
                    rotation: -90,
                    centerY: am5.p50,
                    centerX: 0
                });

                xRenderer.grid.template.setAll({
                    visible: false
                });

                var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
                    maxDeviation: 0.3,
                    categoryField: "category",
                    renderer: xRenderer,
                    tooltip: am5.Tooltip.new(root, {})
                }));

                var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
                    maxDeviation: 0.3,
                    renderer: am5xy.AxisRendererY.new(root, {})
                }));


                // Create series
                // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
                var series = chart.series.push(am5xy.ColumnSeries.new(root, {
                    xAxis: xAxis,
                    yAxis: yAxis,
                    valueYField: "value",
                    categoryXField: "category",
                    adjustBulletPosition: false,
                    tooltip: am5.Tooltip.new(root, {
                        labelText: "{valueY}"
                    })
                }));
                series.columns.template.setAll({
                    width: 0.5
                });

                series.bullets.push(function () {
                    return am5.Bullet.new(root, {
                        locationY: 1,
                        sprite: am5.Circle.new(root, {
                            radius: 5,
                            fill: series.get("fill")
                        })
                    })
                })


                // Set data
                var data = [];
                var value = 120;

                var names = ["Hız",
                    "Şut",
                    "Pas",
                    "Dribling",
                    "Defans",
                    "Fizik",
                ];

                data.push({ category: names[0], value: dataValue.pace });
                data.push({ category: names[1], value: dataValue.shooting });
                data.push({ category: names[2], value: dataValue.passing });
                data.push({ category: names[3], value: dataValue.dribbling });
                data.push({ category: names[4], value: dataValue.defending });
                data.push({ category: names[5], value: dataValue.physicality });

                xAxis.data.setAll(data);
                series.data.setAll(data);


                // Make stuff animate on load
                // https://www.amcharts.com/docs/v5/concepts/animations/
                series.appear(1000);
                chart.appear(1000, 100);

            }
        });

    });

}); // end am5.ready()
