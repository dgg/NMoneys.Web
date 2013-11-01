using System.Collections.Specialized;
using System.Net;
using EasyHttp.Http;
using NMoneys.Web.Api.v1.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using Testing.Commons;
using Tests.Api.Support;
using Tests.Api.v1.Resources.Support;

namespace Tests.Api.v1.Resources
{
	[TestFixture, Category("Integration")]
	public class AuthenticationTester : SingleHostPerFixture
	{
		[Test]
		public void MissingApiKey_NotAuthorized()
		{
			var verifier = Substitute.For<IKeyVerifier>();
			Replacing(verifier);

			HttpResponse response = this.Get();

			Assert.That(response, Must.Not.Be.Ok(HttpStatusCode.Unauthorized));
		}

		[Test]
		public void WrongApiKey_NotAuthorized()
		{
			var verifier = Substitute.For<IKeyVerifier>();
			verifier.Verify(Arg.Any<ApiKey>()).Returns(false);
			Replacing(verifier);

			HttpResponse response = this.Get(client =>
				client.Request.AddExtraHeader(ApiKey.ParameterName, ApiKey.EmptyParameterValue));

			Assert.That(response, Must.Not.Be.Ok(HttpStatusCode.Unauthorized));
		}

		[Test]
		public void CorrectApiKey_AsHeader_Authorized()
		{
			var verifier = Substitute.For<IKeyVerifier>();
			verifier.Verify(Arg.Any<ApiKey>()).Returns(true);
			Replacing(verifier);

			HttpResponse response = this.Get(client =>
				client.Request.AddExtraHeader(ApiKey.ParameterName, ApiKey.EmptyParameterValue));

			Assert.That(response, Must.Be.Ok());
		}

		[Test]
		public void ExistingApiKey_AsQuery_Authorized()
		{
			var verifier = Substitute.For<IKeyVerifier>();
			verifier.Verify(Arg.Any<ApiKey>()).Returns(true);
			Replacing(verifier);

			var response = this.Get(new NameValueCollection { { ApiKey.ParameterName, ApiKey.EmptyParameterValue } });

			Assert.That(response, Must.Be.Ok());
		}
	}
}
