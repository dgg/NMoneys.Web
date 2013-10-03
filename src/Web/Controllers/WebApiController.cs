using System.Web.Mvc;

namespace NMoneys.Web.Controllers
{
	public class WebApiController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}