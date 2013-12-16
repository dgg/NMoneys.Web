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
		public void SecureConnection_GoOnAsNormal()
		{
			var subject = new HttpsEnforcer();
			var request = Substitute.For<IHttpRequest>();
			request.IsSecureConnection.Returns(true);

			Assert.That(() => subject.Enforce(request), Throws.Nothing);
		}

		[Test]
		public void InsecureConnection_403()
		{
			var subject = new HttpsEnforcer();
			var request = Substitute.For<IHttpRequest>();
			request.IsSecureConnection.Returns(false);

			Assert.That(() => subject.Enforce(request), Throws.InstanceOf<HttpError>()
				.With.Property("StatusCode").EqualTo(HttpStatusCode.Forbidden));
		}
	}
}