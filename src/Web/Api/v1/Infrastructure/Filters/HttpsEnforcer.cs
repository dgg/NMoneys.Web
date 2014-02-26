using System.Net;
using NMoneys.Web.Infrastructure.Web;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Infrastructure.Filters
{
	public class HttpsEnforcer : IHttpsEnforcer
	{
		private readonly SecureRequestSpecification _secureSpec;

		public HttpsEnforcer()
		{
			_secureSpec = new SecureRequestSpecification();
		}

		public static void Handle(IHttpRequest request, IHttpResponse response, object dto)
		{
			var enforcer = request.TryResolve<IHttpsEnforcer>();
			enforcer.Enforce(request);
		}

		public void Enforce(IHttpRequest request)
		{
			if (!_secureSpec.IsSatisfiedBy(request))
			{
				throw new HttpError(HttpStatusCode.Forbidden, "All API calls shall be made through SSL.");
			}
		}
	}
}