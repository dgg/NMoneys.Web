using Funq;
using NMoneys.Web.Api.v1.Infrastructure;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

namespace NMoneys.Web.Api
{
	public class AppHost : AppHostBase
	{
		public AppHost() : base(HostBootstrapper.ServiceName, HostBootstrapper.ServiceContainer) { }

		public override void Configure(Container container)
		{
			EndpointHostConfig config = new HostBootstrapper().BootstrapAll(
				container,
				RequestFilters,
				ResponseFilters);

			SetConfig(config);
		}

		
	}
}