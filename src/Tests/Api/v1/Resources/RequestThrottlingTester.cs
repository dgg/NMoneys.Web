using System.Collections.Generic;
using System.Reflection;
using EasyHttp.Http;
using NMoneys.Web.Api.v1.Infrastructure;
using NUnit.Framework;
using ServiceStack.WebHost.Endpoints;
using Testing.Commons;
using Testing.Commons.ServiceStack.v3;
using Testing.Commons.Time;
using Tests.Api.v1.Resources.Support;

namespace Tests.Api.v1.Resources
{
	public abstract class PerTest : SingleHostPerTest
	{
		protected override string ServiceName { get { return HostBootstrapper.ServiceName; } }

		protected override IEnumerable<Assembly> AssembliesWithServices { get { return new[] { HostBootstrapper.ServiceContainer }; } }

		private HostBootstrapper _bootstrapper;
		protected override void Boootstrap(IAppHost arg)
		{
			_bootstrapper = new HostBootstrapper();
			_bootstrapper.BootstrapAll(arg);
			//	.BootstrapAll(this);
		}

		protected override void OnHostDispose(bool disposing)
		{
			base.OnHostDispose(disposing);
			_bootstrapper.Dispose();
		}
	}

	public abstract class PerFixture : SingleHostPerFixture
	{
		protected override string ServiceName { get { return HostBootstrapper.ServiceName; } }

		protected override IEnumerable<Assembly> AssembliesWithServices { get { return new[] { HostBootstrapper.ServiceContainer }; } }

		private HostBootstrapper _bootstrapper;
		protected override void Boootstrap(IAppHost arg)
		{
			_bootstrapper = new HostBootstrapper();
			_bootstrapper.BootstrapAll(arg);
			//	.BootstrapAll(this);
		}

		protected override void OnHostDispose(bool disposing)
		{
			base.OnHostDispose(disposing);
			_bootstrapper.Dispose();
		}
	}

	[TestFixture, Category("Integration")]
	public class RequestThrottlingTester : PerTest
	{
		[Test]
		public void LessRequestsThanLimit_Success()
		{
			this.Throttle(3, 10.Seconds());

			var client = new HttpClient(BaseUrl.ToString());
			client.Request.AddExtraHeader(ApiKey.ParameterName, ApiKey.EmptyParameterValue);
			this.DisableEnforcer().DisableAuthentication();

			HttpResponse response = this.Get(client);
			Assert.That(response, Must.Be.Ok());

			response = this.Get(client);
			Assert.That(response, Must.Be.Ok());
		}

		[Test]
		public void AsManyRequestsAsLimit_Success()
		{
			this.Throttle(2, 10.Seconds());

			var client = new HttpClient(BaseUrl.ToString());
			client.Request.AddExtraHeader(ApiKey.ParameterName, ApiKey.EmptyParameterValue);
			this.DisableEnforcer().DisableAuthentication();

			HttpResponse response = this.Get(client);
			Assert.That(response, Must.Be.Ok());

			response = this.Get(client);
			Assert.That(response, Must.Be.Ok());
		}

		[Test]
		public void MoreRequestsThanLimit_TooManyRequests()
		{
			this.Throttle(2, 10.Seconds());

			var client = new HttpClient(BaseUrl.ToString());
			client.Request.AddExtraHeader(ApiKey.ParameterName, ApiKey.EmptyParameterValue);
			this.DisableEnforcer().DisableAuthentication();

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
			this.Throttle(2, 10.Seconds());

			var client = new HttpClient(BaseUrl.ToString());
			client.Request.AddExtraHeader(ApiKey.ParameterName, ApiKey.EmptyParameterValue);
			this.DisableEnforcer().DisableAuthentication();

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
			this.Throttle(2, 10.Seconds());

			var client = new HttpClient(BaseUrl.ToString());
			client.Request.AddExtraHeader(ApiKey.ParameterName, ApiKey.EmptyParameterValue);
			this.DisableEnforcer().DisableAuthentication();
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
