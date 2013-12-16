using System;
using System.Configuration;
using System.Globalization;
using System.Web.Mvc;

namespace NMoneys.Web.Controllers.Infrastructure
{
	public class ConfigurableRequireHttpsAttribute : RequireHttpsAttribute
	{
		protected override void HandleNonHttpsRequest(AuthorizationContext filterContext)
		{
			ushort port;
			// redirect if port configured (useful for testing)
			if (ushort.TryParse(ConfigurationManager.AppSettings["https_port"], NumberStyles.Integer, CultureInfo.InvariantCulture, out port))
			{
				base.HandleNonHttpsRequest(filterContext);
				var builder = new UriBuilder(filterContext.HttpContext.Request.Url);
				builder.Scheme = Uri.UriSchemeHttps;
				if (port != 443)
				{
					builder.Port = port;
				}

				filterContext.Result = new RedirectResult(builder.Uri.ToString());
			}
		}
	}
}