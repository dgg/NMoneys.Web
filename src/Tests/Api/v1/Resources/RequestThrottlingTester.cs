using System;
using System.Net;
using EasyHttp.Http;
using NUnit.Framework;
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
			this.SetupThrottling(3, TimeSpan.FromSeconds(10));

			var client = new HttpClient(BaseUrl.ToString());
			HttpResponse response = this.Get(client);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

			response = this.Get(client);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void AsManyRequestsAsLimit_Success()
		{
			this.AuthenticateRequest();
			this.SetupThrottling(2, TimeSpan.FromSeconds(10));

			var client = new HttpClient(BaseUrl.ToString());
			
			HttpResponse response = this.Get(client);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

			response = this.Get(client);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void MoreRequestsThanLimit_TooManyRequests()
		{
			this.AuthenticateRequest();
			this.SetupThrottling(2, TimeSpan.FromSeconds(10));

			var client = new HttpClient(BaseUrl.ToString());
			
			HttpResponse response = this.Get(client);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			response = this.Get(client);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

			response = this.Get(client);
			Assert.That(response.StatusCode, Is.EqualTo((HttpStatusCode)429));
		}

		[Test]
		public void MoreRequestsThanLimit_RetryHeaderAndMessage()
		{
			this.AuthenticateRequest();
			this.SetupThrottling(2, TimeSpan.FromSeconds(10));

			var client = new HttpClient(BaseUrl.ToString());
			client.Request.Accept = HttpContentTypes.ApplicationJson;
			
			HttpResponse response = this.Get(client);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			response = this.Get(client);
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

			response = this.Get(client);
			Assert.That(response.RawHeaders["Retry-After"], Is.StringContaining("10"));
			Assert.That(response.DynamicBody.responseStatus.errorCode,
				Is.StringContaining("10").And.StringContaining("2"));
		}
	}
}
