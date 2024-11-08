
var matches;

function MainPage() {
    $(document).ready(function () {
        $.ajax({
            url: '/Admin/Match/GetMatches',
            method: 'GET',
            success: function (data) {

                matches = data;

                //Butonlar
                var buttonsContainer = $('#Buttons');
                buttonsContainer.empty();
                var buttonDiv = `<button class="btn btn-primary" id="newMatchButton">Yeni Maç</button>`;
                buttonsContainer.append(buttonDiv);

                $('#newMatchButton').on('click', function () {
                    // SweetAlert'i göster
                    Swal.fire({
                        title: 'Yeni Maç Oluştur',
                        background: '#040f42',
                        text: 'Yeni bir maç oluşturmak istiyor musunuz?',
                        icon: 'question',
                        showCancelButton: true,
                        confirmButtonText: 'Evet',
                        cancelButtonText: 'Hayır',
                        customClass: {
                            confirmButton: 'pop-up-button btn-success',
                            cancelButton: 'pop-up-button btn-danger',
                        }
                        
                    }).then((result) => {
                        if (result.isConfirmed) {
                            $.ajax({
                                url: '/Admin/Team/GetTeams',
                                method: 'GET',
                                success: function (data) {
                                    Swal.fire({
                                        title: 'Yeni Maç Oluştur',
                                        background: '#040f42',
                                        html: `
                                    <div class = "form-group">
                                        <label for="homeTeam">Ev Sahibi Takım:</label>
                                        <select id="homeTeam" class="form-control bg-primary text-dark">
                                            <option   value="">Ev Sahibi</option>
                                            ${data.map(team => `<option value="${team.id}">${team.teamName}</option>`).join('')}
                                        </select>
                                    </div>
                                    <div class = "form-group">
                                        <label for="awayTeam">Deplasman Takımı:</label>
                                        <select id="awayTeam" class="form-control bg-primary text-dark">
                                            <option value="">Deplasman</option>
                                            ${data.map(team => `<option value="${team.id}">${team.teamName}</option>`).join('')}
                                        </select>
                                    </div>
                                    <div class = "form-group">
                                        <label for="matchDate">Tarih:</label>
                                        <input type="date" id="matchDate" class="form-control  bg-primary text-dark" required>
                                    </div>
                                `,
                                        showCancelButton: true,
                                        confirmButtonText: 'Kaydet',
                                        cancelButtonText: 'Çık',
                                        customClass: {
                                            confirmButton: 'pop-up-button btn-success',
                                            cancelButton: 'pop-up-button btn-danger',
                                        },
                                        preConfirm: () => {
                                            const HomeTeamId = document.getElementById('homeTeam').value;
                                            const AwayTeamId = document.getElementById('awayTeam').value;
                                            const MatchDate = document.getElementById('matchDate').value;
                                            if (!HomeTeamId || !AwayTeamId || !MatchDate) {
                                                Swal.showValidationMessage('Lütfen tüm alanları doldurun!');
                                                return false;
                                            }
                                            const newMatch = { HomeTeamId, AwayTeamId, MatchDate };
                                            return newMatch; // Form verilerini döndür
                                        }
                                    }).then((result) => {
                                        if (result.isConfirmed) {
                                            const newMatch = result.value;
                                            $.ajax({
                                                url: '/Admin/Match/AddMatch',
                                                method: 'POST',
                                                data: JSON.stringify(newMatch),
                                                contentType: "application/json; charset=utf-8",
                                                success: function (data) {
                                                    if (data.success) {
                                                        Swal.fire({
                                                            icon: 'success',
                                                            background: '#040f42',
                                                            text: data.message,
                                                            showConfirmButton: false,
                                                            timer: 1000
                                                        });
                                                        MainPage();
                                                    }
                                                    else {
                                                        Swal.fire({
                                                            icon: 'error',
                                                            background: '#040f42',
                                                            text: data.message,
                                                            showConfirmButton: false,
                                                            timer: 2000
                                                        });
                                                    }
                                            },
                                                error: function (data) {
                                                    Swal.fire({
                                                        icon: 'error',
                                                        background: '#040f42',
                                                        text: "Sistem Hatası!!!",
                                                        showConfirmButton: false,
                                                        timer: 2000
                                                    });
                                                }
                                            });
                                }
                            });

                        }
                    });
                }
                    });
    });

    //Cartlar
    var cardsContainer = $('#Cards');
    cardsContainer.empty();
    data.forEach(function (match) {
        var card = `<div class="col-lg-4 col-md-6 col-sm-12 mb-4 pe-auto">
            <div class="card bg-dark border-0 cursor-pointer" data-id=${match.id}>
                <div class="card-body">
                    <h5 class="fs-6">${match.homeTeam.teamName} vs ${match.awayTeam.teamName}</h5>
                    <p class='card-text'>Maç Skoru: ${match.result == null ? 'oynanmadı' : match.result} </p>
                    <p class="card-text">Tarih: ${formatDate(match.matchDate)}</p>
                </div>
            </div>
        </div>`;
        cardsContainer.append(card);
    });
},
error: function () {

    var cardsContainer = $('#Cards');
    cardsContainer.empty();
    var errorMessage = "<p class='text-danger'>Maçlar Getirilemedi. Şuan bir sorun yaşıyoruz. Lütfen daha sonra tekrar deneyiniz.</p>";

    cardsContainer.append(errorMessage);
}
        });
    });
}

