function formatDate(dateString){
    const date = new Date(dateString);
    const day = String(date.getDate()).padStart(2, '0'); // Gün (2 basamak)
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Ay (0'dan başladığı için +1)
    const year = date.getFullYear(); // Yıl
    return `${day}-${month}-${year}`;
};

function formatDateWithHours(dateString) {
    const date = new Date(dateString);
    const day = String(date.getDate()).padStart(2, '0'); // Gün (2 basamak)
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Ay (0'dan başladığı için +1)
    const year = date.getFullYear(); // Yıl

    const hour = String(date.getHours()).padStart(2, '0');
    const minute = String(date.getMinutes()).padStart(2, '0');
    return `${hour}.${minute} ${day}-${month}-${year}`;
};