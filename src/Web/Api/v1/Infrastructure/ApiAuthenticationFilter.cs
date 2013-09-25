using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Infrastructure
{

	public class ApiAuthenticationFilter : IAuthenticationFilter
	{
		private readonly IKeyVerifier _verifier;

		public ApiAuthenticationFilter(IKeyVerifier verifier)
		{
			_verifier = verifier;
		}

		public void Handle(IHttpRequest request, IHttpResponse response, object dto)
		{
			ApiKey apiKey = ApiKey.ExtractFrom(request);
			if (apiKey.IsMissing || !_verifier.Verify(apiKey))
			{
				string unauthorized = string.Format("Unauthorized request. Please, include a valid '{0}'", ApiKey.ParameterName);
				throw HttpError.Unauthorized(unauthorized);
			}
		}
	}
}