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
			var configuration = Substitute.For<IResourceManager>();

			request.TryResolve<IResourceManager>().Returns(configuration);
			authenticate(request);
			configuration.Get(RequestThrottler.ConfigurationKey, Arg.Any<ThrottlingConfiguration>()).Returns(
				new ThrottlingConfiguration
				{
					NumberOfRequests = 3,
					Period = TimeSpan.FromSeconds(30)
				});

			using (var subject = new RequestThrottler())
			{
				// no assertion due to using of delegates and IDisposable
				subject.Handle(request, response, null);
				subject.Handle(request, response, null);
			}
		}

		private void authenticate(IHttpRequest request)
		{
			var headers = new NameValueCollection
			{
				{ ApiKey.ParameterName, ObjectId.Empty.ToString() }
			};
			request.Headers.Returns(headers);
		}

		[Test]
		public void AsManyRequestsAsLimit_NoException()
		{
			var request = Substitute.For<IHttpRequest>();
			var response = Substitute.For<IHttpResponse>();
			var configuration = Substitute.For<IResourceManager>();

			request.TryResolve<IResourceManager>().Returns(configuration);
			authenticate(request);
			configuration.Get(RequestThrottler.ConfigurationKey, Arg.Any<ThrottlingConfiguration>()).Returns(
				new ThrottlingConfiguration
				{
					NumberOfRequests = 2,
					Period = TimeSpan.FromSeconds(30)
				});

			using (var subject = new RequestThrottler())
			{
				// no assertion due to using of delegates and IDisposable
				subject.Handle(request, response, null);
				subject.Handle(request, response, null);
			}
		}

		[Test]
		public void MoreRequestsThanLimit_Exception()
		{
			var request = Substitute.For<IHttpRequest>();
			var response = Substitute.For<IHttpResponse>();
			var configuration = Substitute.For<IResourceManager>();

			request.TryResolve<IResourceManager>().Returns(configuration);
			authenticate(request);
			configuration.Get(RequestThrottler.ConfigurationKey, Arg.Any<ThrottlingConfiguration>()).Returns(
				new ThrottlingConfiguration
				{
					NumberOfRequests = 2,
					Period = TimeSpan.FromSeconds(30)
				});

			using (var subject = new RequestThrottler())
			{
				// no assertion due to using of delegates and IDisposable
				subject.Handle(request, response, null);
				subject.Handle(request, response, null);

				try
				{
					subject.Handle(request, response, null);
					Assert.Fail("An exception should be thrown");
				}
				catch (HttpError) { }
			}
		}
	}
}