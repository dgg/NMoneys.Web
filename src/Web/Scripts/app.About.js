$(function () {
	function htmlEncode(str) {
		return $('<div/>').text(str).html();
	}

	$('.code').each(function () {
		var $code = $(this);
		var url = $code.data('src');
		$.get(url, function (data, status, xhr) {
			$code.html(prettyPrintOne(htmlEncode(data)));
		});
	});
});