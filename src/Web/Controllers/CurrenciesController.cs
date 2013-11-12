using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NMoneys.Web.Models;

namespace NMoneys.Web.Controllers
{
	public class CurrenciesController: Controller
	{
		[OutputCache(CacheProfile = "snapshots")]
		public ActionResult Index()
		{
			IEnumerable<GroupedByInitialInBatches> byInitial = GroupedByInitialInBatches.Collection(
				Currency.FindAll()
				.Select(c => new Snapshot(c)),
				s => s.AlphabeticCode,
				batchSize: 4);

			return View(byInitial);
		} 

		public ActionResult Detail(CurrencyIsoCode code)
		{
			Currency currency = Currency.Get(code);
			var snapshot = new Snapshot(currency);
			return PartialView(snapshot);
		}
	}
}