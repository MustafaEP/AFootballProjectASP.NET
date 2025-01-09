var countryInformation; // AJAX'tan gelen veriler burada saklanacak
var chartsData = [];    // İşlenecek veriler için boş bir dizi

am5.ready(function () {
    var root = am5.Root.new("chartdiv");
    var theme = document.documentElement.getAttribute("data-bs-theme");
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

    var chart = root.container.children.push(
        am5map.MapChart.new(root, {
            panX: "rotateX",
            panY: "rotateY",
            projection: am5map.geoMercator()
        })
    );



    var backgroundSeries = chart.series.push(am5map.MapPolygonSeries.new(root, {}));
    backgroundSeries.mapPolygons.template.setAll({
        fill: root.interfaceColors.get("alternativeBackground"),
        fillOpacity: 0,
        strokeOpacity: 0
    });

    backgroundSeries.data.push({
        geometry: am5map.getGeoRectangle(90, 180, -90, -180)
    });

    var polygonSeries = chart.series.push(
        am5map.MapPolygonSeries.new(root, {
            geoJSON: am5geodata_worldLow,
            exclude: ["AQ"]
        })
    );

    polygonSeries.mapPolygons.template.setAll({
        fill: root.interfaceColors.get("alternativeBackground"),
        fillOpacity: 0.15,
        strokeWidth: 0.5,
        stroke: root.interfaceColors.get("background")
    });

    var circleTemplate = am5.Template.new({
        tooltipText: "{name}: {value}"
    });

    var bubbleSeries = chart.series.push(
        am5map.MapPointSeries.new(root, {
            calculateAggregates: true,
            valueField: "value",
            polygonIdField: "id"
        })
    );

    bubbleSeries.bullets.push(function () {
        return am5.Bullet.new(root, {
            sprite: am5.Circle.new(root, {
                radius: 10,
                templateField: "circleTemplate"
            }, circleTemplate)
        });
    });

    bubbleSeries.set("heatRules", [{
        target: circleTemplate,
        min: 3,
        max: 30,
        key: "radius",
        dataField: "value"
    }]);

    var colors = am5.ColorSet.new(root, {});

    // AJAX Çağrısı
    $.ajax({
        url: '/Manager/Club/GetMapData',
        method: 'GET',
        success: function (data) {
            countryInformation = data; // Veriyi kaydediyoruz
            console.log("Ülke Bilgileri:", countryInformation);

            // Verileri chartsData'ya ekle
            countryInformation.forEach(function (item, index) {
                chartsData.push({
                    id: item.id,
                    name: item.country,
                    value: item.value,
                    circleTemplate: item.value > 0
                        ? { fill: colors.getIndex(index % 10) }
                        : {} 
                });
            });

            console.log("Chart Verileri:", chartsData);

            // Veri işlendi, bubbleSeries'e setAll ile aktar
            bubbleSeries.data.setAll(chartsData);
        },
        error: function () {
            console.log("Ülke Bilgileri Getirilemedi!");
        }
    });

    // Globe/Map Switch
    var cont = chart.children.push(am5.Container.new(root, {
        layout: root.horizontalLayout,
        x: 20,
        y: 40
    }));
    var theme = document.documentElement.getAttribute("data-bs-theme");
    var textColor;
    if (theme === "dark") {
        textColor = am5.color(0xFFFFFF); // Koyu tema için açık metin rengi
    } else {
        textColor = am5.color(0x000000); // Açık tema için siyah metin rengi
    }


    cont.children.push(am5.Label.new(root, {
        centerY: am5.p50,
        text: "Map",
        fill: textColor
    }));



    var switchButton = cont.children.push(
        am5.Button.new(root, {
            themeTags: ["switch"],
            centerY: am5.p50,
            icon: am5.Circle.new(root, {
                themeTags: ["icon"]
            })
        })
    );

    switchButton.on("active", function () {
        if (!switchButton.get("active")) {
            chart.set("projection", am5map.geoMercator());
            backgroundSeries.mapPolygons.template.set("fillOpacity", 0);
        } else {
            chart.set("projection", am5map.geoOrthographic());
            backgroundSeries.mapPolygons.template.set("fillOpacity", 0.1);
        }
    });

    cont.children.push(
        am5.Label.new(root, {
            centerY: am5.p50,
            text: "Globe",
            fill: textColor
        })
    );

    chart.appear(1000, 100);

}); 

