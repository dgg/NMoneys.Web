using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Infrastructure.Filters
{
	public interface IRequestRater
	{
		void Rate(IHttpRequest request, IHttpResponse response);
	}
}