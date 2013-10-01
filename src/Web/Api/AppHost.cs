using Funq;
using NMoneys.Web.Api.v1.Infrastructure;
using ServiceStack.WebHost.Endpoints;

namespace NMoneys.Web.Api
{
	public class AppHost : AppHostBase
	{
		private readonly HostBootstrapper _bootstrapper;

		public AppHost() : base(HostBootstrapper.ServiceName, HostBootstrapper.ServiceContainer)
		{
			_bootstrapper = new HostBootstrapper();
		}

		public override void Configure(Container container)
		{
			EndpointHostConfig config = _bootstrapper
				.BootstrapAll(this);
			SetConfig(config);
		}

		public override void Dispose()
		{
			base.Dispose();
			_bootstrapper.Dispose();
		}
	}
}