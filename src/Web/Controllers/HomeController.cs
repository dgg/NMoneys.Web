using System.Linq;
using System.Web.Mvc;

using Microsoft.Web.Mvc;

namespace NMoneys.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			Currency.InitializeAllCurrencies();

			ViewBag.Header = "nMoneys : /";
			ViewBag.Currencies = Currency.FindAll().Take(5);
			return View();
		}

		public ActionResult Currencies()
		{
			Currency.InitializeAllCurrencies();

			ViewBag.Header = "nMoneys : Currencies";
			ViewBag.Currencies = Currency.FindAll().Take(5);
			return View();	
		}

		public ActionResult About()
		{
			ViewBag.Header = "nMoneys : About";
			return View();
		}
	}
}