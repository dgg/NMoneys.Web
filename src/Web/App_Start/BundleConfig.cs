using System.Web.Optimization;

namespace NMoneys.Web.App_Start
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.UseCdn = true;
			//BundleTable.EnableOptimizations = true;

			jQuery(bundles);

			modernizr(bundles);

			site(bundles);

			bootstrap(bundles);

			flatUI(bundles);

			currencies(bundles);
		}

		private static void currencies(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/app_currencies").Include(
				"~/Scripts/jquery.quicksearch.js",
				"~/Scripts/jquery.highlight.js",
				"~/Scripts/app.Currencies.js"));
		}

		private static void flatUI(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/flatUI").Include("~/Content/flat-ui.css"));
		}

		private static void site(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
		}

		private static void modernizr(BundleCollection bundles)
		{
			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));
		}

		private static void jQuery(BundleCollection bundles)
		{
			var jquery = new ScriptBundle("~/bundles/jquery", "//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js").Include("~/Scripts/jquery-{version}.js");
			jquery.CdnFallbackExpression = "window.jQuery";
			bundles.Add(jquery);
		}

		private static void bootstrap(BundleCollection bundles)
		{
			var bootstrap = new ScriptBundle("~/bundles/bootstrap", "//netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/js/bootstrap.min.js").Include("~/Scripts/bootstrap.min.js");
			bootstrap.CdnFallbackExpression = "$.fn.modal";
			bundles.Add(bootstrap);

			bundles.Add(new StyleBundle("~/Content/bootstrap", "//netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/css/bootstrap.min.css").Include("~/Content/bootstrap/css/bootstrap.css"));

			bundles.Add(new StyleBundle("~/Content/bootstrap_responsive").Include("~/Content/bootstrap/css/bootstrap-responsive.css"));
		}
	}
}