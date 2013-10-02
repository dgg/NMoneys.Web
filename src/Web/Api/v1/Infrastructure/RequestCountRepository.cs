using System.Runtime.Caching;
using NMoneys.Web.Api.v1.Infrastructure.Filters;

namespace NMoneys.Web.Api.v1.Infrastructure
{
	public class RequestCountRepository : IRequestCountRepository
	{
		private MemoryCache _cache;

		public RequestCountRepository()
		{
			_cache = new MemoryCache(RequestThrottler.ConfigurationKey);
		}

		public void Dispose()
		{
			_cache.Dispose();
			_cache = null;
		}

		public RequestCount Get(ApiKey key)
		{
			string cacheKey = key.ToString();
			var count = _cache.Get(cacheKey) as RequestCount;
			return count;
		}

		public void Add(ApiKey key, RequestCount count)
		{
			string cacheKey = key.ToString();
			_cache.Add(cacheKey, count, new CacheItemPolicy { AbsoluteExpiration = count.Expiration });
		}

		public void Update(ApiKey key, RequestCount count)
		{
			string cacheKey = key.ToString();
			_cache.Set(cacheKey, count, new CacheItemPolicy { AbsoluteExpiration = count.Expiration });
		}
	}
}