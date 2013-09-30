using System;
using System.Collections.Generic;
using System.Reflection;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

namespace NMoneys.Web.Api.v1.Infrastructure
{
	public class HostBootstrapper
	{
		public static readonly string ServiceName = "nMoneys";
		public static readonly Assembly ServiceContainer = typeof(HostBootstrapper).Assembly;

		public HostBootstrapper Bootstrap(Funq.Container container)
		{
			container.RegisterAutoWiredAs<KeyVerifier, IKeyVerifier>();

			return this;
		}

		public HostBootstrapper Bootstrap(
			List<Action<IHttpRequest, IHttpResponse, object>> requestFilters,
			List<Action<IHttpRequest, IHttpResponse, object>> responseFilters)
		{
			requestFilters.Add(ApiAuthenticationFilter.Handle);

			return this;
		}

		public EndpointHostConfig WithConfig()
		{
			ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;

			var config = new EndpointHostConfig
			{
				EnableFeatures = Feature.All.Remove(Feature.Jsv | Feature.Soap | Feature.Xml),
				DefaultContentType = ContentType.Json,
				ServiceStackHandlerFactoryPath = "api",
			};

			return config;
		}

		public EndpointHostConfig BootstrapAll(Funq.Container container,
			List<Action<IHttpRequest, IHttpResponse, object>> requestFilters,
			List<Action<IHttpRequest, IHttpResponse, object>> responseFilters)
		{
				return Bootstrap(container)
					.Bootstrap(requestFilters, responseFilters)
					.WithConfig();	
		}
	}
}