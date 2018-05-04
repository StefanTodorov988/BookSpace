function showSuccessMessage(message) {
    $("div.success").empty().append(`<p>${message}</p>`);
    $("div.success").fadeIn(300).delay(1500).fadeOut(400);
};

function showFailureMessage(message) {
    $("div.failure").empty().append(`<p>${message}</p>`);
    $("div.failure").fadeIn(300).delay(1500).fadeOut(400);
};

function showWarningMessage(message) {
    $("div.warning").empty().append(`<p>${message}</p>`);
    $("div.warning").fadeIn(300).delay(1500).fadeOut(400);
};