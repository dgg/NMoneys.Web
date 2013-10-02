using System;
using System.Threading;
using System.Threading.Tasks;
using EasyHttp.Http;
using NUnit.Framework;
using Testing.Commons;
using Testing.Commons.Time;
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
			this.SetupThrottling(3, 10.Seconds());

			var client = new HttpClient(BaseUrl.ToString());

			HttpResponse response = this.Get(client);
			Assert.That(response, Must.Have.LimitHeader(Is.EqualTo("3")));

			response = this.Get(client);
			Assert.That(response, Must.Have.LimitHeader(Is.EqualTo("3")));
		}

		[Test]
		public void RemainingHeader_MaxNumberMinusRequestsInPeriod()
		{
			this.AuthenticateRequest();
			this.SetupThrottling(3, 10.Seconds());

			var client = new HttpClient(BaseUrl.ToString());

			HttpResponse response = this.Get(client);
			Assert.That(response, Must.Have.RemainingHeader(Is.EqualTo("2")));

			response = this.Get(client);
			Assert.That(response, Must.Have.RemainingHeader(Is.EqualTo("1")));

			response = this.Get(client);
			Assert.That(response, Must.Have.RemainingHeader(Is.EqualTo("0")));

			response = this.Get(client);
			Assert.That(response, Must.Have.RetryHeader(Is.EqualTo("10")));
		}

		[Test]
		public void ResetHeader_LessTimeOrEqualThanThrottlingPeriod()
		{
			this.AuthenticateRequest();
			this.SetupThrottling(3, 10.Seconds());

			var client = new HttpClient(BaseUrl.ToString());

			HttpResponse response = this.Get(client);
			Assert.That(response, Must.Have.ResetHeader(
				Is.LessThanOrEqualTo("10").Using(RemainingSeconds.InPeriod)));

			Task t = Task.Factory
				.StartNew(() => Thread.Sleep(TimeSpan.FromSeconds(1)), TaskCreationOptions.LongRunning)
				.ContinueWith(_ =>
				{
					response = this.Get(client);
					Assert.That(response, Must.Have.ResetHeader(
						Is.LessThan("10").Using(RemainingSeconds.InPeriod)));
				});
			
			t.Wait();
		}
	}
}