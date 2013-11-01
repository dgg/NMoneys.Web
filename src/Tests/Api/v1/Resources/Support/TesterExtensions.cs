using System;
using System.Collections.Specialized;
using System.Linq;
using EasyHttp.Http;
using MongoDB.Bson;
using NMoneys;
using NMoneys.Web.Api.v1.Infrastructure;
using NMoneys.Web.Api.v1.Infrastructure.Filters;
using NSubstitute;
using ServiceStack.Configuration;
using ServiceStack.ServiceClient.Web;
using Tests.Api.Support;
using Currency = NMoneys.Web.Api.v1.Messages.Currency;

namespace Tests.Api.v1.Resources.Support
{
	internal static class TesterExtensions
	{
		public static void DisableAuthentication(this TesterBase tester)
		{
			var authenticator = Substitute.For<IApiAuthenticator>();
			tester.Replacing(authenticator);
		}

		public static void FullThrottle(this TesterBase tester)
		{
			var throttler = Substitute.For<IRequestThrottler>();
			tester.Replacing(throttler);
		}

		public static void Throttle(this TesterBase tester, ushort numberOfRequests, TimeSpan period)
		{
			var configuration = new ThrottlingConfiguration
			{
				NumberOfRequests = numberOfRequests,
				Period = period
			};

			var manager = Substitute.For<IResourceManager>();
			manager.Get(ThrottlingConfiguration.Key, Arg.Any<ThrottlingConfiguration>())
				.Returns(configuration);
			tester.Replacing(manager);
		}

		public static IRequestCountRepository SetupThrottling(this TesterBase tester, ushort numberOfRequests, TimeSpan period, RequestCount count = null)
		{
			var configuration = new ThrottlingConfiguration
			{
				NumberOfRequests = numberOfRequests,
				Period = period
			};

			var manager = Substitute.For<IResourceManager>();
			manager.Get(ThrottlingConfiguration.Key, Arg.Any<ThrottlingConfiguration>())
				.Returns(configuration);
			tester.Replacing(manager);

			var repository = Substitute.For<IRequestCountRepository>();
			if (count != null)
			{
				repository.Get(Arg.Any<ApiKey>()).Returns(count);
			}
			tester.Replacing(repository);
			return repository;
		}

		internal static readonly string AServiceUrl = new Currency { IsoCode = CurrencyIsoCode.EUR }
				.ToUrl("GET");

		public static HttpResponse Get(this TesterBase tester, HttpClient client)
		{
			HttpResponse response = client.Get(AServiceUrl);
			return response;
		}

		public static HttpResponse Get(this TesterBase tester, Action<HttpClient> setup = null)
		{
			var client = new HttpClient(tester.BaseUrl.ToString());
			if (setup != null) setup(client);
			HttpResponse response = client.Get(AServiceUrl);
			return response;
		}

		public static HttpResponse Get(this TesterBase tester, NameValueCollection query)
		{
			var client = new HttpClient(tester.BaseUrl.ToString());
			
			string qs = string.Concat("?",
				string.Join("&", query.AllKeys.Select(k => k + "=" + query[k])));
			HttpResponse response = client.Get(AServiceUrl + qs);
			return response;
		}
	}
}