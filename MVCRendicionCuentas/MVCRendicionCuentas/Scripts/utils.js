function wrapText(s, n, p) {
    var ss = String(s).substring(0, n) + ((String(s).length > n) ? p : '');
    return ss;
}

function mostrarLoading() {
    $('.loading-mod').removeClass('hidden');
}

function cerrarLoading() {
    $('.loading-mod').addClass('hidden');
}

function errorLoading() {
    $('.loading-mod > div > span:first-child').addClass('hidden');
    $('.loading-mod > div > span:last-child').removeClass('hidden');
    setTimeout(function () {
        $('.loading-mod > div > span:first-child').removeClass('hidden');
        $('.loading-mod > div > span:last-child').addClass('hidden');
        cerrarLoading();
    },3000);
}