using System.Web.Mvc;

namespace NMoneys.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}