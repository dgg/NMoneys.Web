using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using EasyHttp.Http;
using MongoDB.Bson;
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
	public class AuthenticationTester : SingleHostPerFixture
	{
		private static string any_key { get { return ObjectId.Empty.ToString(); } }

		[Test]
		public void MissingApiKey_NotAuthorized()
		{
			var verifier = Substitute.For<IKeyVerifier>();
			Replacing(verifier);

			HttpResponse response = get();

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
		}

		[Test]
		public void WrongApiKey_NotAuthorized()
		{
			var verifier = Substitute.For<IKeyVerifier>();
			verifier.Verify(Arg.Any<ApiKey>()).Returns(false);
			Replacing(verifier);

			HttpResponse response = get(client =>
				client.Request.AddExtraHeader(ApiKey.ParameterName, any_key));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
		}

		[Test]
		public void CorrectApiKey_AsHeader_Authorized()
		{
			var verifier = Substitute.For<IKeyVerifier>();
			verifier.Verify(Arg.Any<ApiKey>()).Returns(true);
			Replacing(verifier);

			HttpResponse response = get(client =>
				client.Request.AddExtraHeader(ApiKey.ParameterName, any_key));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void ExistingApiKey_AsQuery_Authorized()
		{
			var verifier = Substitute.For<IKeyVerifier>();
			verifier.Verify(Arg.Any<ApiKey>()).Returns(true);
			Replacing(verifier);

			var response = get(new NameValueCollection { { ApiKey.ParameterName, any_key } });

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		public HttpResponse get(Action<HttpClient> setup = null)
		{
			var client = new HttpClient(BaseUrl.ToString());
			if (setup != null) setup(client);
			var msg = new CurrencyMsg { IsoCode = CurrencyIsoCode.EUR };
			HttpResponse response = client.Get(msg.ToUrl("GET"));
			return response;
		}

		public HttpResponse get(NameValueCollection query)
		{
			var client = new HttpClient(BaseUrl.ToString());
			var msg = new CurrencyMsg { IsoCode = CurrencyIsoCode.EUR };

			string qs = string.Concat("?",
				string.Join("&", query.AllKeys.Select(k => k + "=" + query[k])));
			HttpResponse response = client.Get(msg.ToUrl("GET") + qs);
			return response;
		}
	}
}
