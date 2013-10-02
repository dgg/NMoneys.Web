using System;
using System.Globalization;
using ServiceStack.Configuration;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Infrastructure.Filters
{
	public class RequestRates
	{
		public static void Handle(IHttpRequest request, IHttpResponse response, object dto)
		{
			ApiKey apiKey = ApiKey.ExtractFrom(request);
			if (!apiKey.IsMissing)
			{
				var resources = request.TryResolve<IResourceManager>();
				ThrottlingConfiguration configuration = resources.Get(ThrottlingConfiguration.Key, ThrottlingConfiguration.Empty());

				var repository = request.TryResolve<IRequestCountRepository>();
				RequestCount count = repository.Get(apiKey);

				addLimitHeader(response, configuration);
				addRemainingHeader(response, configuration, count);
				addResetHeader(response, configuration, count);
			}
		}

		private static void addResetHeader(IHttpResponse response, ThrottlingConfiguration configuration, RequestCount count)
		{
			string numberOfSecondsLeftInPeriod = count!= null ?
				count.Remaining(DateTimeOffset.UtcNow).ToString(CultureInfo.InvariantCulture) :
				configuration.FormattedSeconds;

			response.AddHeader("X-Rate-Limit-Reset", numberOfSecondsLeftInPeriod);
		}

		private static void addRemainingHeader(IHttpResponse response, ThrottlingConfiguration configuration, RequestCount count)
		{
			string numberOfRequestLeftInPeriod = count != null ?
				count.Remaining(configuration.NumberOfRequests).ToString(CultureInfo.InvariantCulture):
				configuration.FormattedRequests;
			response.AddHeader("X-Rate-Limit-Remaining", numberOfRequestLeftInPeriod);
		}

		private static void addLimitHeader(IHttpResponse response, ThrottlingConfiguration configuration)
		{
			string numberOfRequestsAllowedInPeriod = configuration.FormattedRequests;
			response.AddHeader("X-Rate-Limit-Limit", numberOfRequestsAllowedInPeriod);
		}
	}
}