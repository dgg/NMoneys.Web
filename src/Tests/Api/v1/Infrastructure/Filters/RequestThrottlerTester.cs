using System;
using System.Collections.Specialized;
using MongoDB.Bson;
using NMoneys.Web.Api.v1.Infrastructure;
using NMoneys.Web.Api.v1.Infrastructure.Filters;
using NSubstitute;
using NUnit.Framework;
using ServiceStack.Common.Web;
using ServiceStack.Configuration;
using ServiceStack.ServiceHost;

namespace Tests.Api.v1.Infrastructure.Filters
{
	[TestFixture]
	public class RequestThrottlerTester
	{
		[Test]
		public void LessRequestsThanLimit_NoException()
		{
			var request = Substitute.For<IHttpRequest>();
			var response = Substitute.For<IHttpResponse>();

			authenticate(request);
			setupRepository(request);
			setupConfiguration(request, 3, TimeSpan.FromSeconds(30));

			Assert.That(() => RequestThrottler.Handle(request, response, null), Throws.Nothing);
			Assert.That(() => RequestThrottler.Handle(request, response, null), Throws.Nothing);

		}
		
		[Test]
		public void AsManyRequestsAsLimit_NoException()
		{
			var request = Substitute.For<IHttpRequest>();
			var response = Substitute.For<IHttpResponse>();

			authenticate(request);
			setupRepository(request);
			setupConfiguration(request, 2, TimeSpan.FromSeconds(30));

			Assert.That(() => RequestThrottler.Handle(request, response, null), Throws.Nothing);
			Assert.That(() => RequestThrottler.Handle(request, response, null), Throws.Nothing);
		}

		[Test]
		public void MoreRequestsThanLimit_Exception()
		{
			var request = Substitute.For<IHttpRequest>();
			var response = Substitute.For<IHttpResponse>();

			authenticate(request);
			setupRepository(request);
			setupConfiguration(request, 2, TimeSpan.FromSeconds(30));

			Assert.That(() => RequestThrottler.Handle(request, response, null), Throws.Nothing);
			Assert.That(() => RequestThrottler.Handle(request, response, null), Throws.Nothing);
			Assert.That(() => RequestThrottler.Handle(request, response, null),
				Throws.InstanceOf<HttpError>());
		}

		private void authenticate(IHttpRequest request)
		{
			var headers = new NameValueCollection
			{
				{ ApiKey.ParameterName, ObjectId.Empty.ToString() }
			};
			request.Headers.Returns(headers);
		}

		private void setupRepository(IHttpRequest request)
		{
			request.TryResolve<IRequestCountRepository>().Returns(new RequestCountRepository());
		}

		private void setupConfiguration(IHttpRequest request, ushort numberOfRequests, TimeSpan period)
		{
			var configuration = Substitute.For<IResourceManager>();

			request.TryResolve<IResourceManager>().Returns(configuration);
			configuration.Get(RequestThrottler.ConfigurationKey, Arg.Any<ThrottlingConfiguration>()).Returns(
				new ThrottlingConfiguration
				{
					NumberOfRequests = numberOfRequests,
					Period = period
				});
		}
	}
}