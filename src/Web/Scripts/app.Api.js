$(function () {
	$('form').parsley({
		successClass: 'success',
		errorClass: 'error',
		errors: {
			classHandler: function(el) {
				return $(el).closest('.control-group');
			},
			errorsWrapper: '<span class=\"help-inline\"></span>',
			errorElem: '<span></span>'
		}
	});
});