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
			return Count <= maxNumberOfRequests;
		}

		public ushort Remaining(ushort maxNumberOfRequests)
		{
			return (ushort)(maxNumberOfRequests - Count);
		}

		public ushort Remaining(DateTimeOffset now)
		{
			if (now.Offset!= TimeSpan.Zero) throw new NotSupportedException("only UTC dates are allowed");
			return (ushort)(Expiration-now).TotalSeconds;
		}

		public RequestCount Increase()
		{
			Count++;
			return this;
		}
	}
}