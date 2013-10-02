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

		private readonly Stack<Action> _disposables = new Stack<Action>();
		
		public HostBootstrapper Bootstrap(Funq.Container container)
		{
			registerTrackingDisposables<KeyVerifier, IKeyVerifier>(container);
			registerTrackingDisposables<AppSettings, IResourceManager>(container);
			registerTrackingDisposables<RequestCountRepository, IRequestCountRepository>(container);
			
			return this;
		}

		private void registerTrackingDisposables<T, TService>(Funq.Container container) where T : class,  TService
		{
			container.RegisterAutoWiredAs<T, TService>();
			// execute dispose later on
			if (typeof(T).GetInterface("IDisposable") != null)
			{
				_disposables.Push(()=> ((IDisposable)container.Resolve<TService>()).Dispose());
			}
		}

		public HostBootstrapper Bootstrap(
			List<Action<IHttpRequest, IHttpResponse, object>> requestFilters,
			List<Action<IHttpRequest, IHttpResponse, object>> responseFilters)
		{
			requestFilters.Add(ApiAuthentication.Handle);
			requestFilters.Add(RequestThrottler.Handle);

			responseFilters.Add(RequestRates.Handle);

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
			while (_disposables.Count > 0)
			{
				_disposables.Pop()();
			}
		}
	}
}