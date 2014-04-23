using System.IO;
using System.Web.Mvc;
using System.Web.SessionState;
using NMoneys.Web.Models;

namespace NMoneys.Web.Controllers
{
	[SessionState(SessionStateBehavior.Disabled)]
	public class SamplesController: Controller
	{
		[OutputCache(CacheProfile = "samples")]
		public ActionResult Index()
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