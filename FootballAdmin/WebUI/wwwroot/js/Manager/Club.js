var footballers;
var eleven;
let previousSwalHtml = "";
var selectedValue;
var club;

$.ajax({
    url: '/Manager/Club/ClubInfo',
    method: 'GET',
    success: function (data) {
        if (data.success) {
            ClubPlayerList(data.values.footballers);
            club = data.values;
            footballers = data.values.footballers;
            eleven = JSON.parse(data.values.lineUp);
            const preferredLineUpSelect = document.getElementById("preferredLineUp");
            preferredLineUpSelect.value = eleven.formation;
            WriteLineUp();
        }
        else {
            DontCreateClub();
        }
    }
});




preferredLineUp.addEventListener("change", function () {
    WriteLineUp();
});




function WriteLineUp() {
    selectedValue = preferredLineUp.value;

    const offensiveDiv = document.getElementById("offensiveId");
    const midfieldDiv = document.getElementById("midfieldId");
    const backMidfieldDiv = document.getElementById("backMidfieldId");
    const defenceDiv = document.getElementById("defenceId");
    const goalKeeperDiv = document.getElementById("goalKeeperId");

    const LineUp = selectedValue.split("-").map(Number);

    offensiveDiv.innerHTML = ``
    midfieldDiv.innerHTML = ``
    backMidfieldDiv.innerHTML = ``
    defenceDiv.innerHTML = ``
    goalKeeperDiv.innerHTML = ``;



    const PlayerInEleven = eleven.footballers.map(id => {
        const player = footballers.find(player => player.id === id);

        

        if (player == null) {
            return "Oyuncu Yok";
        } else {
            const strength = (
                (player.defending + player.dribbling + player.pace + player.passing + player.shooting + player.physicality) / 6
            ).toFixed(2); 
            return {
                name: player.name.charAt(0) + ". " + player.surName,
                strength: Math.floor(strength) 
            }
        }
    });



    if (LineUp.length == 3) {
        backMidfieldDiv.style.display = "none";
        var playerNumber = 10;
        
        for (let i = 0; i < LineUp[2]; i++) {
            const playerIsHere = PlayerInEleven[playerNumber] == "Oyuncu Yok";

            const newDiv = document.createElement("div");
            newDiv.className = "d-flex flex-column align-items-center cursor-pointer";
            newDiv.dataset.id = playerNumber;
            newDiv.dataset.position = "offensive"; 
            newDiv.id = "player-" + playerNumber; 

            const dotSpan = document.createElement("span");
            dotSpan.style.fontSize = "24px";
            dotSpan.textContent = ".";

            const nameParagraph = document.createElement("p");
            nameParagraph.className = "m-0";
            nameParagraph.style.fontSize = "12px";
            nameParagraph.textContent = `${playerIsHere ? " " : PlayerInEleven[playerNumber].name}`;

            const strengthParagraph = document.createElement("p");
            strengthParagraph.className = "m-0";
            strengthParagraph.style.fontSize = "12px";
            strengthParagraph.textContent = `${playerIsHere ? " " : String(PlayerInEleven[playerNumber].strength)}`;

            newDiv.appendChild(dotSpan);
            newDiv.appendChild(nameParagraph);
            newDiv.appendChild(strengthParagraph);

            offensiveDiv.appendChild(newDiv);
            playerNumber--;
        }
        for (let i = 0; i < LineUp[1]; i++) {
            const playerIsHere = PlayerInEleven[playerNumber] == "Oyuncu Yok";

            const newDiv = document.createElement("div");
            newDiv.className = "d-flex flex-column align-items-center cursor-pointer";
            newDiv.dataset.id = playerNumber;
            newDiv.dataset.position = "midfield";
            newDiv.id = "player-" + playerNumber; // Her div'e benzersiz id

            const dotSpan = document.createElement("span");
            dotSpan.style.fontSize = "24px";
            dotSpan.textContent = ".";

            const nameParagraph = document.createElement("p");
            nameParagraph.className = "m-0";
            nameParagraph.style.fontSize = "12px";
            nameParagraph.textContent = `${playerIsHere ? " " : PlayerInEleven[playerNumber].name}`;

            const strengthParagraph = document.createElement("p");
            strengthParagraph.className = "m-0";
            strengthParagraph.style.fontSize = "12px";
            strengthParagraph.textContent = `${playerIsHere ? " " : String(PlayerInEleven[playerNumber].strength)}`;
            playerNumber--;
            newDiv.appendChild(dotSpan);
            newDiv.appendChild(nameParagraph);
            newDiv.appendChild(strengthParagraph);

            midfieldDiv.appendChild(newDiv);
        }
        for (let i = 0; i < LineUp[0]; i++) {
            const playerIsHere = PlayerInEleven[playerNumber] == "Oyuncu Yok";

            const newDiv = document.createElement("div");
            newDiv.className = "d-flex flex-column align-items-center cursor-pointer";
            newDiv.dataset.position = "defence";
            newDiv.dataset.id = playerNumber;
            newDiv.id = "player-" + playerNumber; // Her div'e benzersiz id

            const dotSpan = document.createElement("span");
            dotSpan.style.fontSize = "24px";
            dotSpan.textContent = ".";

            const nameParagraph = document.createElement("p");
            nameParagraph.className = "m-0";
            nameParagraph.style.fontSize = "12px";
            nameParagraph.textContent = `${playerIsHere ? " " : PlayerInEleven[playerNumber].name}`;
            const strengthParagraph = document.createElement("p");
            strengthParagraph.className = "m-0";
            strengthParagraph.style.fontSize = "12px";
            strengthParagraph.textContent = `${playerIsHere ? " " : String(PlayerInEleven[playerNumber].strength)}`;
            playerNumber--;

            newDiv.appendChild(dotSpan);
            newDiv.appendChild(nameParagraph);
            newDiv.appendChild(strengthParagraph);

            defenceDiv.appendChild(newDiv);
        }


    }
    if (LineUp.length == 4) {
        backMidfieldDiv.style.display = "flex";
        var playerNumberFour = 10;

        for (let i = 0; i < LineUp[3]; i++) {
            const playerIsHere = PlayerInEleven[playerNumberFour] == "Oyuncu Yok";

            const newDiv = document.createElement("div");
            newDiv.className = "d-flex flex-column align-items-center cursor-pointer";
            newDiv.dataset.position = "offensive";
            newDiv.dataset.id = playerNumberFour;
            newDiv.id = "player-" + playerNumberFour;

            // Span elemanını oluştur
            const dotSpan = document.createElement("span");
            dotSpan.style.fontSize = "24px";
            dotSpan.textContent = ".";

            // P elemanını oluştur
            const nameParagraph = document.createElement("p");
            nameParagraph.className = "m-0";
            nameParagraph.style.fontSize = "12px";
            nameParagraph.textContent = `${playerIsHere ? " " : PlayerInEleven[playerNumberFour].name}`;

            const strengthParagraph = document.createElement("p");
            strengthParagraph.className = "m-0";
            strengthParagraph.style.fontSize = "12px";
            strengthParagraph.textContent = `${playerIsHere ? " " : String(PlayerInEleven[playerNumberFour].strength)}`;
            playerNumberFour--;

            // Span ve P elemanlarını ana div'e ekle
            newDiv.appendChild(dotSpan);
            newDiv.appendChild(nameParagraph);
            newDiv.appendChild(strengthParagraph);

            // Ana div'i offensiveDiv'e ekle
            offensiveDiv.appendChild(newDiv);
        }

        for (let i = 0; i < LineUp[2]; i++) {
            const playerIsHere = PlayerInEleven[playerNumberFour] == "Oyuncu Yok";

            const newDiv = document.createElement("div");
            newDiv.className = "d-flex flex-column align-items-center cursor-pointer";
            newDiv.dataset.position = "midfield";
            newDiv.dataset.id = playerNumberFour;
            newDiv.id = "player-" + playerNumberFour;

            const dotSpan = document.createElement("span");
            dotSpan.style.fontSize = "24px";
            dotSpan.textContent = ".";

            const nameParagraph = document.createElement("p");
            nameParagraph.className = "m-0";
            nameParagraph.style.fontSize = "12px";
            nameParagraph.textContent = `${playerIsHere ? " " : PlayerInEleven[playerNumberFour].name}`;

            const strengthParagraph = document.createElement("p");
            strengthParagraph.className = "m-0";
            strengthParagraph.style.fontSize = "12px";
            strengthParagraph.textContent = `${playerIsHere ? " " : String(PlayerInEleven[playerNumberFour].strength)}`;
            playerNumberFour--;

            newDiv.appendChild(dotSpan);
            newDiv.appendChild(nameParagraph);
            newDiv.appendChild(strengthParagraph);

            midfieldDiv.appendChild(newDiv);
        }
        for (let i = 0; i < LineUp[1]; i++) {
            const playerIsHere = PlayerInEleven[playerNumberFour] == "Oyuncu Yok";

            const newDiv = document.createElement("div");
            newDiv.className = "d-flex flex-column align-items-center cursor-pointer";
            newDiv.dataset.position = "backMidfield";
            newDiv.dataset.id = playerNumberFour;
            newDiv.id = "player-" + playerNumberFour;

            const dotSpan = document.createElement("span");
            dotSpan.style.fontSize = "24px";
            dotSpan.textContent = ".";

            const nameParagraph = document.createElement("p");
            nameParagraph.className = "m-0";
            nameParagraph.style.fontSize = "12px";
            nameParagraph.textContent = `${playerIsHere ? " " : PlayerInEleven[playerNumberFour].name}`;

            const strengthParagraph = document.createElement("p");
            strengthParagraph.className = "m-0";
            strengthParagraph.style.fontSize = "12px";
            strengthParagraph.textContent = `${playerIsHere ? " " : String(PlayerInEleven[playerNumberFour].strength)}`;
            playerNumberFour--;

            newDiv.appendChild(dotSpan);
            newDiv.appendChild(nameParagraph);
            newDiv.appendChild(strengthParagraph);

            backMidfieldDiv.appendChild(newDiv);
        }
        for (let i = 0; i < LineUp[0]; i++) {
            const playerIsHere = PlayerInEleven[playerNumberFour] == "Oyuncu Yok";

            const newDiv = document.createElement("div");
            newDiv.className = "d-flex flex-column align-items-center cursor-pointer";
            newDiv.dataset.position = "defence";
            newDiv.dataset.id = playerNumberFour;
            newDiv.id = "player-" + playerNumberFour;

            const dotSpan = document.createElement("span");
            dotSpan.style.fontSize = "24px";
            dotSpan.textContent = ".";

            const nameParagraph = document.createElement("p");
            nameParagraph.className = "m-0";
            nameParagraph.style.fontSize = "12px";
            nameParagraph.textContent = `${playerIsHere ? " " : PlayerInEleven[playerNumberFour].name}`;

            const strengthParagraph = document.createElement("p");
            strengthParagraph.className = "m-0";
            strengthParagraph.style.fontSize = "12px";
            strengthParagraph.textContent = `${playerIsHere ? " " : String(PlayerInEleven[playerNumberFour].strength)}`;

            playerNumberFour--;

            newDiv.appendChild(dotSpan);
            newDiv.appendChild(nameParagraph);
            newDiv.appendChild(strengthParagraph);

            defenceDiv.appendChild(newDiv);
        }
    }
    const playerIsHere = PlayerInEleven[0] == "Oyuncu Yok";
    goalKeeperDiv.innerHTML = `
    <div class="d-flex flex-column align-items-center cursor-pointer" data-id="0" data-position="goalkeeper" id="player-0">
        <span style="font-size: 24px;">.</span>
        <p class="m-0" style="font-size: 12px;">${playerIsHere ? " " : PlayerInEleven[0].name}</p>
    </div>`;
    addClickListenerToPlayers();

}

