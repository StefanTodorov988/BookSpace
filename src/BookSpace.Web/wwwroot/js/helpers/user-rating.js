function rate(id, title, userRate) {
    $.ajax({
        type: "POST",
        url: "/UserBooks/RateBook",
        data: { id: id, rate: userRate },
        success: (response) => {
            showSuccessMessage('You have successfully rated ' + title + ' with rate : ' + userRate);
            setTimeout(function () {
                location.reload();
            }, 2000);
        },
        error: (response) => {
            showFailureMessage('Error rating ' + title);
        }
    })
}