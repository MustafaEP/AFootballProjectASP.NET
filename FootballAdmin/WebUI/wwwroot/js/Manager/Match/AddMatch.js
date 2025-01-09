$(document).ready(function () {
    $("#add_match_button").click(function () {
        const awayTeamId = $("#away_team_id").val();
        const matchText = $("#match_message").val() // CKEditor içeriğini al
        const countryName = $("#select_country_id").val();

        var isOk = true;
        
        if (awayTeamId == "") {
            $("#away_team_danger").text("Rakip Seç Lan! Birden Gerildim Pardon 😊");
            isOk = false;
        }
        if (matchText == "") {
            $("#match_message_danger").text("Rakibe Bir Mesaj Mı Göndersek 😊");
            isOk = false;
        }
        if (countryName == "") {
            $("#country_danger").text("Ülke Seçki Konumumuzu Bilelim 😊");
            isOk = false;
        }

        if (isOk) {
            var datas = { AwayClubId: awayTeamId, ProposeLetter: matchText, Country: countryName }
            $.ajax({
                url: '/Manager/FootballMatches/AddMatch',
                method: 'POST',
                data: datas,
                success: (data) => {
                    if (data.success) {
                        Swal.fire({
                            text: data.message,
                            icon: 'success',
                            timer: 2000,
                            showConfirmButton: false
                        })
                    }
                }
            });
        }

    });
});