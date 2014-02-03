using System;
using System.Threading;
using System.Threading.Tasks;
using EasyHttp.Http;
using NMoneys.Web.Api.v1.Infrastructure;
using NUnit.Framework;
using Testing.Commons;
using Testing.Commons.Time;
using Tests.Api.v1.Resources.Support;

namespace Tests.Api.v1.Resources
{
	[TestFixture, Category("Integration")]
	public class RatesAndLimitsTester : PerFixture
	{
		[Test]
		public void LimitHeader_AnyNumberOfRequests_AsPerThrottlingConfiguration()
		{
			this.SetupThrottling(3, 10.Seconds());

			var client = new HttpClient(BaseUrl.ToString());
			client.Request.AddExtraHeader(ApiKey.ParameterName, ApiKey.EmptyParameterValue);
			this.DisableEnforcer().DisableAuthentication().FullThrottle();

			HttpResponse response = this.Get(client);
			Assert.That(response, Must.Have.LimitHeader(Is.EqualTo("3")));

			response = this.Get(client);
			Assert.That(response, Must.Have.LimitHeader(Is.EqualTo("3")));
		}

		[Test]
		public void RemainingHeader_TotalMinusRepositoryCount()
		{
			var two = new RequestCount(TimeSpan.Zero).Increase();
			this.SetupThrottling(3, 10.Seconds(), two);

			var client = new HttpClient(BaseUrl.ToString());
			client.Request.AddExtraHeader(ApiKey.ParameterName, ApiKey.EmptyParameterValue);
			this.DisableEnforcer().DisableAuthentication().FullThrottle();

			HttpResponse response = this.Get(client);
			Assert.That(response, Must.Have.RemainingHeader(Is.EqualTo("1")));
		}

		[Test]
		public void ResetHeader_LessTimeOrEqualThanThrottlingPeriod()
		{
			var sixSecondsLeft = new RequestCount(5.Seconds());
			this.SetupThrottling(3, 10.Seconds(), sixSecondsLeft);

			var client = new HttpClient(BaseUrl.ToString());
			client.Request.AddExtraHeader(ApiKey.ParameterName, ApiKey.EmptyParameterValue);
			this.DisableEnforcer().DisableAuthentication().FullThrottle();

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