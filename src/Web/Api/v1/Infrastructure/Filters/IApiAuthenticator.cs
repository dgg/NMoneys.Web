using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Infrastructure.Filters
{
	public interface IApiAuthenticator {
		void Authenticate(IHttpRequest request);
	}
}