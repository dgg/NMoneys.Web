using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Infrastructure.Filters
{
	public interface IHttpsEnforcer
	{
		void Enforce(IHttpRequest request);
	}
}