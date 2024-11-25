function DontCreateClub() {
    Swal.fire({
        title: '😎',
        html: `<div>
                            <div class="card-body">
                            <p class="text-white">Daha Takım Oluşturmamışsın. Hemen Bir Takım Oluşturalım</p>
                                <form class="forms-sample" id="addClub">
                                    <div class="form-group">
                                        <label for="clubNameId" style="color: #fff;">Takım Adı:</label>
                                        <input type="text" id="clubNameId" class="text-white form-control" placeholder="Takım Adı" maxlength="20" required>
                                    </div>
                                </form>
                            </div>
                        </div>`,
        showConfirmButton: true,
        background: '#2f2f2f',
        confirmButtonText: "Takım Oluştur",
        showCancelButton: true,
        cancelButtonText: "Ana Sayfa",
        allowOutsideClick: false,
        customClass: {
            confirmButton: 'btn-secondary',
            cancelButton: 'btn-secondary'
        },
        preConfirm: () => {
            const addData = {
                Name: document.getElementById('clubNameId').value,
            };

            if (Object.values(addData).some(field => !field)) {
                Swal.showValidationMessage("Lütfen tüm alanları doldurun.");
                return false;
            }
            if (addData.Name.length < 2) {
                Swal.showValidationMessage("Takım İsmi en az 2 karakter olmalıdır.");
                return false;
            }
            if (addData.Name.length > 20) {
                Swal.showValidationMessage("Takım İsmi en fazla 20 karakter olmalıdır.");
                return false;
            }
            return addData;
        }

    }).then((result) => {
        if (result.isConfirmed) {
            var addData = result.value;
            $.ajax({
                url: '/Manager/Club/AddClub',
                method: 'POST',
                data: JSON.stringify(addData),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.success) {
                        Swal.fire({
                            text: data.message,
                            timer: 1500,
                            icon: 'success',
                            showConfirmButton: false,
                            allowOutsideClick: false,
                            background: '#2f2f2f',
                        });
                        setTimeout(() => {
                            window.location.reload();
                        }, 1500);
                    }

                    else {
                        Swal.fire({
                            text: data.message,
                            timer: 1500,
                            icon: 'error',
                            showConfirmButton: false,
                            allowOutsideClick: false,
                            background: '#2f2f2f',
                        });
                        setTimeout(() => {
                            window.location.reload();
                        }, 1500);
                    }
                }
            })
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire({
                icon: 'success',
                text: 'Ana Sayfaya Dönülüyor',
                timer: 1500,
                background: '#2f2f2f',
                showConfirmButton: false,
            });
            setTimeout(() => {
                window.location.assign('/Manager/Home/Index');
            }, 1500);
        }
    })
}