GetTables();
ManagerClub();
function GetTables() {
    $.ajax({
        url: '/Manager/Transfer/GetFootballers',
        method: 'GET',
        success: function (data) {
            const tbody = document.querySelector(".table tbody");
            tbody.innerHTML = "";
            data.forEach(item => {
                const trTable = document.createElement("tr");
                trTable.setAttribute("data-id", item.id);

                // Verileri tablo satırına ekleyelim
                trTable.innerHTML = `
                  <td>${item.name}</td>
                  <td>${item.surName}</td>
                  <td>${translateCountry(item.country)}</td>
                  <td>${translatePosition(item.position)}</td>
                  <td>₺${item.marketPrice}</td>
                `;



                trTable.addEventListener("click", () => {
                    const id = trTable.getAttribute("data-id");
                    $.ajax({
                        url: '/Manager/Transfer/GetFootballer/' + id,
                        method: 'GET',
                        success: function (data) {
                            Swal.fire({
                                showConfirmButton: false,
                                width: 1000,
                                height: 1000,
                                html: `
                        <div class="m-3">
                            <p>${data.name} ${data.surName}</p>
                            <div class="col-md-12 row">
                                <div class="col-md-6">
                                    <canvas id="playerStatsChart" width="400" height="400"></canvas>
                                </div>
                                <div class="col-md-6">
                                    <p>Özellikler</p>
                                    <table class="table table-hover">
                                        <tbody id="gifts">

                                        </tbody>
                                        <tfooot>
                                            <tr>
                                                <td>
                                                    ${translateStrongFoot(data.preferredFoot)}
                                                </td>
                                            </tr>
                                        </tfoot>
                                    </table>

                                    <div class="mt-3">
                                        <button class="btn btn-sm btn-secondary me-2" onclick="AddPlayerClub(${data.id})">Oyuncu Ekle</button>
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                `
                            });


                            const playerStats = {
                                labels: ["Hız", "Fiziksel Güç", "Şut", "Pas", "Dribbling", "Savunma"],
                                datasets: [
                                    {
                                        label: "Oyuncu Güçleri",

                                        data: [data.pace, data.physicality, data.shooting, data.passing, data.dribbling, data.defending], // Örnek veri, oyuncunun her bir gücü için 100 üzerinden değerler
                                        fill: true,
                                        backgroundColor: 'rgba(54, 162, 235, 0.2)',
                                        borderColor: 'rgb(54, 162, 235)',
                                        pointBackgroundColor: 'rgb(54, 162, 235)',
                                        pointBorderColor: '#fff',
                                        pointHoverBackgroundColor: '#fff',
                                        pointHoverBorderColor: 'rgb(54, 162, 235)'
                                    }
                                ]
                            };

                            const ctx = document.getElementById("playerStatsChart").getContext("2d");

                            new Chart(ctx, {
                                type: "radar",
                                data: playerStats,
                                maintainAspectRatio: false, // Template varsayımlarını engellemek için kullanılabilir
                                options: {
                                    scale: {
                                        ticks: {
                                            beginAtZero: true,
                                            max: 100,
                                            min: 0,
                                            stepSize: 20,
                                        },

                                    }
                                }
                            });

                            const dataGifts = data.abilities;

                            const giftsAtributes = dataGifts.split(", ");

                            const dataAltPositions = data.altPositions;

                            const positionAtributes = dataAltPositions.split(", ");

                            const mergedAttributes = giftsAtributes.concat(positionAtributes);

                            const tbodyGifts = document.getElementById("gifts");



                            mergedAttributes.forEach(attribute => {
                                const trGift = document.createElement("tr");
                                const tdGift = document.createElement("td");
                                tdGift.textContent = translateAttributes(translatePosition(attribute)); // td'ye attribute ismini ekle
                                trGift.appendChild(tdGift);
                                tbodyGifts.appendChild(trGift);
                            });

                        }
                    });
                });





                tbody.appendChild(trTable);
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

function ManagerClub() {
    $.ajax({
        url: '/Manager/Club/ClubInfo',
        method: 'GET',
        success: function (data) {
            console.log(data);
            if (data.success) {
                document.getElementById("clubName").innerText = data.values.name;
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