using System;

namespace NMoneys.Web.Api.v1.Infrastructure
{
	public class RequestCount
	{
		public RequestCount(TimeSpan period)
		{
			Count = 1;
			Expiration = DateTimeOffset.UtcNow.Add(period);
		}

		public ushort Count { get; private set; }

		public DateTimeOffset Expiration { get; private set; }

		public bool IsLessThan(ushort maxNumberOfRequests)
		{
			return Count < maxNumberOfRequests;
		}

		public RequestCount Increase()
		{
			Count++;
			return this;
		}
	}
}