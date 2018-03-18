$(document).ready(function(){
	var w_h = $('NAVIGATOR').height() * 1.5;
	$('#logo img').css('height', w_h);

	$('.exp_cat').hide();
})

function meow(flag) {
	if (flag == 1) {
        $('#cat_login').fadeIn(300, function () { });
	}
	else {
        $('#cat_register').fadeIn(300, function () { });
	}
}