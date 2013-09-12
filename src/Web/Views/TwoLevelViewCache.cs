using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace NMoneys.Web.Views
{
	public class TwoLevelViewCache : IViewLocationCache
	{
		private readonly IViewLocationCache _secondLevelCache;

		public TwoLevelViewCache(IViewLocationCache secondLevelCache)
		{
			_secondLevelCache = secondLevelCache;
		}

		private readonly static object _key = new object();
		private static IDictionary<string, string> getRequestCache(HttpContextBase httpContext)
		{
			var d = httpContext.Items[_key] as IDictionary<string, string>;
			if (d == null)
			{
				d = new Dictionary<string, string>();
				httpContext.Items[_key] = d;
			}
			return d;
		}

		public string GetViewLocation(HttpContextBase httpContext, string key)
		{
			var firstLevelCache = getRequestCache(httpContext);
			string location;
			if (!firstLevelCache.TryGetValue(key, out location))
			{
				location = _secondLevelCache.GetViewLocation(httpContext, key);
				firstLevelCache[key] = location;
			}
			return location;
		}

		public void InsertViewLocation(HttpContextBase httpContext, string key, string virtualPath)
		{
			_secondLevelCache.InsertViewLocation(httpContext, key, virtualPath);
		}
	}
}