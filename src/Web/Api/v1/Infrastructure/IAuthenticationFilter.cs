using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Infrastructure
{
	public interface IAuthenticationFilter
	{
		void Handle(IHttpRequest request, IHttpResponse response, object dto);
	}
}