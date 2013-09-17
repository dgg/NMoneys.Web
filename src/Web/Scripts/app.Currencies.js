$(function () {
	var $searchBox = $('#search-box');
	var snapshotSelector = '.snapshot';
	var $snapshots = $(snapshotSelector);
	var $titles = $('article h3');
	$searchBox.quicksearch(snapshotSelector,
		{
			'delay': 300,
			'onAfter': function () {
				var searchTerms = $searchBox.val();
				if (searchTerms) {
					$snapshots.unhighlight().highlight(searchTerms);
					$titles.hide();
				}
				else {
					$snapshots.unhighlight();
					$titles.show();
				}
			}
		});
});