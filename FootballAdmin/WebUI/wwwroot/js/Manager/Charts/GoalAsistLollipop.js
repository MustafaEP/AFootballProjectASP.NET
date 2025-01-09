am5.ready(function () {

    $.ajax({
        url: '/Manager/DataJson/GoalAsist',
        method: 'GET',
        dataType: 'json',
        success: function (dataGet) {
            // Create root element for the first chart
            var rootGoal = am5.Root.new("chartdiv2");
            var theme = document.documentElement.getAttribute("data-bs-theme");

            // Set themes for the first chart
            if (theme === "dark") {
                rootGoal.setThemes([am5themes_Dark.new(rootGoal)]);
            } else {
                rootGoal.setThemes([am5themes_Frozen.new(rootGoal)]);
            }

            // Create first chart
            var chartGoal = rootGoal.container.children.push(am5xy.XYChart.new(rootGoal, {
                panX: true,
                panY: true,
                wheelX: "panX",
                wheelY: "zoomX",
                scrollbarX: am5.Scrollbar.new(rootGoal, { orientation: "horizontal" }),
                scrollbarY: am5.Scrollbar.new(rootGoal, { orientation: "vertical" }),
                pinchZoomX: true,
                paddingLeft: 0
            }));

            // Add cursor for the first chart
            var cursorGoal = chartGoal.set("cursor", am5xy.XYCursor.new(rootGoal, {}));
            cursorGoal.lineY.set("visible", false);

            // Create axes for the first chart
            var xRendererGoal = am5xy.AxisRendererX.new(rootGoal, {
                minGridDistance: 15,
                minorGridEnabled: true
            });

            xRendererGoal.labels.template.setAll({
                rotation: -90,
                centerY: am5.p50,
                centerX: 0
            });

            xRendererGoal.grid.template.setAll({
                visible: false
            });

            var xAxisGoal = chartGoal.xAxes.push(am5xy.CategoryAxis.new(rootGoal, {
                maxDeviation: 0.3,
                categoryField: "category",
                renderer: xRendererGoal,
                tooltip: am5.Tooltip.new(rootGoal, {})
            }));

            var yAxisGoal = chartGoal.yAxes.push(am5xy.ValueAxis.new(rootGoal, {
                maxDeviation: 0.3,
                renderer: am5xy.AxisRendererY.new(rootGoal, {})
            }));

            // Create series for the first chart
            var seriesGoal = chartGoal.series.push(am5xy.ColumnSeries.new(rootGoal, {
                xAxis: xAxisGoal,
                yAxis: yAxisGoal,
                valueYField: "value",
                categoryXField: "category",
                adjustBulletPosition: false,
                tooltip: am5.Tooltip.new(rootGoal, { labelText: "{valueY}" })
            }));

            seriesGoal.columns.template.setAll({
                width: 0.5
            });

            seriesGoal.bullets.push(function () {
                return am5.Bullet.new(rootGoal, {
                    locationY: 1,
                    sprite: am5.Circle.new(rootGoal, {
                        radius: 5,
                        fill: seriesGoal.get("fill")
                    })
                })
            });

            // Set data for the first chart
            var dataGoal = [];
            dataGet.forEach(footballer => {
                dataGoal.push({ category: footballer.fullName, value: footballer.goal });
            });

            xAxisGoal.data.setAll(dataGoal);
            seriesGoal.data.setAll(dataGoal);

            // Make stuff animate on load for the first chart
            seriesGoal.appear(1000);
            chartGoal.appear(1000, 100);




            // Create root element for the second chart
            var rootAssist = am5.Root.new("chartdiv3");

            // Set themes for the second chart
            if (theme === "dark") {
                rootAssist.setThemes([am5themes_Dark.new(rootAssist)]);
            } else {
                rootAssist.setThemes([am5themes_Frozen.new(rootAssist)]);
            }

            // Create second chart
            var chartAssist = rootAssist.container.children.push(am5xy.XYChart.new(rootAssist, {
                panX: true,
                panY: true,
                wheelX: "panX",
                wheelY: "zoomX",
                scrollbarX: am5.Scrollbar.new(rootAssist, { orientation: "horizontal" }),
                scrollbarY: am5.Scrollbar.new(rootAssist, { orientation: "vertical" }),
                pinchZoomX: true,
                paddingLeft: 0
            }));

            // Add cursor for the second chart
            var cursorAssist = chartAssist.set("cursor", am5xy.XYCursor.new(rootAssist, {}));
            cursorAssist.lineY.set("visible", false);

            // Create axes for the second chart
            var xRendererAssist = am5xy.AxisRendererX.new(rootAssist, {
                minGridDistance: 15,
                minorGridEnabled: true
            });

            xRendererAssist.labels.template.setAll({
                rotation: -90,
                centerY: am5.p50,
                centerX: 0
            });

            xRendererAssist.grid.template.setAll({
                visible: false
            });

            var xAxisAssist = chartAssist.xAxes.push(am5xy.CategoryAxis.new(rootAssist, {
                maxDeviation: 0.3,
                categoryField: "category",
                renderer: xRendererAssist,
                tooltip: am5.Tooltip.new(rootAssist, {})
            }));

            var yAxisAssist = chartAssist.yAxes.push(am5xy.ValueAxis.new(rootAssist, {
                maxDeviation: 0.3,
                renderer: am5xy.AxisRendererY.new(rootAssist, {})
            }));

            // Create series for the second chart
            var seriesAssist = chartAssist.series.push(am5xy.ColumnSeries.new(rootAssist, {
                xAxis: xAxisAssist,
                yAxis: yAxisAssist,
                valueYField: "value",
                categoryXField: "category",
                adjustBulletPosition: false,
                tooltip: am5.Tooltip.new(rootAssist, { labelText: "{valueY}" })
            }));

            seriesAssist.columns.template.setAll({
                width: 0.5
            });

            seriesAssist.bullets.push(function () {
                return am5.Bullet.new(rootAssist, {
                    locationY: 1,
                    sprite: am5.Circle.new(rootAssist, {
                        radius: 5,
                        fill: seriesAssist.get("fill")
                    })
                })
            });

            // Set data for the second chart
            var dataAssist = [];
            dataGet.forEach(footballer => {
                dataAssist.push({ category: footballer.fullName, value: footballer.asist });
            });

            xAxisAssist.data.setAll(dataAssist);
            seriesAssist.data.setAll(dataAssist);

            // Make stuff animate on load for the second chart
            seriesAssist.appear(1000);
            chartAssist.appear(1000, 100);

        }
    });

});
