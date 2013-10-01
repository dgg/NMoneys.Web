using Funq;
using NMoneys.Web.Api.v1.Infrastructure;
using ServiceStack.WebHost.Endpoints;

namespace Tests.Api.Support
{
	public class TestHost : AppHostHttpListenerBase
	{
		private readonly HostBootstrapper _bootrapper;

		public TestHost() : base(HostBootstrapper.ServiceName, HostBootstrapper.ServiceContainer)
		{
			_bootrapper= new HostBootstrapper();
		}

		public override void Configure(Container container)
		{
			EndpointHostConfig config = _bootrapper
				.BootstrapAll(this);
			SetConfig(config);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			_bootrapper.Dispose();
		}
	}
}