    //NAV
    //toggle active on click
$('.navbar li').click(function () {
    $('.navbar li').removeClass('active');
    $(this).addClass('active');
});
    //collapse menu on click
$(document).on('click', '.navbar-collapse.in', function (e) {
    if ($(e.target).is('a') && $(e.target).attr('class') != 'dropdown-toggle') {
        $(this).collapse('hide');
    }
 });