function BuyFootboller(id) {
    $.ajax({
        url: '/Manager/Transfer/AddFootballerForClub/' + id,
        method: 'POST',
        success: (data) => {
            Message(data.success, data.message);
        },
        error: () => {
            ErrorMessage();
        }

    });
}