using System;
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
	}
}