var offensivePositions = [
    "Forward", "Striker", "Left Winger", "Right Winger", "Center Forward"
];
var midfieldPositions = [
    "Central Midfielder", "Attacking Midfielder", "Midfielder", "Left Midfielder", "Right Midfielder"
];
var backMidfieldPositions = [
    "Central Midfielder", "Defensive Midfielder"
];
var defencePositions = [
    "Left Back", "Right Back", "Center Back"
]
var goalkeeperPositions = [
    "Goalkeeper", "Sweeper Keeper"
]


function isOk(positionArray, position) {
    return positionArray.includes(position);
}
function isOkForArray(postionArray, altPositions) {
    var altPostionsArray = altPositions.split(", ");
    return altPostionsArray.some(position => postionArray.includes(position))
}


function addClickListenerToPlayers() {
    // Tüm oyuncu div'lerini seç
    const playerDivs = document.querySelectorAll(".d-flex.align-items-center[data-id]");

    // Her bir div'e tıklama olayı ekle
    playerDivs.forEach(playerDiv => {
        playerDiv.addEventListener("click", function () {
            console.log("Tıklandı");
            console.log("Data ID:", this.getAttribute("data-id"));
            const dataId = this.getAttribute("data-id");
            console.log("Pozisyon:", this.getAttribute("data-position"));
            var positionPlayer = this.getAttribute("data-position");
            console.log(footballers);
            // Dinamik içeriği oluşturmak için değişken
            let footballerHtml = `
            <button class="btn btn-secondary fs-6" data-id="${dataId}" id="removeButtonId">
                Oyuncuyu Kaldır
            </button>
            <div class='separator separator-dashed my-4'></div>`;

            var countColor = 0;
            // for döngüsü ile dinamik HTML oluşturma
            for (let i = 0; i < footballers.length; i++) {
                const footballer = footballers[i];
                if (positionPlayer == "offensive") {
                    console.log();
                    if (!(isOk(offensivePositions, footballer.position) || isOkForArray(offensivePositions, footballer.altPositions))) {
                        continue;
                    }
                }
                else if (positionPlayer == "midfield") {
                    console.log();
                    if (!(isOk(midfieldPositions, footballer.position) || isOkForArray(midfieldPositions, footballer.altPositions))) {
                        continue;
                    }
                }
                else if (positionPlayer == "backMidfield") {
                    console.log();
                    if (!(isOk(backMidfieldPositions, footballer.position) || isOkForArray(backMidfieldPositions, footballer.altPositions))) {
                        continue;
                    }
                }
                else if (positionPlayer == "defence") {
                    console.log();
                    if (!(isOk(defencePositions, footballer.position) || isOkForArray(defencePositions, footballer.altPositions))) {
                        continue;
                    }
                }
                else if (positionPlayer == "goalkeeper") {
                    console.log();
                    if (!(isOk(goalkeeperPositions, footballer.position) || isOkForArray(goalkeeperPositions, footballer.altPositions))) {
                        continue;
                    }
                }

                var color = ["bg-danger text-inverse-danger", "bg-success text-inverse-success", "bg-info text-inverse-info", "bg-dark text-inverse-dark"];

                var footballerString = JSON.stringify(footballer);


                footballerHtml += `
                    <div class="card-body pt-6">
                        <div class="d-flex flex-stack">
                            <div class="symbol symbol-40px me-4 cursor-pointer" onClick="footballerDetail(${footballer.id})">
                                <div class="symbol-label fs-2 fw-semibold ${color[countColor]}">
                                    ${footballer.position[0]}
                                </div>
                            </div>
                            <div class="d-flex align-items-center flex-row-fluid flex-wrap">
                                <div class="flex-grow-1 me-2">
                                    <a href="#" class="text-gray-800 text-hover-primary fs-6 fw-bold">
                                        ${footballer.name} ${footballer.surName}
                                    </a>
                                    
                                    <span class="text-muted fw-semibold d-block fs-7">
                                        ${footballer.position} - ${footballer.altPositions}
                                    </span>
                                </div>
                                <button onClick="ChangeFootballer(${this.getAttribute("data-id")}, ${footballer.id})" class="btn btn-sm btn-icon btn-bg-light btn-active-color-primary w-30px h-30px">
                                    <i class="ki-duotone ki-arrow-right fs-2">
                                        <span class="path1"></span>
                                        <span class="path2"></span>
                                    </i>
                                </button>
                            </div>
                        </div>
                        <div class="separator separator-dashed my-4"></div>
                    </div>
                `;
                countColor++;
                if (countColor == 4) countColor = 0;

            }
            previousSwalHtml = footballerHtml;

            Swal.fire({
                html: footballerHtml,
                showConfirmButton: false,
                width: 900,
            });


        });
    });
}

