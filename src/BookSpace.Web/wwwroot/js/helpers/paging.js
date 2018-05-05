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
    });
});

//$().ready(function () {
//    $(".page-num-category").on("click", function () {
//        $(".page-num.current").removeClass("current");
//        $(this).addClass("current");
//        var page = parseInt($(this).html());

//        $.ajax({
//            url: 'Book/BooksByCategoryList',
//            data: { "page": page },
//            success: function (data) {
//                $("#paged-by-category").html(data);
//            }
//        });
//    });
//});