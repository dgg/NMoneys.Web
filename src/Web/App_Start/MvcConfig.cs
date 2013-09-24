using System.Web.Mvc;
using NMoneys.Web.Views;

namespace NMoneys.Web.App_Start
{
	public class MvcConfig
	{
		public static void Optimize()
		{
			ViewEngines.Engines.Clear();
			var razor = new RazorViewEngine();
			razor.ViewLocationCache = new TwoLevelViewCache(razor.ViewLocationCache);
			ViewEngines.Engines.Add(razor);
		}
	}
}