using System.Net;
using EasyHttp.Http;
using NUnit.Framework;
using Testing.Commons;
using Testing.Commons.Time;
using Tests.Api.Support;
using Tests.Api.v1.Resources.Support;

namespace Tests.Api.v1.Resources
{
	[TestFixture, Category("Integration")]
	public class RequestThrottlingTester : SingleHostPerTest
	{
		[Test]
		public void LessRequestsThanLimit_Success()
		{
			this.AuthenticateRequest();
			this.SetupThrottling(3, 10.Seconds());

			var client = new HttpClient(BaseUrl.ToString());
			HttpResponse response = this.Get(client);
			Assert.That(response, Must.Be.Ok());

			response = this.Get(client);
			Assert.That(response, Must.Be.Ok());
		}

		[Test]
		public void AsManyRequestsAsLimit_Success()
		{
			this.AuthenticateRequest();
			this.SetupThrottling(2, 10.Seconds());

			var client = new HttpClient(BaseUrl.ToString());
			
			HttpResponse response = this.Get(client);
			Assert.That(response, Must.Be.Ok());

			response = this.Get(client);
			Assert.That(response, Must.Be.Ok());
		}

		[Test]
		public void MoreRequestsThanLimit_TooManyRequests()
		{
			this.AuthenticateRequest();
			this.SetupThrottling(2, 10.Seconds());

			var client = new HttpClient(BaseUrl.ToString());
			
			HttpResponse response = this.Get(client);
			Assert.That(response, Must.Be.Ok());
			response = this.Get(client);
			Assert.That(response, Must.Be.Ok());

			response = this.Get(client);
			Assert.That(response, Must.Not.Be.Ok(429));
		}

		[Test]
		public void MoreRequestsThanLimit_RetryHeaderWithPeriod()
		{
			this.AuthenticateRequest();
			this.SetupThrottling(2, 10.Seconds());

			var client = new HttpClient(BaseUrl.ToString());
			client.Request.Accept = HttpContentTypes.ApplicationJson;
			
			HttpResponse response = this.Get(client);
			Assert.That(response, Must.Be.Ok());
			response = this.Get(client);
			Assert.That(response, Must.Be.Ok());

			response = this.Get(client);
			Assert.That(response, Must.Have.Header("Retry-After", Is.StringContaining("10")));
		}

		[Test]
		public void MoreRequestsThanLimit_MessageWithThrottlingInformation()
		{
			this.AuthenticateRequest();
			this.SetupThrottling(2, 10.Seconds());

			var client = new HttpClient(BaseUrl.ToString());
			client.Request.Accept = HttpContentTypes.ApplicationJson;

			HttpResponse response = this.Get(client);
			Assert.That(response, Must.Be.Ok());
			response = this.Get(client);
			Assert.That(response, Must.Be.Ok());

			response = this.Get(client);

			Assert.That(response.DynamicBody.responseStatus.errorCode,
				Is.StringContaining("10").And.StringContaining("2"));
		}
	}
}
