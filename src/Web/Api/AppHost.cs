using Funq;
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
			SetConfig(new EndpointHostConfig
			{
				EnableFeatures = Feature.All.Remove(Feature.Jsv | Feature.Soap),
				DefaultContentType = ContentType.Json,
				ServiceStackHandlerFactoryPath = "api",
			});
			ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;
		}
	}
}