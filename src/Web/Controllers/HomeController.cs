using System.Web.Mvc;
using System.Web.SessionState;

namespace NMoneys.Web.Controllers
{
	[SessionState(SessionStateBehavior.Disabled)]
	public class HomeController : Controller
	{
		[OutputCache(CacheProfile = "home")]
		public ActionResult Index()
		{
			return View();
		}
	}
}