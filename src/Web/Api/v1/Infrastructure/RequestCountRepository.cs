using System.Runtime.Caching;
using Antlr.Runtime.Misc;

namespace NMoneys.Web.Api.v1.Infrastructure
{
	public class RequestCountRepository : IRequestCountRepository
	{
		private MemoryCache _cache;

		public RequestCountRepository()
		{
			_cache = new MemoryCache(ThrottlingConfiguration.Key);
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

		public RequestCount Ensure(ApiKey key, Func<RequestCount> initialization)
		{
			string cacheKey = key.ToString();
			var count = _cache.Get(cacheKey) as RequestCount;
			if (count == null)
			{
				count = initialization();
				_cache.Add(cacheKey, count, new CacheItemPolicy { AbsoluteExpiration = count.Expiration });
			}
			return count;
		}

		public void Update(ApiKey key, RequestCount count)
		{
			string cacheKey = key.ToString();
			_cache.Set(cacheKey, count, new CacheItemPolicy { AbsoluteExpiration = count.Expiration });
		}
	}
}