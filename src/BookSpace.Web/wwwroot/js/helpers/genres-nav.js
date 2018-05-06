$().ready(function () {
    $.get("/Home/GenresNav", function (content) {
        $("#genres-nav").html(content);
    });
});