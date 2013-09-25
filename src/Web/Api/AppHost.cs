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
		public AppHost() : base("NMoneys", typeof(AppHost).Assembly) { }

		public override void Configure(Container container)
		{
			configure(container);

			SetConfig(new EndpointHostConfig
			{
				EnableFeatures = Feature.All.Remove(Feature.Jsv | Feature.Soap | Feature.Xml),
				DefaultContentType = ContentType.Json,
				ServiceStackHandlerFactoryPath = "api",
			});
			ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;

			RequestFilters.Add(container.Resolve<IAuthenticationFilter>().Handle);
		}

		private void configure(Container container)
		{
			container.RegisterAutoWiredAs<KeyVerifier, IKeyVerifier>();
			container.RegisterAutoWiredAs<ApiAuthenticationFilter, IAuthenticationFilter>();
		}
	}
}