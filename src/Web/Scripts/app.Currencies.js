$(function () {
	var $searchBox = $('#search-box');
	var snapshotSelector = '.snapshot';
	var $snapshots = $(snapshotSelector);
	var $titles = $('.initial');
	$searchBox.quicksearch(snapshotSelector,
		{
			'delay': 300,
			'onAfter': function () {
				var searchTerms = $searchBox.val();
				if (searchTerms) {
					$snapshots.unhighlight().highlight(searchTerms);
					$titles.hide();
					
					ga('send', 'event', 'Currencies', 'Search', searchTerms);
				}
				else {
					$snapshots.unhighlight();
					$titles.show();
				}
			}
		});

	$('.modal').on('shown', function() {
		$('.show-info').tooltip();
	});
	
});