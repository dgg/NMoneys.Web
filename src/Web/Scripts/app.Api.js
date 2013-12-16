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
		},
		listeners: {
			onFormValidate: function (isFormValid, evt, form) {
				if (isFormValid) {
					ga('send', 'event', 'Api', 'Request', $('#Email').val());
				}
				return isFormValid;
			}
		}
	});

	var apiKey = queryObj()['apikey'];
	if ($('#apiKeyConfirmed').length > 0) {
		ga('send', 'event', 'Api', 'Confirmed', apiKey);
	}
	else if ($('#apiKeyNotConfirmed').length > 0) {
		ga('send', 'event', 'Api', 'NotConfirmed', apiKey);
	}
});
// poluting global namespace
function queryObj() {
	var result = {}, keyValuePairs = location.search.slice(1).split('&');

	$(keyValuePairs).each(function () {
		var keyValuePair = this.split('=');
		result[keyValuePair[0].toLowerCase()] = keyValuePair[1] || '';
	});

	return result;
}