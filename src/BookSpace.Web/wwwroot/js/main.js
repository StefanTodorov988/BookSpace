(function ($) {

    "use strict";

    var cfg = {
        scrollDuration: 800,
    },

        $WIN = $(window);

    var preloader = function () {
        $("html").addClass('cl-preload');

        $WIN.on('load', function () {

            $("#loader").fadeOut("slow", function () {
                $("#preloader").delay(300).fadeOut("slow");
            });

            $("html").removeClass('cl-preload');
            $("html").addClass('cl-loaded');

        });
    };

    var masonryGrid = function () {
        var containerBricks = $('.masonry');

        containerBricks.imagesLoaded(function () {
            containerBricks.masonry({
                itemSelector: '.masonry-brick',
                percentPosition: true,
                resize: true
            });
        });

        containerBricks.imagesLoaded().progress(function () {
            containerBricks.masonry('layout');
        });
    };


    var smoothScroll = function () {
        $('.smoothscroll').on('click', function (e) {
            var target = this.hash,
                $target = $(target);

            e.preventDefault();
            e.stopPropagation();

            $('html, body').stop().animate({
                'scrollTop': $target.offset().top
            }, cfg.scrollDuration, 'swing').promise().done(function () {

                // check if menu is open
                if ($('body').hasClass('menu-is-open')) {
                    $('.header-menu-toggle').trigger('click');
                }

                window.location.hash = target;
            });
        });

    };


    //var loadFile = function (event) {
    //    var output = document.getElementById('image-preview');
    //    output.src = URL.createObjectURL(event.target.files[0]);
    //};

    (function Init() {
        preloader();
        masonryGrid();
        smoothScroll();
        //loadFile(event);
    })();

})(jQuery);