document.addEventListener('click', function (e) {
    if (e.target && e.target.id === 'removeButtonId') {
        const removeDataId = e.target.getAttribute('data-id'); // Butonun data-id'sini al
        eleven.footballers[removeDataId] = 0;
        WriteLineUp();
        Swal.close();
    }
});

function ChangeFootballer(way, playerId) {
    var firstPlayer
    if (eleven.footballers[way] == 0) {
        firstPlayer = {}
    }
    else {
        firstPlayer = footballers.find(footballer => footballer.id === eleven.footballers[way]);
    }
    var secondPlayer = footballers.find(footballer => footballer.id === playerId);

    Swal.fire({
        html: `
            <div id="changePlayerChart" style="width: 100%; height: 500px;"></div>
            <button onClick="Change(${way}, ${playerId})" class="btn btn-primary text-inserve-primary">Değiştir</button>
        `,
        width: 500,
        showConfirmButton: false,
    });

    am5.ready(function () {
        // 1. Root oluştur
        var root = am5.Root.new("changePlayerChart");

        // 2. Tema ekle
        root.setThemes([am5themes_Animated.new(root)]);

        // 3. Radar chart oluştur
        var chart = root.container.children.push(am5radar.RadarChart.new(root, {
            panX: false,
            panY: false,
            wheelX: "none",
            wheelY: "none",
            innerRadius: am5.percent(20)
        }));

        // 4. Ekseni tanımla
        var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
            categoryField: "label",
            renderer: am5radar.AxisRendererCircular.new(root, {}),
            tooltip: am5.Tooltip.new(root, {})
        }));

        var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
            min: 0,
            max: 100,
            renderer: am5radar.AxisRendererRadial.new(root, {})
        }));

        var data = [
            { label: "Hız", value: firstPlayer.pace + firstPlayer.paceUpgrade + firstPlayer.confidence },
            { label: "Fiziksel Güç", value: firstPlayer.physicality + firstPlayer.physicalityUpgrade + firstPlayer.confidence },
            { label: "Şut", value: firstPlayer.shooting + firstPlayer.shootingUpgrade + firstPlayer.confidence },
            { label: "Pas", value: firstPlayer.passing + firstPlayer.passingUpgrade + firstPlayer.confidence },
            { label: "Dribbling", value: firstPlayer.dribbling + firstPlayer.dribblingUpgrade + firstPlayer.confidence },
            { label: "Savunma", value: firstPlayer.defending + firstPlayer.defendingUpgrade + firstPlayer.confidence }
        ];
        var data2 = [
            { label: "Hız", value: secondPlayer.pace + secondPlayer.paceUpgrade + secondPlayer.confidence },
            { label: "Fiziksel Güç", value: secondPlayer.physicality + secondPlayer.physicalityUpgrade + secondPlayer.confidence },
            { label: "Şut", value: secondPlayer.shooting + secondPlayer.shootingUpgrade + secondPlayer.confidence },
            { label: "Pas", value: secondPlayer.passing + secondPlayer.passingUpgrade + secondPlayer.confidence },
            { label: "Dribbling", value: secondPlayer.dribbling + secondPlayer.dribblingUpgrade + secondPlayer.confidence },
            { label: "Savunma", value: secondPlayer.defending + secondPlayer.defendingUpgrade + secondPlayer.confidence }
        ];

        var theme = document.documentElement.getAttribute("data-bs-theme");

        var seriesColor;
        var textColor;
        var axisLineColor;
        if (theme === "dark") {
            seriesColor = am5.color(0xFFDD00); // Koyu tema için parlak sarı
            textColor = am5.color(0xFFFFFF); // Koyu tema için açık metin rengi
            axisLineColor = am5.color(0xBBBBBB); // Koyu tema için açık çizgi rengi
        } else {
            seriesColor = am5.color(0x36A2EB); // Açık tema için mavi
            textColor = am5.color(0x000000); // Açık tema için siyah metin rengi
            axisLineColor = am5.color(0x333333); // Açık tema için koyu çizgi rengi
        }

        // 5. Birinci seriyi oluştur
        var series1 = chart.series.push(am5radar.RadarLineSeries.new(root, {
            name: firstPlayer.name + " " + firstPlayer.surName,
            xAxis: xAxis,
            yAxis: yAxis,
            valueYField: "value",
            categoryXField: "label",
            fill: seriesColor,
            stroke: seriesColor,
            tooltip: am5.Tooltip.new(root, {}),
            clustered: true
        }));

        // 6. İkinci seriyi oluştur
        var series2 = chart.series.push(am5radar.RadarLineSeries.new(root, {
            name: secondPlayer.name + " " + secondPlayer.surName,
            xAxis: xAxis,
            yAxis: yAxis,
            valueYField: "value",
            categoryXField: "label",
            fill: am5.color(0xFF5733), // İkinci seri için farklı bir renk
            stroke: am5.color(0xFF5733), // İkinci seri için farklı renk
            tooltip: am5.Tooltip.new(root, {}),
            clustered: true
        }));

        // Tooltip metin rengini ayarla
        series1.get("tooltip").label.set("text", "[bold]{name}[/]\n{valueX.formatDate()}: {valueY}");
        series1.get("tooltip").label.set("fill", textColor);

        series2.get("tooltip").label.set("text", "[bold]{name}[/]\n{valueX.formatDate()}: {valueY}");
        series2.get("tooltip").label.set("fill", textColor);

        // Eksen metin rengini ayarla
        xAxis.get("renderer").labels.template.set("fill", textColor);
        yAxis.get("renderer").labels.template.set("fill", textColor);

        // Eksen çizgi rengini ayarla
        xAxis.get("renderer").grid.template.setAll({
            stroke: axisLineColor
        });
        yAxis.get("renderer").grid.template.setAll({
            stroke: axisLineColor
        });

        // Add cursor
        chart.set("cursor", am5radar.RadarCursor.new(root, {
            behavior: "zoomX",
            xAxis: xAxis,
        }));

        series1.strokes.template.setAll({
            strokeWidth: 3
        });

        series2.strokes.template.setAll({
            strokeWidth: 3
        });

        // 7. Veriyi grafiğe ekle
        series1.data.setAll(data);
        series2.data.setAll(data2);
        xAxis.data.setAll(data);

        series1.bullets.push(function () {
            return am5.Bullet.new(root, {
                sprite: am5.Circle.new(root, {
                    radius: 5,
                    fill: seriesColor,
                    stroke: am5.color(0x333333),
                    strokeWidth: 2
                })
            });
        });

        series2.bullets.push(function () {
            return am5.Bullet.new(root, {
                sprite: am5.Circle.new(root, {
                    radius: 5,
                    fill: am5.color(0xFF5733), // İkinci seri için farklı renk
                    stroke: am5.color(0x333333),
                    strokeWidth: 2
                })
            });
        });

        // 8. Animasyon ekle
        chart.appear(1000, 100);
    });

}

