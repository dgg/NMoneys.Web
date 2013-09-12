using System.Linq;
using System.Web.Mvc;

namespace NMoneys.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			Currency.InitializeAllCurrencies();

			ViewBag.Header = "nMoneys";
			ViewBag.Currencies = Currency.FindAll();
			return View();
		}
	}
}