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
using Testing.Commons.Time;

namespace Tests.Api.v1.Infrastructure.Filters
{
	[TestFixture]
	public class RequestThrottlerTester
	{
		[Test]
		public void Throttle_LessRequestsThanLimit_NoException()
		{
			var request = Substitute.For<IHttpRequest>();
			var response = Substitute.For<IHttpResponse>();

			authenticate(request);
			setupRepository(request);
			setupConfiguration(request, 3, 30.Seconds());

			var subject = new RequestThrottler();

			Assert.That(() => subject.Throttle(request, response), Throws.Nothing);
			Assert.That(() => subject.Throttle(request, response), Throws.Nothing);
		}
		
		[Test]
		public void Throttle_AsManyRequestsAsLimit_NoException()
		{
			var request = Substitute.For<IHttpRequest>();
			var response = Substitute.For<IHttpResponse>();

			authenticate(request);
			setupRepository(request);
			setupConfiguration(request, 2, 30.Seconds());

			var subject = new RequestThrottler();

			Assert.That(() => subject.Throttle(request, response), Throws.Nothing);
			Assert.That(() => subject.Throttle(request, response), Throws.Nothing);
		}

		[Test]
		public void Throttle_MoreRequestsThanLimit_Exception()
		{
			var request = Substitute.For<IHttpRequest>();
			var response = Substitute.For<IHttpResponse>();

			authenticate(request);
			setupRepository(request);
			setupConfiguration(request, 2, 30.Seconds());

			var subject = new RequestThrottler();

			Assert.That(() => subject.Throttle(request, response), Throws.Nothing);
			Assert.That(() => subject.Throttle(request, response), Throws.Nothing);
			Assert.That(() => subject.Throttle(request, response),
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
			configuration.Get(ThrottlingConfiguration.Key, Arg.Any<ThrottlingConfiguration>()).Returns(
				new ThrottlingConfiguration
				{
					NumberOfRequests = numberOfRequests,
					Period = period
				});
		}
	}
}