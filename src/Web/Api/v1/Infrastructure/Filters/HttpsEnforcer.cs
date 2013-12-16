using System.Net;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Infrastructure.Filters
{
	public class HttpsEnforcer : IHttpsEnforcer
	{
		public static void Handle(IHttpRequest request, IHttpResponse response, object dto)
		{
			var rater = request.TryResolve<IHttpsEnforcer>();
			rater.Enforce(request);
		}

		public void Enforce(IHttpRequest request)
		{
			if (!request.IsSecureConnection)
			{
				throw new HttpError(HttpStatusCode.Forbidden, "All API calls shall be made through SSL.");
			}
		}
	}
}