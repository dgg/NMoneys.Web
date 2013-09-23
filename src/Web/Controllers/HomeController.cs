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

		public ActionResult Samples()
		{
			var model = new CodeSnippetCollection(
				mapDirectory("/content/src/quickstart"),
				mapDirectory("/content/src/codeproject"),
				mapDirectory("/content/src/codeproject_exchange")
				);
			return View(model);
		}

		private static DirectoryInfo mapDirectory(string virtualPath)
		{
			string physicalPath = System.Web.Hosting.HostingEnvironment.MapPath(virtualPath);
			return new DirectoryInfo(physicalPath);
		}
	}
}