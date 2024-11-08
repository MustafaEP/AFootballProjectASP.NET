function translateData(data) {
    // Ülke çevirisi
    data.forEach(object => {
        object.county = translateCountry(object.county);
        object.playerPosition = translatePosition(object.playerPosition);
        object.playerSecondPositions = translatePosition(object.playerSecondPositions);
        object.strongFoot = translateStrongFoot(object.strongFoot);
        // Diğer veriler için çevirmeler yapılabilir.
    });
    return data;
}

function translateCountry(country) {
    const countries = {
        'Turkey': 'Türkiye',
        'United States': 'Amerika Birleşik Devletleri',
        'United Kingdom': 'Birleşik Krallık',
        'Germany': 'Almanya',
        'France': 'Fransa',
        'Canada': 'Kanada',
        'Australia': 'Avustralya',
        'Italy': 'İtalya',
        'Spain': 'İspanya',
        'Netherlands': 'Hollanda',
        'Brazil': 'Brezilya',
        'Japan': 'Japonya',
        'South Korea': 'Güney Kore',
        'India': 'Hindistan',
        'China': 'Çin',
        'Russia': 'Rusya',
        'Mexico': 'Meksika',
        'Sweden': 'İsveç',
        'Switzerland': 'İsviçre',
        'Argentina': 'Arjantin'
    };
    return countries[country] || country; // Eğer çeviri yoksa orijinal ülke ismini döner
}

function reverseTranslateCountry(country) {
    const countries = {
        'Türkiye': 'Turkey',
        'Amerika Birleşik Devletleri': 'United States',
        'Birleşik Krallık': 'United Kingdom',
        'Almanya': 'Germany',
        'Fransa': 'France',
        'Kanada': 'Canada',
        'Avustralya': 'Australia',
        'İtalya': 'Italy',
        'İspanya': 'Spain',
        'Hollanda': 'Netherlands',
        'Brezilya': 'Brazil',
        'Japonya': 'Japan',
        'Güney Kore': 'South Korea',
        'Hindistan': 'India',
        'Çin': 'China',
        'Rusya': 'Russia',
        'Meksika': 'Mexico',
        'İsveç': 'Sweden',
        'İsviçre': 'Switzerland',
        'Arjantin': 'Argentina'
    };
    return countries[country] || country; // Eğer çeviri yoksa orijinal ülke ismini döner
}


function translatePosition(playerPosition) {
    const playerPositions = {
        'Goalkeeper': 'Kaleci',
        'Right Back': 'Sağ Bek',
        'Center Back': 'Stoper',
        'Left Back': 'Sol Bek',
        'Defensive Midfielder': 'Defansif Orta Saha',
        'Central Midfielder': 'Merkez Orta Saha',
        'Attacking Midfielder': 'Ofansif Orta Saha',
        'Right Winger': 'Sağ Kanat',
        'Left Winger': 'Sol Kanat',
        'Striker': 'Forvet',
        'Second Striker': 'İkinci Forvet'
    };
    return playerPositions[playerPosition] || playerPosition; // Eğer çeviri yoksa orijinal pozisyonu döner
}

function translateStrongFoot(strongFoot) {
    const feet = {
        'Right': 'Sağ Ayak',
        'Left': 'Sol Ayak',
        'Both': 'İkisi'
    };
    return feet[strongFoot] || strongFoot; // Eğer çeviri yoksa orijinal güçlü ayak bilgisini döner
}