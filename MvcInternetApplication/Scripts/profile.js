$(document).ready(function () {
	$('#t2').hide();

	$('#prOK').hide();
	$('#prCancel').hide();
	$('.edit_profile').hide();
})

function profile_edit(flag) {
	if (flag == 1) {
		$('.edit_profile').show(1000);
		$('#prOK').show();
		$('#prCancel').show();

		$('.profile_edit').prop('disabled', false);
	}
	if (flag == 2) {
		$('.edit_profile').hide(1000);
		$('#prOK').hide();
		$('#prCancel').hide();

		$('.profile_edit').prop('disabled', true);
	}
}

function profile_edit_no_anim(flag) {
	if (flag == 1) {
		$('.edit_profile').show();
		$('#prOK').show();
		$('#prCancel').show();

		$('.profile_edit').prop('disabled', false);
	}
	if (flag == 2) {
		$('.edit_profile').hide();
		$('#prOK').hide();
		$('#prCancel').hide();

		$('.profile_edit').prop('disabled', true);
	}
}