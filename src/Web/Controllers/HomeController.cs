using System.Web.Mvc;

namespace NMoneys.Web.Controllers
{
	[OutputCache(CacheProfile = "home")]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}