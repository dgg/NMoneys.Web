using System.Collections.Specialized;
using System.Web;
using NMoneys.Web.Infrastructure.Web;
using NSubstitute;
using NUnit.Framework;
using ServiceStack.ServiceHost;

namespace Tests.Infrastructure.Web
{
	[TestFixture]
	public class SecureRequestSpecificationTester
	{
		#region api request

		[Test]
		public void IsSatisfiedBy_LocalSecureApiRequest_True()
		{
			var subject = new SecureRequestSpecification();
			var request = Substitute.For<IHttpRequest>();
			request.IsSecureConnection.Returns(true);

			Assert.That(subject.IsSatisfiedBy(request), Is.True);
		}

		[Test]
		public void IsSatisfiedBy_LocalInsecureApiRequest_False()
		{
			var subject = new SecureRequestSpecification();
			var request = Substitute.For<IHttpRequest>();
			request.IsSecureConnection.Returns(false);

			Assert.That(subject.IsSatisfiedBy(request), Is.False);
		}

		[Test]
		public void IsSatisfiedBy_CloudSecureApiRequest_True()
		{
			var subject = new SecureRequestSpecification();
			var request = Substitute.For<IHttpRequest>();
			request.Headers.Returns(new NameValueCollection {{"X-Forwarded-Proto", "https"}});

			Assert.That(subject.IsSatisfiedBy(request), Is.True);
		}

		[Test]
		public void IsSatisfiedBy_CloudInsecureApiRequest_False()
		{
			var subject = new SecureRequestSpecification();
			var request = Substitute.For<IHttpRequest>();
			request.Headers.Returns(new NameValueCollection {{"X-Forwarded-Proto", "http"}});

			Assert.That(subject.IsSatisfiedBy(request), Is.False);
		}

		#endregion

		#region web request

		[Test]
		public void IsSatisfiedBy_LocalSecureWebRequest_True()
		{
			var subject = new SecureRequestSpecification();
			var request = Substitute.For<HttpRequestBase>();
			request.IsSecureConnection.Returns(true);

			Assert.That(subject.IsSatisfiedBy(request), Is.True);
		}

		[Test]
		public void IsSatisfiedBy_LocalInsecureWebRequest_False()
		{
			var subject = new SecureRequestSpecification();
			var request = Substitute.For<HttpRequestBase>();
			request.IsSecureConnection.Returns(false);

			Assert.That(subject.IsSatisfiedBy(request), Is.False);
		}

		[Test]
		public void IsSatisfiedBy_CloudSecureWebRequest_True()
		{
			var subject = new SecureRequestSpecification();
			var request = Substitute.For<HttpRequestBase>();
			request.Headers.Returns(new NameValueCollection { { "X-Forwarded-Proto", "https" } });

			Assert.That(subject.IsSatisfiedBy(request), Is.True);
		}

		[Test]
		public void IsSatisfiedBy_CloudInsecureWebRequest_False()
		{
			var subject = new SecureRequestSpecification();
			var request = Substitute.For<HttpRequestBase>();
			request.Headers.Returns(new NameValueCollection { { "X-Forwarded-Proto", "http" } });

			Assert.That(subject.IsSatisfiedBy(request), Is.False);
		}

		#endregion

	}
}