using System.Net;
using EasyHttp.Http;
using MongoDB.Bson;
using NMoneys;
using NMoneys.Web.Api.v1.Infrastructure;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using Testing.Commons;
using Tests.Api.v1.Resources.Support;
using CurrencyMsg = NMoneys.Web.Api.v1.Messages.Currency;
using FormatMsg = NMoneys.Web.Api.v1.Messages.Format;

namespace Tests.Api.v1.Resources
{
	[TestFixture]
	public class CurrenciesTester : PerFixture
	{
		private static string any_key { get { return ObjectId.Empty.ToString(); } }

		[Test]
		public void Currency_UndefinedIsoCode_NotFound()
		{
			var client = new HttpClient(BaseUrl.ToString());
			this.DisableAuthentication().FullThrottle().DisableEnforcer();

			string qs = string.Format("?{0}={1}", ApiKey.ParameterName, any_key);
			string undefinedIsoCodeUrl = new CurrencyMsg { IsoCode = CurrencyIsoCode.EUR }
				.ToUrl("GET")
				.Replace("EUR", "NotAnIsoCode");

			HttpResponse response = client.Get(undefinedIsoCodeUrl + qs);
			Assert.That(response, Must.Not.Be.Ok(HttpStatusCode.NotFound));
		}

		[Test]
		public void FormatCurrency_UndefinedIsoCode_NotFound()
		{
			var client = new HttpClient(BaseUrl.ToString());
			this.DisableAuthentication().FullThrottle().DisableEnforcer();

			string qs = string.Format("?{0}={1}", ApiKey.ParameterName, any_key);
			string undefinedIsoCodeUrl = new FormatMsg { IsoCode = CurrencyIsoCode.EUR, Amount = 42 }
				.ToUrl("GET")
				.Replace("EUR", "NotAnIsoCode");

			HttpResponse response = client.Get(undefinedIsoCodeUrl + qs);
			Assert.That(response, Must.Not.Be.Ok(HttpStatusCode.NotFound));
		}

		[Test]
		public void AlternativeFormatCurrency_UndefinedIsoCode_NotFound()
		{
			var client = new HttpClient(BaseUrl.ToString());
			this.DisableAuthentication().FullThrottle().DisableEnforcer();

			string qs = string.Format("?{0}={1}", ApiKey.ParameterName, any_key);
			string undefinedIsoCodeUrl = new FormatMsg { IsoCode = CurrencyIsoCode.EUR }
				.ToUrl("POST")
				.Replace("EUR", "NotAnIsoCode");

			HttpResponse response = client.Post(undefinedIsoCodeUrl + qs, new { amount = 42 }, HttpContentTypes.ApplicationJson);
			Assert.That(response, Must.Not.Be.Ok(HttpStatusCode.NotFound));
		}
	}
}