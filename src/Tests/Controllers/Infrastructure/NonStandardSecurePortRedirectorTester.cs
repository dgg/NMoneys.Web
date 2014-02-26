using System;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using NMoneys.Web.Controllers.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using Testing.Commons;
using Testing.Commons.NUnit.Constraints;

namespace Tests.Controllers.Infrastructure
{
	[TestFixture]
	public class NonStandardSecurePortRedirectorTester
	{
		private static readonly string[] _noPorts = { null, string.Empty };

		[Test]
		public void RedirectIfPortConfigured_NoPortConfigured_NoRequestHandling(
			[ValueSource("_noPorts")]
			string noPort)
		{
			bool called = false;
			Action<AuthorizationContext> handle = _ => called = true;
			var authorizationContext = new AuthorizationContext();
			var subject = new NonStandardSecurePortRedirector(noPort);

			subject.RedirectIfPortConfigured(authorizationContext, handle);

			Assert.That(called, Is.False);
		}

		[Test]
		public void RedirectIfPortConfigured_NoPortConfigured_NoRedirection(
			[ValueSource("_noPorts")]
			string noPort)
		{
			var initialResult = Substitute.For<ActionResult>();
			var authorizationContext = new AuthorizationContext
			{
				Result = initialResult
			};
			var subject = new NonStandardSecurePortRedirector(noPort);

			subject.RedirectIfPortConfigured(authorizationContext, _ => { });

			Assert.That(authorizationContext.Result, Is.SameAs(initialResult));
		}

		[Test]
		public void RedirectIfPortConfigured_PortConfigured_RequestHandled()
		{
			string port = "44300";

			bool called = false;
			Action<AuthorizationContext> handle = _ => called = true;

			var contextBase = Substitute.For<HttpContextBase>();
			contextBase.Request.Url.Returns(new Uri("http://localhost/WebApi"));
			var authorizationContext = new AuthorizationContext
			{
				HttpContext = contextBase,
			};

			var subject = new NonStandardSecurePortRedirector(port);
			subject.RedirectIfPortConfigured(authorizationContext, handle);

			Assert.That(called, Is.True);
		}

		[Test]
		public void RedirectIfPortConfigured_PortConfigured_RedirectionToUrlWithConfiguredPort()
		{
			string port = "44300";

			var initialResult = Substitute.For<ActionResult>();
			var contextBase = Substitute.For<HttpContextBase>();
			contextBase.Request.Url.Returns(new Uri("http://localhost/WebApi"));
			var authorizationContext = new AuthorizationContext
			{
				HttpContext = contextBase,
				Result = initialResult
			};

			var subject = new NonStandardSecurePortRedirector(port);
			subject.RedirectIfPortConfigured(authorizationContext, _ => { });

			Assert.That(authorizationContext.Result, Must.Satisfy.Conjunction(
				Is.InstanceOf<RedirectResult>(),
				Must.Have.Property<RedirectResult>(r => r.Url, Is.StringContaining(port))));
		}
	}
}