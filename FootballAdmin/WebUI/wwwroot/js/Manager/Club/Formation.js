document.addEventListener("DOMContentLoaded", () => {
    GetFootballers();
    ZoomJobs();
    
    WriteLineUp();

});
function GetFootballers() {
    $.ajax({
        url: '/Manager/Club/ClubInfoForFormation',
        method: 'GET',
        success: (data) => {
            if (data.success) {
                FootballerCards(data.value)
            }
            else {
                Message(false, data.message);
            }
        },
        error: () => {
            ErrorMessage();
        }
    });
}
function FootballerCards(players) {
    let playerCardHTML = "";

    console.log(players);

    players.$values.forEach((player) => {
        var positions = player.footballer.footballerPositions

        var positionsHTML = ``;

        let positionArray = [];

        positions.$values.forEach((position, i) => {
            positionsHTML += `${position.shortPosition} `;
            positionArray[i] = position.shortPosition;
        });
        const power = player.footballer.statistic.power;
        console.log(power);
        const url = power >= 90 ? '5' : power >= 85 ? '4' : power >= 80 ? '3' : power >= 75 ? '2' : '';
        playerCardHTML += `
            <div draggable="true" class="player" data-id="${player.id}" data-position1="${positionArray[0]}" data-position2="${positionArray[1]}" data-position3="${positionArray[2]}" data-position4="${positionArray[3]}" style="background-image: url('/Image/FUTCARD${url}.png')">
                <div class="flex-column fut_card">
                    <div class="fut_card_img">
                        <img id="footballer_img" alt="Futbolcu Resmi" class="card_image" src="/image/Footballers/footballer.png" />
                    </div>
                    <div class="flex-column text_card">
                        <p class="fut_card_name">${player.footballer.name} ${player.footballer.surName}</p>
                        <p class="fut_card_position">${positionsHTML}</p>
                    </div>
                    <div class="d-flex row text_powers">
                        <div class="col-6 powers powers_left">
                            <p class="power_1"><b>${player.footballer.statistic.pace}</b> PAC</p>
                            <p class="power_2"><b>${player.footballer.statistic.shooting}</b> SHO</p>
                            <p class="power_3"><b>${player.footballer.statistic.passing}</b> PAS</p>
                        </div>
                        <div class="col-6 powers powers_right">
                            <p class="power_4"><b>${player.footballer.statistic.pace}</b> DRI</p>
                            <p class="power_5"><b>${player.footballer.statistic.dribbling}</b> DEF</p>
                            <p class="power_6"><b>${player.footballer.statistic.physicality}</b> PHY</p>
                        </div>
                    </div>
                </div>
            </div>
`
    })

    // Player list div'ini seçin
    const playerList = document.getElementById('player-list');

    // Yeni HTML içeriğini listeye ekleyin
    playerList.insertAdjacentHTML('beforeend', playerCardHTML);

    
    DragDrop();
    GetFormation();
}
function DragDrop() {
    const players = document.querySelectorAll(".player");
    const positions = document.querySelectorAll(".position");
    const playerList = document.getElementById("player-list");
    const clearFieldButton = document.getElementById("clear-field");

    let draggedPlayer = null;

    players.forEach(player => {
        player.addEventListener("dragstart", () => {
            draggedPlayer = player;
            player.classList.add("dragging");
        });

        player.addEventListener("dragend", () => {
            draggedPlayer = null;
            player.classList.remove("dragging");
        });
    });

    positions.forEach(position => {
        position.addEventListener("dragover", (e) => {
            e.preventDefault();
        });

        position.addEventListener("drop", () => {
            if (draggedPlayer) {
                const existingPlayer = position.querySelector(".player");

                // Pozisyon kontrolü
                const positionAtt = position.getAttribute("data-position");
                const positionData = positionAtt.split(" ")[0];

                const playerData1 = draggedPlayer.getAttribute("data-position1");
                const playerData2 = draggedPlayer.getAttribute("data-position2");
                const playerData3 = draggedPlayer.getAttribute("data-position3");
                const playerData4 = draggedPlayer.getAttribute("data-position4");


                if (!(positionData === playerData1 || positionData === playerData2 || positionData === playerData3 || positionData === playerData4)) {
                    let warningIcon = position.querySelector(".warning-icon");
                    if (!warningIcon) {
                        // Kapsayıcı için relative ayarı
                        position.style.position = "relative";

                        warningIcon = document.createElement("div");
                        warningIcon.className = "warning-icon";
                        warningIcon.textContent = "!";
                        warningIcon.style.position = "absolute"; // Konumlandırma kapsayıcıya göre
                        warningIcon.style.left = "-15px"; // Kapsayıcının solundan -20px
                        warningIcon.style.top = "0px"; // Kapsayıcının üstünden -20px
                        warningIcon.style.backgroundColor = "red";
                        warningIcon.style.color = "white";
                        warningIcon.style.borderRadius = "50%";
                        warningIcon.style.width = "20px";
                        warningIcon.style.height = "20px";
                        warningIcon.style.display = "flex";
                        warningIcon.style.alignItems = "center";
                        warningIcon.style.justifyContent = "center";
                        warningIcon.style.cursor = "pointer";
                        warningIcon.style.zIndex = 2;

                        const warningMessage = document.createElement("div");
                        warningMessage.textContent = `Uyarı: Konum Oyuncu İçin Uygun Değil. Düşük Performansta Oyniyacak.`;
                        warningMessage.style.position = "absolute"; // Mesaj da kapsayıcıya göre konumlanacak
                        warningMessage.style.left = "25px";
                        warningMessage.style.top = "0";
                        warningMessage.style.backgroundColor = "white";
                        warningMessage.style.color = "black";
                        warningMessage.style.border = "1px solid black";
                        warningMessage.style.padding = "5px";
                        warningMessage.style.borderRadius = "5px";
                        warningMessage.style.display = "none";
                        warningMessage.style.zIndex = 3;

                        warningIcon.addEventListener("mouseenter", () => {
                            warningMessage.style.display = "block";
                        });

                        warningIcon.addEventListener("mouseleave", () => {
                            warningMessage.style.display = "none";
                        });

                        position.appendChild(warningIcon);
                        position.appendChild(warningMessage);
                    }

                }
                else {
                    const warningIcon = position.querySelector(".warning-icon");
                    const warningMessage = position.querySelector(".warning-icon + div"); // İlgili uyarı mesajını seç
                    if (warningIcon) warningIcon.remove();
                    if (warningMessage) warningMessage.remove();
                }


                if (existingPlayer) {
                    // Eğer pozisyon doluysa, mevcut oyuncuyla yer değiştir.
                    const parentOfDraggedPlayer = draggedPlayer.parentNode;
                    parentOfDraggedPlayer.appendChild(existingPlayer);
                }

                // Sürüklenen oyuncuyu bırakılan pozisyona ekle
                position.appendChild(draggedPlayer);
            }
        });
    });

    clearFieldButton.addEventListener("click", () => {
        positions.forEach(position => {
            const player = position.querySelector(".player");
            if (player) {
                playerList.appendChild(player);
            }

            const warningIcon = position.querySelector(".warning-icon");
            const warningMessage = position.querySelector("div[style*='background-color: white']");
            if (warningIcon) position.removeChild(warningIcon);
            if (warningMessage) position.removeChild(warningMessage);
        });
    });

    document.getElementById('preferredLineUp').addEventListener("click", () => {
        positions.forEach(position => {
            const player = position.querySelector(".player");
            if (player) {
                playerList.appendChild(player);
            }

            const warningIcon = position.querySelector(".warning-icon");
            const warningMessage = position.querySelector("div[style*='background-color: white']");
            if (warningIcon) position.removeChild(warningIcon);
            if (warningMessage) position.removeChild(warningMessage);
        });
    });

    //Pozisyona Tıklama Olayı Gerçekleştirilecek...
    document.querySelectorAll('.position').forEach(div => {
        div.addEventListener('click', (e) => {
            const info = e.target.dataset.value;
            const player_list = document.getElementById('player_list_div').innerHTML;
            Swal.fire({
                showConfirmButton: false,
                html: player_list,
                width: 1000
            });


        });
    });
}
function ZoomJobs() {
    const playerList = document.getElementById('player-list');
    const formation = document.getElementById('formation');
    const overlay = document.createElement('div');
    overlay.className = 'overlay';
    document.body.appendChild(overlay);

    playerList.addEventListener('dblclick', (event) => {
        const target = event.target.closest('.player');
        if (!target) return;

        // Kartı büyüt veya küçült
        if (target.classList.contains('zoomed')) {
            target.classList.remove('zoomed');
            overlay.classList.remove('active');
        } else {
            document.querySelectorAll('.player.zoomed').forEach((el) => el.classList.remove('zoomed'));
            target.classList.add('zoomed');
            overlay.classList.add('active');
        }
    });



    // Overlay'e tıklama ile büyütmeyi kapat
    overlay.addEventListener('click', () => {
        document.querySelectorAll('.player.zoomed').forEach((el) => el.classList.remove('zoomed'));
        overlay.classList.remove('active');
    });
}
function WriteLineUp() {

    const preferredLineUp = document.getElementById('preferredLineUp');

    document.getElementById('preferredLineUp').addEventListener('change', () => {

        const formationMap = document.getElementById('formation');

        formationMap.innerHTML = "";

        const selectedValue = preferredLineUp.value;

        const LineUp = selectedValue.split("-").map(Number);

        let formationHTML = "";

        var lengthLineUp = LineUp.length;

        if (LineUp[4] == 3) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="frw">
                     <div class="col-3 d-flex justify-content-center">
                           <div class="position" data-position="SLK" style="position: relative; top: 5px;"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                           <div class="position" data-position="SNT"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                           <div class="position" data-position="SĞK" style="position: relative; top: 5px"></div>
                    </div>
                </div>
                `;
        } else if (LineUp[4] == 2) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="frw">
                     <div class="col-3 d-flex justify-content-center">
                        <div class="position" data-position="SNT 1"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                        <div class="position" data-position="SNT 2"></div>
                    </div>

                   
                </div>
                `;
        } else if (LineUp[4] == 1) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="frw">
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="SNT"></div>
                    </div>
                </div>
                `;
        }
        if (LineUp[3] == 1) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="oos">
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="OOS"></div>
                    </div>
                </div>
                `;
        } else if (LineUp[3] == 2) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="oos">
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="OOS 1"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="OOS 2"></div>
                    </div>
                </div>
                `;
        } else if (LineUp[3] == 3) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="oos">
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="SLO"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="OOS"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="SĞO"></div>
                    </div>
                </div>
                `;
        } else if (LineUp[3] == 4) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="oos">
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="SLO"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="OOS 1"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="OOS 2"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="SĞO"></div>
                    </div>
                </div>
                `;
        }
        if (LineUp[2] == 1) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="mo">
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="MO"></div>
                    </div>  
                </div>
                `;
        } else if (LineUp[2] == 2) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="mo">
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="MO 1"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="MO 2"></div>
                    </div>
                </div>
                `;
        } else if (LineUp[2] == 3) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="mo">
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="MO 1"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="MO 2"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="MO 3"></div>
                    </div>
                </div>
                `;
        } else if (LineUp[2] == 4) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="mo">
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="SKB" style="position: relative; top: 5px;"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="MO 1"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="MO 2"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="SĞB" style="position: relative; top: 5px;"></div>
                    </div>
                </div>
                `;
        } else if (LineUp[2] == 5) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="mo">
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="SLKB" style="position: relative; top: 20px;"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="MO 1"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="MO 2"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="MO 3"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="SĞKB" style="position: relative; top: 20px;"></div>
                    </div>
                </div>
                `;
        }

        if (LineUp[1] == 1) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="dos">  
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="DOS"></div>
                    </div>
                </div>
                `;
        } else if (LineUp[1] == 1) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="dos">  
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="DOS 1"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="DOS 2"></div>
                    </div>
                </div>
                `;
        }

        if (LineUp[0] == 3) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="dos">  
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="STP 1"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="STP 2"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="STP 3"></div>
                    </div>
                </div>
                `;
        } else if (LineUp[0] == 4) {
            formationHTML += `
                <div class="row d-flex justify-content-center" id="dos">  
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="SLB"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="STP 1"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="STP 2"></div>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="SĞB"></div>
                    </div>
                </div>
                `;
        } else if (LineUp[0] == 5) {
            formationHTML += `
               <div class="row d-flex justify-content-center" id="dos">
                    <div class="custom-col d-flex justify-content-center">
                       <div class="position" data-position="SLB" style="position: relative; top: -20px;"></div>
                    </div>
                    <div class="custom-col d-flex justify-content-center">
                       <div class="position" data-position="STP 1"></div>
                    </div>
                    <div class="custom-col d-flex justify-content-center">
                       <div class="position" data-position="STP 2"></div>
                    </div>
                    <div class="custom-col d-flex justify-content-center">
                       <div class="position" data-position="STP 3"></div>
                    </div>
                    <div class="custom-col d-flex justify-content-center">
                       <div class="position" data-position="SĞB" style="position: relative; top: -20px;"></div>
                    </div>
                </div>
                `;
        }

        formationHTML += `
                <div class="row d-flex justify-content-center" id="gk">  
                    <div class="col-3 d-flex justify-content-center">
                       <div class="position" data-position="GK"></div>
                    </div>
                </div>
                `;

        formationMap.insertAdjacentHTML('beforeend', formationHTML);

        document.querySelectorAll('.position').forEach((element, index) => {
            const position = element.dataset.position; // data-position değerini al
            const uniqueValue = `${position}-${index + 1}`; // Özgün bir değer oluştur
            element.setAttribute('data-value', uniqueValue); // data-value'yu ata
        });


        DragDrop();
    });

}
function PostXI() {
    const positionDivs = document.querySelectorAll("div.position");
    const positionDivArray = [...positionDivs];

    const preferredLineUp = document.getElementById('preferredLineUp');
    if (preferredLineUp.value == "") {
        Message(false, "Diziliş Seçmediniz");
        return;
    }



    var jsonData1 = [];
    var ids = [];
    var positions = [];
    for (var i = 0; i < 11; i++) {
        const dataPosition = positionDivArray[i].getAttribute('data-position');
        const player = positionDivArray[i].querySelector('div.player');
        const playerId = player != null ? player.getAttribute('data-id') : null;


        ids.push(playerId);
        positions.push(dataPosition);
    }

    const jsonData = {
        formationString: preferredLineUp.value,
        ids: ids,
        positions: positions
    };
   
   

    PostXIAjax(jsonData);

    




}
function PostXIAjax(jsonData) {
    $.ajax({
        url: '/Manager/Club/FormationPost',
        method: 'POST',
        data: JSON.stringify(jsonData), // JSON verisini string'e dönüştür
        contentType: "application/json; charset=utf-8",
        success: (data) => {
            Message(data.success, data.message);
        },
        error: (xhr, status, error) => {
            ErrorMessage();
        }
    });
}

function GetFormation() {
    $.ajax({
        url: '/Manager/Club/GetFormation',
        method: 'GET',
        success: (data) => {
            FormationUploading(data.value); 
        },
        error: () => {
            console.warn("Daha önce formasyonu kaydedilmemiş.");
        }
    });
}

function FormationUploading(formation) {

    const preferredLineUp = document.getElementById('preferredLineUp');
    preferredLineUp.value = formation.fotmationType;
    // change olayı tetiklemek için
    const event = new Event('change');
    preferredLineUp.dispatchEvent(event);


    const positions = document.querySelectorAll('.position');
    const players = document.querySelectorAll('.player');
    const playerList = document.getElementById("player-list");

    var xiplayers = formation.xiPlayers.$values; 

    for (var i = 0; i < 11; i++) {

        const targetPlayer = Array.from(players).find(
            x => parseInt(x.getAttribute('data-id'), 10) === xiplayers[i].addedFootballerId
        );

        const targetPosition = Array.from(positions).find(
            position => position.getAttribute('data-position') === xiplayers[i].position
        );



        if (targetPlayer && targetPosition) {
            const existing = targetPosition.querySelector(".player");
            if (existing) {
                playerList.appendChild(existing);
            }
            targetPosition.appendChild(targetPlayer);
        }
    }



}