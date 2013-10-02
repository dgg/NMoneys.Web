using ServiceStack.Common.Web;
using ServiceStack.Configuration;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Infrastructure.Filters
{
	public class RequestThrottler
	{
		public static readonly string ConfigurationKey = "throttling";

		public static void Handle(IHttpRequest request, IHttpResponse response, object dto)
		{
			var resources = request.TryResolve<IResourceManager>();
			ThrottlingConfiguration configuration = resources.Get(ConfigurationKey, ThrottlingConfiguration.Empty());
			var repository = request.TryResolve<IRequestCountRepository>();

			var key = ApiKey.ExtractFrom(request);
			if (!key.IsMissing && configuration.ThrottlingEnabled)
			{
				RequestCount count =repository.Get(key);
				if (count == null)
				{
					count = new RequestCount(configuration.Period);
					repository.Add(key, count);
					
				}
				else if (count.IsLessThan(configuration.NumberOfRequests))
				{
					repository.Update(key, count.Increase());
				}
				else
				{
					response.AddHeader("Retry-After", configuration.FormattedSeconds);
					throw new HttpError(429, configuration.ErrorMessage());
				}
			}
		}
	}
}