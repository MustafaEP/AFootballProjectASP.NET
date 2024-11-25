var footballers;
var eleven;
var club;

$.ajax({
    url: '/Manager/Club/ClubInfo',
    method: 'GET',
    success: function (data) {
        if (data.success) {
            club = data.values;
            footballers = data.values.footballers;
            eleven = JSON.parse(data.values.lineUp);
            WriteLineUp();
        }
        else {
            $(document).ready(function () {
                // Mevcut 5 div'i kaldır
                $('#offensiveId, #midfieldId, #backMidfieldId, #defenceId, #goalKeeperId').remove();

                // Yeni bir div oluştur ve id olarak offensiveId belirle
                var newDiv = $('<div class="text-center">Daha Takım Oluşturmamışsın</div>');

                // Yeni div'i #elevenId içerisine ekle
                $('#elevenMapId').append(newDiv);
            });
        }
    }
});

function WriteLineUp() {
    selectedValue = eleven.formation;

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
            newDiv.className = "d-flex flex-column align-items-center ";
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
            newDiv.className = "d-flex flex-column align-items-center ";
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
            newDiv.className = "d-flex flex-column align-items-center ";
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
            newDiv.className = "d-flex flex-column align-items-center ";
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
            newDiv.className = "d-flex flex-column align-items-center ";
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
            newDiv.className = "d-flex flex-column align-items-center ";
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
            newDiv.className = "d-flex flex-column align-items-center ";
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
    <div class="d-flex flex-column align-items-center " data-id="0" data-position="goalkeeper" id="player-0">
        <span style="font-size: 24px;">.</span>
        <p class="m-0" style="font-size: 12px;">${playerIsHere ? " " : PlayerInEleven[0].name}</p>
    </div>`;
    

}
