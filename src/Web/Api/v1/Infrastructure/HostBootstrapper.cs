using System;
using System.Collections.Generic;
using System.Reflection;
using NMoneys.Web.Api.v1.Infrastructure.Filters;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.Configuration;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

namespace NMoneys.Web.Api.v1.Infrastructure
{
	public class HostBootstrapper : IDisposable
	{
		public static readonly string ServiceName = "nMoneys";
		public static readonly Assembly ServiceContainer = typeof(HostBootstrapper).Assembly;

		private readonly IList<IDisposable> _disposables;
		private readonly RequestThrottler _throttler;

		public HostBootstrapper()
		{
			//create only one cache
			_throttler = new RequestThrottler();
			_disposables = new List<IDisposable>(5) { _throttler };
		}

		public HostBootstrapper Bootstrap(Funq.Container container)
		{
			container.RegisterAutoWiredAs<KeyVerifier, IKeyVerifier>();
			container.RegisterAutoWiredAs<AppSettings, IResourceManager>();

			return this;
		}

		public HostBootstrapper Bootstrap(
			List<Action<IHttpRequest, IHttpResponse, object>> requestFilters,
			List<Action<IHttpRequest, IHttpResponse, object>> responseFilters)
		{
			requestFilters.Add(ApiAuthentication.Handle);
			requestFilters.Add(_throttler.Handle);

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

		public EndpointHostConfig BootstrapAll<T>(T host) where T : IAppHost, IDisposable
		{
			return Bootstrap(host.GetContainer())
				.Bootstrap(host.RequestFilters, host.ResponseFilters)
				.WithConfig();
		}

		public void Dispose()
		{
			foreach (var disposable in _disposables)
			{
				disposable.Dispose();
			}
		}
	}
}