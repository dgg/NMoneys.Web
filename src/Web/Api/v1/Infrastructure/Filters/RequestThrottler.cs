using System;
using System.Globalization;
using System.Runtime.Caching;
using ServiceStack.Common.Web;
using ServiceStack.Configuration;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Infrastructure.Filters
{
	// IDisposable implementation to reset cache in between tests
	public class RequestThrottler : IDisposable
	{
		public static readonly string ConfigurationKey = "throttling";
		private MemoryCache _cache;

		public RequestThrottler()
		{
			_cache = new MemoryCache(ConfigurationKey);
		}

		public void Handle(IHttpRequest request, IHttpResponse response, object dto)
		{
			var resources = request.TryResolve<IResourceManager>();
			ThrottlingConfiguration configuration = resources.Get(ConfigurationKey, ThrottlingConfiguration.Empty());

			var id = ApiKey.ExtractFrom(request).AsId();
			if (id.HasValue && configuration.ThrottlingEnabled)
			{
				string cacheKey = id.ToString();

				var count = _cache.Get(cacheKey) as RequestCount;
				if (count == null)
				{
					count = new RequestCount(configuration.Period);
					_cache.Add(cacheKey, count, new CacheItemPolicy { AbsoluteExpiration = count.Expiration });
				}
				else if (count.IsLessThan(configuration.NumberOfRequests))
				{
					_cache.Set(cacheKey, count.Increase(), new CacheItemPolicy { AbsoluteExpiration = count.Expiration });
				}
				else
				{
					response.AddHeader("Retry-After", configuration.FormattedSeconds);
					throw new HttpError(429, configuration.ErrorMessage());
				}
			}
		}

		/// <summary>
		/// Do not call it unless you are testing
		/// </summary>
		public void Dispose()
		{
			_cache.Dispose();
			_cache = null;
		}
	}
}