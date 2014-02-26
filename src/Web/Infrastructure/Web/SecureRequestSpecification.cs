using System;
using System.Collections.Specialized;
using System.Web;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Infrastructure.Web
{
	public class SecureRequestSpecification
	{
		public bool IsSatisfiedBy(HttpRequestBase request)
		{
			bool isSecure = request.IsSecureConnection ||
				originatingProtocolIsHttps(request.Headers);
			return isSecure;
		}

		public bool IsSatisfiedBy(IHttpRequest request)
		{
			bool isSecure = request.IsSecureConnection ||
				originatingProtocolIsHttps(request.Headers);
			return isSecure;
		}

		private static bool originatingProtocolIsHttps(NameValueCollection headers)
		{
			return headers != null &&
				StringComparer.OrdinalIgnoreCase.Equals(headers["X-Forwarded-Proto"],
					Uri.UriSchemeHttps);
		}
	}
}