using System;
using System.Globalization;
using System.Web.Mvc;

namespace NMoneys.Web.Controllers.Infrastructure
{
	// useful for testing locally (where standard secure ports cannot be used)
	public class NonStandardSecurePortRedirector
	{
		private ushort? _port;

		public NonStandardSecurePortRedirector(string configurationValue)
		{
			_port = tryGetConfiguredHttpPort(configurationValue);
		}

		public void RedirectIfPortConfigured(AuthorizationContext filterContext, Action<AuthorizationContext> handleInsecureRequest)
		{
			if (_port.HasValue)
			{
				handleInsecureRequest(filterContext);
				var builder = new UriBuilder(filterContext.HttpContext.Request.Url)
				{
					Scheme = Uri.UriSchemeHttps,
					Port = _port.Value
				};

				filterContext.Result = new RedirectResult(builder.Uri.ToString());
			}
		}

		private ushort? tryGetConfiguredHttpPort(string configurationValue)
		{
			ushort parsed;
			return ushort.TryParse(configurationValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed) ?
				parsed :
				default(ushort?);
		}
	}
}