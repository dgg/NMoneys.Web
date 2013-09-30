using Funq;
using NMoneys.Web.Api.v1.Infrastructure;
using ServiceStack.WebHost.Endpoints;

namespace Tests.Api.Support
{
	public class TestHost : AppHostHttpListenerBase
	{
		public TestHost() : base(HostBootstrapper.ServiceName, HostBootstrapper.ServiceContainer) { }

		public override void Configure(Container container)
		{
			EndpointHostConfig config = new HostBootstrapper()
				.BootstrapAll(
					container,
					RequestFilters,
					ResponseFilters);
			SetConfig(config);
		}
	}
}