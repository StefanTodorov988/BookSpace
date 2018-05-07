$().ready(function () {
    $.get("/Home/TagsNav", function (content) {
        $("#tags-nav").html(content);
    });
});