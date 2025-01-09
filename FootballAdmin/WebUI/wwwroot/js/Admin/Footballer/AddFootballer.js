
document.getElementById("url_button_id").addEventListener("click", function () {
    var url = document.getElementById("url_id").value;
    Swal.fire({
        position: "top-end",
        title: 'İşlem yapılıyor...',
        text: 'Lütfen bekleyin, Bu işlem biraz zaman alabilir...',
        allowOutsideClick: false, 
        didOpen: () => {
            Swal.showLoading(); 
        }
    });



    $.ajax({
        url: `https://api.allorigins.win/get?url=${encodeURIComponent(url)}`,
        method: "GET",
        success: function (response) {
            // HTML içeriğini bir DOM elementine çevir
            var parser = new DOMParser();
            var doc = parser.parseFromString(response.contents, "text/html");

            // Stat_value__TT86G sınıfına sahip span elemanlarını seç
            var spanElements = doc.querySelectorAll(".Stat_value__TT86G");

            var playerName = doc.querySelectorAll(".AthletePage_playerName__WSrAu");
            document.getElementById("first_name").value = playerName[0].textContent;
            document.getElementById("last_name").value = playerName[1].textContent;
            document.getElementById("username").value = playerName[0].textContent + "_" + playerName[1].textContent;


            var abilities = doc.querySelectorAll("div.AbilityPanel_textWrapper__MGrFJ");


            document.getElementById('skill-count-select').value = abilities.length;
            generateSkillInputsFromSelect();

            abilities.forEach((ability, index) => {
                var html = ability.innerHTML;
                var parserHtml = new DOMParser();
                var docAbilityHtml = parserHtml.parseFromString(html, "text/html");
                "<h5 class=\"Typography_typography___mliz generated_headline5__U7Jsl Typography_margins__GGnT6\">Teknik+</h5><p class=\"generated_body2__P92dE AbilityPanel_descriptionWrapper__2T1D7\">Daha yüksek hızlı Kontrollü Sprint, topu sürerken keskin geniş açılı dönüşler</p>"

                var abilityTitle = docAbilityHtml.querySelector("h5").textContent;
                var abilityText = docAbilityHtml.querySelector("p").textContent;


                const titleInput = document.querySelector(`input[name="Statistic.footballerAbilities[${index}].AbilityName"]`);
                const descriptionInput = document.querySelector(`input[name="Statistic.footballerAbilities[${index}].Description"]`);

                if (titleInput) titleInput.value = abilityTitle;
                if (descriptionInput) descriptionInput.value = abilityText;

                //  ItemGrid_grid__cbrs8 ItemGrid_equalRows__fpam1
            })

            var positions = doc.querySelectorAll('.Tag_tagInner__vFnF6 > div');
            console.log(positions);
            document.getElementById('position-count-select').value = String(positions.length - 1);




            generatePositionInputsFromSelect();

            positions.forEach((position, index) => {
                const inputElement = document.querySelector(`input[name="FootballerPositions[${index}].ShortPosition"]`);
                if (inputElement) {
                    inputElement.value = position.textContent.trim();
                }
            })



            const informations = doc.querySelectorAll('span.Typography_typography___mliz.generated_body2__P92dE.Typography_margins__GGnT6');
            document.getElementById('foot').value = informations[0].innerHTML;

            const informations2 = doc.querySelectorAll('span.Typography_typography___mliz.generated_body2__P92dE.Typography_margins__GGnT6');


            document.getElementById("country").value = informations2[4].innerHTML;
            document.getElementById("age").value = informations2[5].innerHTML;
            document.getElementById("height").value = informations2[1].innerHTML;
            document.getElementById("weight").value = informations2[2].innerHTML;

            const divs = doc.querySelectorAll("div.Typography_typography___mliz.generated_body2__P92dE.Typography_margins__GGnT6");

            // Değerleri bir diziye ekle
            var values = Array.from(spanElements).map(span => span.textContent.trim());
            var str_id = 1;

            var market = 0;
            for (var i = 1; i <= 35; i++) {
                if (i == 1 || i == 4 || i == 11 || i == 18 || i == 25 || i == 31) {
                    continue;
                }

                var str = str_id.toString();
                document.getElementById(str).value = values[i - 1];
                market += parseInt(values[i - 1], 10);
                str_id++;

            }
            var marketPrice = parseInt(((market * 40000000) / 2000),10);

            document.getElementById("market_price").value = String(marketPrice);

            Swal.close();
        },
        error: function (xhr, status, error) {
            Swal.close();
            Message(false, "Veriler sunucundan getirilemedi. Lütfen linkinizi kontrol ediniz!");
            console.error("Hata oluştu:", error);
        }
    });
});

function generateSkillInputsFromSelect() {
    const count = parseInt(document.getElementById('skill-count-select').value, 10);
    const container = document.getElementById('skills-container');
    container.innerHTML = ''; // Önceki alanları temizle

    if (isNaN(count) || count < 1) {
        return;
    }

    for (let i = 0; i < count; i++) {
        const skillDiv = document.createElement('div');
        skillDiv.classList.add('skill-item');
        skillDiv.innerHTML = `
            <label class="required fs-5 fw-semibold mb-2" for="skill-title-${i}">Yetenek ${i + 1} Başlık:</label>
            <input class="form-control form-control-solid" type="text" name="Statistic.footballerAbilities[${i}].AbilityName" placeholder="Yetenek Başlığı" required />

            <label class="required fs-5 fw-semibold mb-2" for="skill-description-${i}">Açıklama:</label>
            <input class="form-control form-control-solid mb-5" name="Statistic.footballerAbilities[${i}].Description" placeholder="Yetenek Açıklaması" required />

            <hr />
            `;
        container.appendChild(skillDiv);
    }
}
function generatePositionInputsFromSelect() {
    const count = parseInt(document.getElementById('position-count-select').value, 10);
    const container = document.getElementById('positions-container');
    container.innerHTML = ''; // Önceki alanları temizle

    if (isNaN(count) || count < 1) {
        return;
    }

    // Dinamik input oluşturma
    for (let i = 0; i < count; i++) {
        const positionDiv = document.createElement('div');
        positionDiv.classList.add('position-item');
        positionDiv.innerHTML = `
                <label class="required fs-5 fw-semibold mb-2" for="position-title-${i}">Pozisyon ${i + 1} Başlık:</label>
                <input 
                    class="form-control form-control-solid" 
                    type="text" 
                    id="position-title-${i}" 
                    name="FootballerPositions[${i}].ShortPosition" 
                    placeholder="Pozisyon Başlığı" 
                    required 
                />
                <hr />
            `;
        container.appendChild(positionDiv);
    }

}



