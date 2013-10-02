using System;
using System.Net;
using EasyHttp.Http;
using MongoDB.Bson;
using NMoneys;
using NMoneys.Web.Api.v1.Infrastructure;
using NMoneys.Web.Api.v1.Infrastructure.Filters;
using NSubstitute;
using NUnit.Framework;
using ServiceStack.Configuration;
using ServiceStack.ServiceClient.Web;
using Tests.Api.Support;
using CurrencyMsg = NMoneys.Web.Api.v1.Messages.Currency;

namespace Tests.Api.v1.Resources
{
	[TestFixture, Category("Integration")]
	public partial class CurrenciesTester
	{
		[TestFixture, Category("Integration")]
		public class RequestThrottling : SingleHostPerTest
		{
			[Test]
			public void LessRequestsThanLimit_Success()
			{
				var threeEveryTenSeconds = new ThrottlingConfiguration
				{
					NumberOfRequests = 3,
					Period = TimeSpan.FromSeconds(10)
				};

				authenticateRequest();
				configureThrottling(threeEveryTenSeconds);

				var client = new HttpClient(BaseUrl.ToString());
				client.Request.AddExtraHeader(ApiKey.ParameterName, ObjectId.Empty);
				var msg = new CurrencyMsg { IsoCode = CurrencyIsoCode.EUR };

				HttpResponse response = client.Get(msg.ToUrl("GET"));
				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

				response = client.Get(msg.ToUrl("GET"));
				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			}

			private void configureThrottling(ThrottlingConfiguration threeEveryTenSeconds)
			{
				var configuration = Substitute.For<IResourceManager>();
				configuration.Get(RequestThrottler.ConfigurationKey, Arg.Any<ThrottlingConfiguration>()).Returns(threeEveryTenSeconds);
				Replacing(configuration);
			}

			private void authenticateRequest()
			{
				var verifier = Substitute.For<IKeyVerifier>();
				verifier.Verify(Arg.Any<ApiKey>()).Returns(true);
				Replacing(verifier);
			}

			[Test]
			public void AsManyRequestsAsLimit_Success()
			{
				var twoEveryTenSeconds = new ThrottlingConfiguration
				{
					NumberOfRequests = 2,
					Period = TimeSpan.FromSeconds(10)
				};

				authenticateRequest();
				configureThrottling(twoEveryTenSeconds);

				var client = new HttpClient(BaseUrl.ToString());
				client.Request.AddExtraHeader(ApiKey.ParameterName, ObjectId.Empty);
				var msg = new CurrencyMsg { IsoCode = CurrencyIsoCode.EUR };

				HttpResponse response = client.Get(msg.ToUrl("GET"));
				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

				response = client.Get(msg.ToUrl("GET"));
				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			}

			[Test]
			public void MoreRequestsThanLimit_TooManyRequests()
			{
				var twoEveryTenSeconds = new ThrottlingConfiguration
				{
					NumberOfRequests = 2,
					Period = TimeSpan.FromSeconds(10)
				};

				authenticateRequest();
				configureThrottling(twoEveryTenSeconds);

				var client = new HttpClient(BaseUrl.ToString());
				client.Request.AddExtraHeader(ApiKey.ParameterName, ObjectId.Empty);
				var msg = new CurrencyMsg { IsoCode = CurrencyIsoCode.EUR };

				HttpResponse response = client.Get(msg.ToUrl("GET"));
				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
				response = client.Get(msg.ToUrl("GET"));
				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

				response = client.Get(msg.ToUrl("GET"));
				Assert.That(response.StatusCode, Is.EqualTo((HttpStatusCode)429));
			}

			[Test]
			public void MoreRequestsThanLimit_RetryHeaderAndMessage()
			{
				var twoEveryTenSeconds = new ThrottlingConfiguration
				{
					NumberOfRequests = 2,
					Period = TimeSpan.FromSeconds(10)
				};

				authenticateRequest();
				configureThrottling(twoEveryTenSeconds);

				var client = new HttpClient(BaseUrl.ToString());
				client.Request.Accept = HttpContentTypes.ApplicationJson;
				client.Request.AddExtraHeader(ApiKey.ParameterName, ObjectId.Empty);
				var msg = new CurrencyMsg { IsoCode = CurrencyIsoCode.EUR };

				HttpResponse response = client.Get(msg.ToUrl("GET"));
				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
				response = client.Get(msg.ToUrl("GET"));
				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

				response = client.Get(msg.ToUrl("GET"));
				Assert.That(response.RawHeaders["Retry-After"], Is.StringContaining("10"));
				Assert.That(response.DynamicBody.responseStatus.errorCode,
					Is.StringContaining("10").And.StringContaining("2"));
			}
		}
	}
}
