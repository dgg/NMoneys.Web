using System;

namespace NMoneys.Web.Api.v1.Infrastructure
{
	public interface IRequestCountRepository: IDisposable
	{
		RequestCount Get(ApiKey key);
		void Add(ApiKey key, RequestCount count);
		void Update(ApiKey key, RequestCount count);
	}
}