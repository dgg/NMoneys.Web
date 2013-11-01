using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Infrastructure.Filters
{
	public interface IRequestThrottler
	{
		void Throttle(IHttpRequest request, IHttpResponse response);
	}
}