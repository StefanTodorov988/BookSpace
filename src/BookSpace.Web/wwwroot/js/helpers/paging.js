$().ready(function () {
    $(".page-num").on("click", function () {
        $(".page-num.current").removeClass("current");
        $(this).addClass("current");
        var page = parseInt($(this).html());

        $.ajax({
            url: 'Book/BooksList',
            data: { "page": page },
            success: function (data) {
                $(".books-list").html(data);
            }
        });
        $('html, body').animate({ scrollTop: 0 }, 'slow');
    });
});