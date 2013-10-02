using System;
using System.Collections.Specialized;
using System.Linq;
using EasyHttp.Http;
using MongoDB.Bson;
using NMoneys;
using NMoneys.Web.Api.v1.Infrastructure;
using NSubstitute;
using ServiceStack.Configuration;
using ServiceStack.ServiceClient.Web;
using Tests.Api.Support;
using Currency = NMoneys.Web.Api.v1.Messages.Currency;

namespace Tests.Api.v1.Resources.Support
{
	internal static class TesterExtensions
	{
		public static void AuthenticateRequest(this TesterBase tester)
		{
			var verifier = Substitute.For<IKeyVerifier>();
			verifier.Verify(Arg.Any<ApiKey>()).Returns(true);
			tester.Replacing(verifier);
		}

		public static void SetupThrottling(this TesterBase tester, ushort numberOfRequests, TimeSpan period)
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

		internal static readonly string AServiceUrl = new Currency { IsoCode = CurrencyIsoCode.EUR }
				.ToUrl("GET");

		public static HttpResponse Get(this TesterBase tester, HttpClient client)
		{
			client.Request.AddExtraHeader(ApiKey.ParameterName, ObjectId.Empty);
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