function Change(way, playerId) {
    eleven.footballers[way] = playerId;
    WriteLineUp();
    Swal.close();
}

function footballerDetail(id) {
    
    var jsonData = footballers.find(footballer => footballer.id === id);

    Swal.fire({
        html: `
        <div id="playerStatsChart" style="width: 100%; height: 500px;"></div>
        `,
        width: 500,
        showConfirmButton: false
    }).then(() => {
        // Detay popup kapatıldığında liste popup'ını yeniden aç
        Swal.fire({
            html: previousSwalHtml, // Kaydedilmiş liste popup içeriği
            showConfirmButton: false,
            width: 900,
        });
    });

    am5.ready(function () {
        // 1. Root oluştur
        var root = am5.Root.new("playerStatsChart");

        // 2. Tema ekle
        root.setThemes([am5themes_Animated.new(root)]);

        // 3. Radar chart oluştur
        var chart = root.container.children.push(am5radar.RadarChart.new(root, {
            panX: false,
            panY: false,
            wheelX: "none",
            wheelY: "none",
            innerRadius: am5.percent(20)
        }));

        // 4. Ekseni tanımla
        var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
            categoryField: "label",
            renderer: am5radar.AxisRendererCircular.new(root, {}),
            tooltip: am5.Tooltip.new(root, {})
        }));

        var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
            min: 0,
            max: 100,
            renderer: am5radar.AxisRendererRadial.new(root, {})
        }));

        var data = [
            { label: "Hız", value: jsonData.pace },
            { label: "Fiziksel Güç", value: jsonData.physicality },
            { label: "Şut", value: jsonData.shooting },
            { label: "Pas", value: jsonData.passing },
            { label: "Dribbling", value: jsonData.dribbling },
            { label: "Savunma", value: jsonData.defending }
        ];

        var data2 = [
            { label: "Hız", value: jsonData.pace + jsonData.paceUpgrade + jsonData.confidence },
            { label: "Fiziksel Güç", value: jsonData.physicality + jsonData.physicalityUpgrade + jsonData.confidence },
            { label: "Şut", value: jsonData.shooting + jsonData.shootingUpgrade + jsonData.confidence },
            { label: "Pas", value: jsonData.passing + jsonData.passingUpgrade + jsonData.confidence },
            { label: "Dribbling", value: jsonData.dribbling + jsonData.dribblingUpgrade + jsonData.confidence },
            { label: "Savunma", value: jsonData.defending + jsonData.defendingUpgrade + jsonData.confidence }
        ];

        var theme = document.documentElement.getAttribute("data-bs-theme");

        var seriesColor;
        var textColor;
        var axisLineColor;
        if (theme === "dark") {
            seriesColor = am5.color(0xFFDD00); // Koyu tema için parlak sarı
            textColor = am5.color(0xFFFFFF); // Koyu tema için açık metin rengi
            axisLineColor = am5.color(0xBBBBBB); // Koyu tema için açık çizgi rengi
        } else {
            seriesColor = am5.color(0x36A2EB); // Açık tema için mavi
            textColor = am5.color(0x000000); // Açık tema için siyah metin rengi
            axisLineColor = am5.color(0x333333); // Açık tema için koyu çizgi rengi
        }

        // 5. Birinci seriyi oluştur
        var series1 = chart.series.push(am5radar.RadarLineSeries.new(root, {
            name: "Normal",
            xAxis: xAxis,
            yAxis: yAxis,
            valueYField: "value",
            categoryXField: "label",
            fill: seriesColor,
            stroke: seriesColor,
            tooltip: am5.Tooltip.new(root, {}),
            clustered: true
        }));

        // 6. İkinci seriyi oluştur
        var series2 = chart.series.push(am5radar.RadarLineSeries.new(root, {
            name: "Performans",
            xAxis: xAxis,
            yAxis: yAxis,
            valueYField: "value",
            categoryXField: "label",
            fill: am5.color(0xFF5733), // İkinci seri için farklı bir renk
            stroke: am5.color(0xFF5733), // İkinci seri için farklı renk
            tooltip: am5.Tooltip.new(root, {}),
            clustered: true
        }));

        // Tooltip metin rengini ayarla
        series1.get("tooltip").label.set("text", "[bold]{name}[/]\n{valueX.formatDate()}: {valueY}");
        series1.get("tooltip").label.set("fill", textColor);

        series2.get("tooltip").label.set("text", "[bold]{name}[/]\n{valueX.formatDate()}: {valueY}");
        series2.get("tooltip").label.set("fill", textColor);

        // Eksen metin rengini ayarla
        xAxis.get("renderer").labels.template.set("fill", textColor);
        yAxis.get("renderer").labels.template.set("fill", textColor);

        // Eksen çizgi rengini ayarla
        xAxis.get("renderer").grid.template.setAll({
            stroke: axisLineColor
        });
        yAxis.get("renderer").grid.template.setAll({
            stroke: axisLineColor
        });

        // Add cursor
        chart.set("cursor", am5radar.RadarCursor.new(root, {
            behavior: "zoomX",
            xAxis: xAxis,
        }));

        series1.strokes.template.setAll({
            strokeWidth: 3
        });

        series2.strokes.template.setAll({
            strokeWidth: 3
        });

        // 7. Veriyi grafiğe ekle
        series1.data.setAll(data);
        series2.data.setAll(data2);
        xAxis.data.setAll(data);

        series1.bullets.push(function () {
            return am5.Bullet.new(root, {
                sprite: am5.Circle.new(root, {
                    radius: 5,
                    fill: seriesColor,
                    stroke: am5.color(0x333333),
                    strokeWidth: 2
                })
            });
        });

        series2.bullets.push(function () {
            return am5.Bullet.new(root, {
                sprite: am5.Circle.new(root, {
                    radius: 5,
                    fill: am5.color(0xFF5733), // İkinci seri için farklı renk
                    stroke: am5.color(0x333333),
                    strokeWidth: 2
                })
            });
        });

        // 8. Animasyon ekle
        chart.appear(1000, 100);
    });



}



function ClubPlayerList(footballers) {
    const tbody = document.querySelector(".table tbody");
    tbody.innerHTML = "";
    footballers.forEach(item => {
        const trTable = document.createElement("tr");
        trTable.setAttribute("data-id", item.id);

        trTable.innerHTML = `
            <td>${translatePosition(item.position)}</td>
            <td>${item.name}</td>
            <td>${item.surName}</td>
        `;

        tbody.appendChild(trTable);
    });
}


document.getElementById('saveChangesButton').addEventListener('click', function () {
    eleven.formation = selectedValue;
    club.lineUp = JSON.stringify(eleven);
    console.log(club);

    $.ajax({
        url: '/Manager/Club/SaveFormation',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(club),  // JSON stringine çevir
        success: function (data) {
            if (data.success) {
                Swal.fire({
                    icon: 'success',
                    showConfirmButton: false,
                    text: data.message,
                    timer: 1500,
                });
            }
            else {
                Swal.fire({
                    text: data.message,
                    icon: 'error',
                    showConfirmButton: false,
                    timer: 1500,
                });
            }
        },
        error: function () {
            Swal.fire({
                text: "Sistem Hatası",
                icon: 'error',
                showConfirmButton: false,
                timer: 1500,
            });
        }
    });

});

