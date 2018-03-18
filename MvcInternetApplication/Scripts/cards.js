function ref_cards() {
    //resizing cards
    var cards_h = $('.cards').width() * 1.618;
    jQuery('.cards').css("height", cards_h);
    jQuery('.card_container').css("height", cards_h);
    jQuery('.mist_card').css("height", cards_h);
    jQuery('.cancel_card').css("height", cards_h);

    jQuery('.mist_card').css("bottom", cards_h);
    jQuery('.cancel_card').css("bottom", cards_h);

    //resizing big_cards
    var big_cards_h = $('.cards').width() * 1.618;
    jQuery('.big_cards').css("height", big_cards_h);
}

function rev() {
    $('#screen').fadeIn(800, function () { });

    var exp_card_w, exp_card_left;

    if ($(window).width() > 500) {
        exp_card_w = $('#exp_card').height() / 1.618;
        exp_card_left = ($(document).width() / 2) - (exp_card_w / 2);
        jQuery('#exp_card').css("width", exp_card_w);
        jQuery('#exp_card').css("left", exp_card_left);
    }
    else {
        exp_card_w = $('#exp_card').width();
        exp_card_left = ($(window).width() - exp_card_w) / 2;
        jQuery('#exp_card').css("height", exp_card_w * 1.618);
        jQuery('#exp_card').css("left", exp_card_left);
    }
}

