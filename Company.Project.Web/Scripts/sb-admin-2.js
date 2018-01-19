$(function () {
    $(window).bind("load resize", function () {
        topOffset = 50;
        width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            $('div.navbar-collapse').addClass('collapse');
            topOffset = 100; // 2-row-menu
        } else {
            $('div.navbar-collapse').removeClass('collapse');
        }

        height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
        height = height - topOffset;
        if (height < 1) height = 1;
        if (height > topOffset) {
            $("#page-wrapper").css("min-height", (height) + "px");
        }
    });
    var url = window.location;
    var element = $('ul.nav a').filter(function () {
        return this.href == url || url.href.indexOf(this.href) == 0;
    }).addClass('active').parent().parent().addClass('in').parent();
    if (element.is('li')) {
        element.addClass('active');
    }

    $('#side-menu').metisMenu();

    if (typeof (localStorage.getItem('sidebar-toggle')) === 'undefined') {
        localStorage.setItem('sidebar-toggle', (typeof ($('.sidebar').closest('.sidebar-open')) == 'undefined' ? true : false));
    }

    if (localStorage.getItem('sidebar-toggle') === 'true') {
        $('.page-wrapper').css('margin', '0 0 0 250px');
        $('.sidebar').css({ 'left': '0px' });

    } else {
        $('.page-wrapper').css('margin', '0');
        $('.sidebar').css('left', '-250px');
    }

    //console.log(localStorage.getItem('sidebar-toggle'));

    $('.sidebar-toggle').click(function () {
        if (localStorage.getItem('sidebar-toggle') === 'true') {
            $('.page-wrapper').animate({ margin: '0' });
            $('.sidebar').animate({ left: '-250px' });

            localStorage.setItem('sidebar-toggle', false);
        } else {
            $('.page-wrapper').animate({ margin: '0 0 0 250px' });
            $('.sidebar').animate({ left: '0px' });

            localStorage.setItem('sidebar-toggle', true);
        }
    });
});