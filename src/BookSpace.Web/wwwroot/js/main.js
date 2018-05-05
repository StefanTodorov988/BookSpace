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


    var search = function () {

        var searchWrap = $('.header-search'),
            searchField = searchWrap.find('.search-field'),
            closeSearch = searchWrap.find('.header-search-close'),
            searchTrigger = $('.header-search-btn'),
            siteBody = $('body');


        searchTrigger.on('click', function (e) {

            e.preventDefault();
            e.stopPropagation();

            var $this = $(this);

            siteBody.addClass('search-is-visible');
            setTimeout(function () {
                searchWrap.find('.search-field').focus();
            }, 100);

        });

        closeSearch.on('click', function (e) {

            var $this = $(this);

            e.stopPropagation();

            if (siteBody.hasClass('search-is-visible')) {
                siteBody.removeClass('search-is-visible');
                setTimeout(function () {
                    searchWrap.find('.search-field').blur();
                }, 100);
            }
        });

        searchWrap.on('click', function (e) {
            if (!$(e.target).is('.search-field')) {
                closeSearch.trigger('click');
            }
        });

        searchField.on('click', function (e) {
            e.stopPropagation();
        });

        searchField.attr({ placeholder: 'Type Keywords', autocomplete: 'off' });

    };

    var login = function () {
        var loginWrap = $('.header-login'),
            usernameField = loginWrap.find('.text-field'),
            passwordField = loginWrap.find('password-field'),
            closeLogin = loginWrap.find('.header-login-close'),
            loginTrigger = $('.header-login-btn'),
            siteBody = $('body');


        loginTrigger.on('click', function (e) {
            e.preventDefault();
            e.stopPropagation();

            var $this = $(this);

            siteBody.addClass('login-is-visible');
            setTimeout(function () {
                loginWrap.find('.text-field').focus();
                loginWrap.find('password-field').focus();
            }, 100);

        });

        closeLogin.on('click', function (e) {

            var $this = $(this);

            e.stopPropagation();

            if (siteBody.hasClass('login-is-visible')) {
                siteBody.removeClass('login-is-visible');
                setTimeout(function () {
                    loginWrap.find('.text-field').blur();
                    loginWrap.find('.password-field').blur();
                }, 100);
            }
        });

        loginWrap.on('click', function (e) {
            if (!$(e.target).is('.text-field') || !$(e.target).is('.password-field')) {
                closeLogin.trigger('click');
            }
        });

        usernameField.on('click', function (e) {
            e.stopPropagation();
        });

        passwordField.on('click', function (e) {
            e.stopPropagation();
        });

        usernameField.attr({ placeholder: 'Username' });
        passwordField.attr({ placeholder: 'Password' });

    };

    var register = function () {
        var registerWrap = $('.header-register'),
            usernameField = registerWrap.find('.text-field'),
            passwordField = registerWrap.find('.password-field'),
            emailField = registerWrap.find('.email-text-field'),
            confirmPasswordField = registerWrap.find('.confirm-password-field'),
            closeRegister = registerWrap.find('.header-register-close'),
            registerTrigger = $('.header-register-btn'),
            siteBody = $('body');


        registerTrigger.on('click', function (e) {
            e.preventDefault();
            e.stopPropagation();

            var $this = $(this);

            siteBody.addClass('register-is-visible');
            setTimeout(function () {
                registerWrap.find('.text-field').focus();
                registerWrap.find('.password-field').focus();
            }, 100);

        });

        closeRegister.on('click', function (e) {

            var $this = $(this);

            e.stopPropagation();

            if (siteBody.hasClass('register-is-visible')) {
                siteBody.removeClass('register-is-visible');
                setTimeout(function () {
                    registerWrap.find('.text-field').blur();
                    registerWrap.find('.password-field').blur();
                    registerWrap.find('.email-text-field').blur();
                    registerWrap.find('.confirm-password-field').blur();
                }, 100);
            }
        });

        registerWrap.on('click', function (e) {
            if (!$(e.target).is('.text-field') || !$(e.target).is('.password-field')) {
                closeRegister.trigger('click');
            }
        });

        usernameField.on('click', function (e) {
            e.stopPropagation();
        });

        passwordField.on('click', function (e) {
            e.stopPropagation();
        });

        emailField.on('click', function (e) {
            e.stopPropagation();
        });

        confirmPasswordField.on('click', function (e) {
            e.stopPropagation();
        });

        usernameField.attr({ placeholder: 'Username' });
        passwordField.attr({ placeholder: 'Password' });
        emailField.attr({ placeholder: 'Email' });
        confirmPasswordField.attr({ placeholder: 'Confirm password' });

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

        var loadFile = function (event) {
            var output = document.getElementById('image-preview');
            output.src = URL.createObjectURL(event.target.files[0]);
        };

    };




    (function Init() {
        preloader();
        search();
        login();
        register();
        masonryGrid();
        smoothScroll();
        loadFile();
    })();

})(jQuery);