MainPage();

$(document).on('click', '.card', function () {
    var matchId = $(this).data('id');
    var match = matches.find(m => m.id === matchId);

    Swal.fire({
        width: 1000,
        background: '#111111',
        html: `<div class="container">
    <div class="row d-flex">
        <div class="col-md-6 col-sm-12 bg-dark">
            <div class="header m-2">
                <div class="header"> ${match.homeTeam.teamName} </div>
            </div>
            <div class="body m-2">
                <div class="row-cols-1">
                    Menajer: ${match.homeTeam.manager.name} ${match.homeTeam.manager.surName}
                </div>
                <div class="row-cols-2">
                    Oyun Planı: ${match.homeTeam.manager.preferredLineUp}
                </div>
            </div>
        </div>
        <div class="col-md-6 col-sm-12 bg-dark">
            <div class="header m-2">
                <div class=""> ${match.awayTeam.teamName} </div>
            </div>
            <div class="body m-2">
                <div class="row-cols-1">
                    Menajer: ${match.awayTeam.manager.name} ${match.awayTeam.manager.surName}
                </div>
                <div class="row-cols-2">
                    Oyun Planı: ${match.awayTeam.manager.preferredLineUp}
                </div>
            </div>
        </div>
        <div class="col-md-12 header mt-2">
            Maç Zamanı: ${formatDateWithHours(match.matchDate)}
        </div>
        <div class="col-md-12 header mt-2">
            Maç Skoru: ${match.result}
        </div>
    </div>
</div>
`,
        confirmButtonText: 'Çık',
        showCancelButton: true,
        cancelButtonText: 'Maçı Sil',
        customClass: {
            confirmButton: 'pop-up-button btn-success',
            cancelButton: 'pop-up-button btn-danger',
        }
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                icon: 'success',
                background: '#111111',
                title: 'Çıkış Gerçekleştirildi',
                timer: 1000,
                showConfirmButton: false
            });
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire({
                icon: 'info',
                background: '#111111',
                title: 'Silmek İstediğinize Emin Misiniz',
                showConfirmButton: true,
                confirmButtonText: 'Çık',
                showCancelButton: true,
                cancelButtonText: 'Maçı Sil',
                customClass: {
                    confirmButton: 'pop-up-button btn-success',
                    cancelButton: 'pop-up-button btn-danger',
                }
            }).then((deleteResult) => {
                if (deleteResult.isConfirmed) {
                    Swal.fire({
                        icon: 'success',
                        background: '#111111',
                        title: 'Çıkış Gerçekleştirildi',
                        timer: 1000,
                        showConfirmButton: false
                    });
                }
                else if (result.dismiss === Swal.DismissReason.cancel) {
                    $.ajax({
                        url: '/Admin/Match/DeleteMatch/' + match.id,
                        method: 'POST',
                        success: function () {
                            Swal.fire({
                                icon: 'success',
                                background: '#111111',
                                title: 'Silme İşlemi Gerçekleştirildi',
                                timer: 1500,
                                showConfirmButton: false
                            });
                            MainPage();
                        },
                        error: function () {
                            Swal.fire({
                                icon: 'error',
                                background: '#111111',
                                title: 'Silme İşlemi Gerçekleştirilemedi',
                                timer: 1500,
                                showConfirmButton: false
                            });
                        }
                    });
                }
            });
        }
    });
});

