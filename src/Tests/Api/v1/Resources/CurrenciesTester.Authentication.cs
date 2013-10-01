using System.Net;
using EasyHttp.Http;
using NMoneys;
using NMoneys.Web.Api.v1.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using Tests.Api.Support;
using CurrencyMsg = NMoneys.Web.Api.v1.Messages.Currency;

namespace Tests.Api.v1.Resources
{
	[TestFixture, Category("Integration")]
	public partial class CurrenciesTester
	{
		[TestFixture, Category("Integration")]
		public class Authentication : SingleHostPerFixture
		{
			[Test]
			public void MissingApiKey_NotAuthorized()
			{
				var verifier = Substitute.For<IKeyVerifier>();
				Replacing(verifier);

				var client = new HttpClient(BaseUrl.ToString());
				var msg = new CurrencyMsg { IsoCode = CurrencyIsoCode.EUR };
				HttpResponse response = client.Get(msg.ToUrl("GET"));

				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
			}

			[Test]
			public void WrongApiKey_NotAuthorized()
			{
				var verifier = Substitute.For<IKeyVerifier>();
				verifier.Verify(Arg.Any<ApiKey>()).Returns(false);
				Replacing(verifier);

				var client = new HttpClient(BaseUrl.ToString());
				client.Request.AddExtraHeader(ApiKey.ParameterName, "any_key");

				var msg = new CurrencyMsg { IsoCode = CurrencyIsoCode.EUR };
				HttpResponse response = client.Get(msg.ToUrl("GET"));

				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
			}

			[Test]
			public void ExistingApiKey_AsHeader_Authorized()
			{
				var verifier = Substitute.For<IKeyVerifier>();
				verifier.Verify(Arg.Any<ApiKey>()).Returns(true);
				Replacing(verifier);

				var client = new HttpClient(BaseUrl.ToString());
				client.Request.AddExtraHeader(ApiKey.ParameterName, "any_key");

				var msg = new CurrencyMsg { IsoCode = CurrencyIsoCode.EUR };
				HttpResponse response = client.Get(msg.ToUrl("GET"));

				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			}

			[Test]
			public void ExistingApiKey_AsQuery_Authorized()
			{
				var msg = new CurrencyMsg { IsoCode = CurrencyIsoCode.EUR };

				var verifier = Substitute.For<IKeyVerifier>();
				verifier.Verify(Arg.Any<ApiKey>()).Returns(true);
				Replacing(verifier);

				var client = new HttpClient(BaseUrl.ToString());
				string urlWithApiKey = msg.ToUrl("GET") + "?api_key=any_key";
				HttpResponse response = client.Get(urlWithApiKey);

				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			}
		}
	}
}
