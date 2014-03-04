using System;

namespace NMoneys.Web.Api.v1.Infrastructure
{
	public interface IRequestCountRepository: IDisposable
	{
		RequestCount Get(ApiKey key);
		void Update(ApiKey key, RequestCount count);
		RequestCount Ensure(ApiKey key, Antlr.Runtime.Misc.Func<RequestCount> initialization);
	}
}