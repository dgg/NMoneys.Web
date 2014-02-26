using System.Collections.Specialized;
using System.Net;
using NMoneys.Web.Api.v1.Infrastructure.Filters;
using NSubstitute;
using NUnit.Framework;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;

namespace Tests.Api.v1.Infrastructure.Filters
{
	[TestFixture]
	public class HttpsEnforcerTester
	{
		[Test]
		public void LocalRequest_SecureConnection_GoOnAsNormal()
		{
			var subject = new HttpsEnforcer();
			var request = Substitute.For<IHttpRequest>();
			request.IsSecureConnection.Returns(true);

			Assert.That(() => subject.Enforce(request), Throws.Nothing);
		}

		[Test]
		public void LocalRequest_InsecureConnection_403()
		{
			var subject = new HttpsEnforcer();
			var request = Substitute.For<IHttpRequest>();
			request.IsSecureConnection.Returns(false);

			Assert.That(() => subject.Enforce(request), Throws.InstanceOf<HttpError>()
				.With.Property("StatusCode").EqualTo(HttpStatusCode.Forbidden));
		}

		[Test]
		public void CloudRequest_SecureConnection_GoOnAsNormal()
		{
			var subject = new HttpsEnforcer();
			var request = Substitute.For<IHttpRequest>();
			request.Headers.Returns(new NameValueCollection{{"X-Forwarded-Proto", "https"}});

			Assert.That(() => subject.Enforce(request), Throws.Nothing);
		}

		[Test]
		public void CloudRequest_InsecureConnection_403()
		{
			var subject = new HttpsEnforcer();
			var request = Substitute.For<IHttpRequest>();
			request.Headers.Returns(new NameValueCollection { { "X-Forwarded-Proto", "http" } });

			Assert.That(() => subject.Enforce(request), Throws.InstanceOf<HttpError>()
				.With.Property("StatusCode").EqualTo(HttpStatusCode.Forbidden));
		}
	}
}