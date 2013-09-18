using System.Collections.Generic;
using System.IO;
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

		[OutputCache(CacheProfile = "snapshots")]
		public ActionResult Currencies()
		{
			IEnumerable<GroupedByInitialInBatches> byInitial = GroupedByInitialInBatches.Collection(
				Currency.FindAll()
				.Select(c => new Snapshot(c)),
				s => s.AlphabeticCode,
				batchSize: 4);

			return View(byInitial);	
		}

		public ActionResult About()
		{
			var model = new SnippetCollection(new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath("/Content/src")));
			return View(model);
		}
	}
}