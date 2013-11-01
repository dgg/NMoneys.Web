using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Infrastructure.Filters
{
	public class ApiAuthenticator : IApiAuthenticator
	{
		public static void Handle(IHttpRequest request, IHttpResponse response, object dto)
		{
			var authenticator = request.TryResolve<IApiAuthenticator>();
			authenticator.Authenticate(request);
		}

		public void Authenticate(IHttpRequest request)
		{
			var verifier = request.TryResolve<IKeyVerifier>();

			ApiKey apiKey = ApiKey.ExtractFrom(request);
			if (apiKey.IsMissing || !verifier.Verify(apiKey))
			{
				string unauthorized = string.Format("Unauthorized request. Please, include a valid '{0}'", ApiKey.ParameterName);
				throw HttpError.Unauthorized(unauthorized);
			}
		}
	}
}