using System;
using System.Threading;
using System.Threading.Tasks;
using EasyHttp.Http;
using NUnit.Framework;
using Tests.Api.Support;
using Tests.Api.v1.Resources.Support;

namespace Tests.Api.v1.Resources
{
	[TestFixture, Category("Integration")]
	public class RatesAndLimitsTester : SingleHostPerTest
	{
		[Test]
		public void LimitHeader_AnyNumberOfRequests_AsPerThrottlingConfiguration()
		{
			this.AuthenticateRequest();
			this.SetupThrottling(3, TimeSpan.FromSeconds(10));

			var client = new HttpClient(BaseUrl.ToString());

			HttpResponse response = this.Get(client);
			Assert.That(response.RawHeaders["X-Rate-Limit-Limit"], Is.EqualTo("3"));

			response = this.Get(client);
			Assert.That(response.RawHeaders["X-Rate-Limit-Limit"], Is.EqualTo("3"));
		}

		[Test]
		public void RemainingHeader_MaxNumberMinusRequestsInPeriod()
		{
			this.AuthenticateRequest();
			this.SetupThrottling(3, TimeSpan.FromSeconds(10));

			var client = new HttpClient(BaseUrl.ToString());

			HttpResponse response = this.Get(client);
			Assert.That(response.RawHeaders["X-Rate-Limit-Remaining"], Is.EqualTo("2"));

			response = this.Get(client);
			Assert.That(response.RawHeaders["X-Rate-Limit-Remaining"], Is.EqualTo("1"));

			response = this.Get(client);
			Assert.That(response.RawHeaders["X-Rate-Limit-Remaining"], Is.EqualTo("0"));

			response = this.Get(client);
			Assert.That(response.RawHeaders["Retry-After"], Is.EqualTo("10"));
		}

		[Test]
		public void ResetHeader_LessTimeOrEqualThanThrottlingPeriod()
		{
			this.AuthenticateRequest();
			this.SetupThrottling(3, TimeSpan.FromSeconds(10));

			var client = new HttpClient(BaseUrl.ToString());

			HttpResponse response = this.Get(client);
			Assert.That(remainingSecondsInPeriod(response), Is.LessThanOrEqualTo(10));

			Task t = Task.Factory
				.StartNew(() => Thread.Sleep(TimeSpan.FromSeconds(1)), TaskCreationOptions.LongRunning)
				.ContinueWith(_ =>
				{
					response = this.Get(client);
					Assert.That(remainingSecondsInPeriod(response), Is.LessThan(10));
				});
			
			t.Wait();
		}

		private ushort remainingSecondsInPeriod(HttpResponse response)
		{
			return ushort.Parse(response.RawHeaders["X-Rate-Limit-Reset"]);
		}
	}
}