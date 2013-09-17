using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NMoneys.Web.Models;

namespace NMoneys.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		//[OutputCache(CacheProfile = "snapshots")]
		public ActionResult Currencies()
		{
			IEnumerable<GroupedByInitialInBatches> byInitial = GroupedByInitialInBatches.Collection(
				Currency.FindAll().Take(15).Select(c => new Snapshot(c)),
				s => s.AlphabeticCode,
				batchSize: 4);

			return View(byInitial);	
		}

		public ActionResult About()
		{
			ViewBag.Header = "nMoneys : About";
			return View();
		}
	}
}