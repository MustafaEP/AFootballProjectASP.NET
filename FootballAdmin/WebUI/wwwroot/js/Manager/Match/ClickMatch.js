document.getElementById("statusFilter").addEventListener("change", function () {
    const selectedStatus = this.value;
    const cards = document.querySelectorAll(".col-md-6.col-xl-4");

    cards.forEach(card => {
        const status = card.getAttribute("data-status");
        if (selectedStatus === "all" || status === selectedStatus) {
            card.style.display = "block";
        } else {
            card.style.display = "none";
        }
    });
});

$('.match-card').on('click', function () {
    const cardId = parseInt($(this).data('id'), 10); // data-id'yi al
    console.log(cardId);
    $.ajax({
        url: '/Manager/FootballMatches/GetMatch/' + cardId,
        method: 'POST',
        success: (data) => {

            if (data.success) {
                console.log(data);
                Swal.fire({
                    text:
                        data.isHomeClub == true && data.matchData.isAccepted == true ? 'Maç Oynanmıştır!'
                            : data.isHomeClub && data.matchData.isRejected ? 'Maç Tekfiniz Reddedilmiştir.'
                                : data.isHomeClub ? 'Teklifinize Herhangi bir cevap verilmemiştir.'
                                    : !data.isHomeClub && data.matchData.isAccepted ? 'Teklifi Kabul Ettiniz ve maç oynanmıştır.'
                                        : !data.isHomeClub && data.matchData.isRejected ? 'Teklifi Reddettiniz.'
                                            : data.matchData.proposeLetter,
                    showConfirmButton: !data.isHomeClub && data.matchData.isAccepted ? false
                        : data.matchData.isRejected == true ? false
                            : data.isHomeClub == true ? false : true,
                    showCancelButton: !data.isHomeClub && data.matchData.isAccepted ? false
                        : data.matchData.isRejected == true ? false
                            : data.isHomeClub == true ? false : true,
                    showDenyButton: true,
                    confirmButtonText: 'Kabul Et',
                    cancelButtonText: 'Reddet',
                    denyButtonText: !data.isHomeClub && (data.matchData.isAccepted || data.matchData.isRejected) ? 'Çık' : data.isHomeClub == false ? 'Başka Zaman' : 'Çık',
                    customClass: {
                        confirmButton: 'btn btn-success',
                        cancelButton: 'btn btn-danger',
                        denyButton: 'btn btn-primary'
                    }
                }).then((result) => {
                    if (result.isConfirmed) {
                        if (data.hClubStatus != true && data.aClubStatus != true) {
                            Message(false, "Her İki Takımınındad 11'i Tamamlanmamış");
                        } else if (data.hClubStatus != true) {
                            Message(false, "İç Saha Takımının 11'i Tamamlanmamış");
                        } else if (data.aClubStatus != true) {
                            Message(false, "Deplasman Takımının 11'i Tamamlanmamış");
                        } else {
                            console.log(data)
                            $.ajax({
                                url: '/Manager/FootballMatches/AcceptMatch/' + data.matchData.id,
                                method: 'POST',
                                success: (data) => {
                                    Message(data.success, data.message);
                                },
                                error: () => {
                                    ErrorMessage();
                                }
                            });
                        }
                    } else if (result.isDenied) {
                        ExitMessage();
                    } else if (result.dismiss === Swal.DismissReason.cancel) {
                        AreYouSure(() => {
                            $.ajax({
                                url: '/Manager/FootballMatches/RejectedMatch/' + data.matchData.id,
                                method: 'POST',
                                success: (data) => {
                                    Message(data.success, data.message);
                                },
                                error: () => {
                                    ErrorMessage();
                                }
                            });
                        })
                    }
                })
            }
            else {
                Message(data.success, data.message);
            }
        }
    });

});

function HtmlPlayers(footballer, bgColor, icon, upgrade) {
    return `<div class="card-toolbar d-flex justify-content-between align-items-center mb-2">
                <div class="text-gray-400 fw-semibold fs-5">${footballer.name} ${footballer.surName}</div>
                <span class="badge ${bgColor} fw-bold px-4 py-3">${upgrade}${icon}</span>
           </div>`;
}