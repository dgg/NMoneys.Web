using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NMoneys.Web.App_Start;
using NMoneys.Web.Views;

namespace NMoneys.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			BundleConfig.RegisterBundles(BundleTable.Bundles);

			Currency.InitializeAllCurrencies();

			ViewEngines.Engines.Clear();
			var razor = new RazorViewEngine();
			razor.ViewLocationCache = new TwoLevelViewCache(razor.ViewLocationCache);
			ViewEngines.Engines.Add(razor);
		}
	}
}