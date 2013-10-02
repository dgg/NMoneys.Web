using System.Net;
using EasyHttp.Http;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Testing.Commons;
using Testing.Commons.NUnit.Constraints;

namespace Tests.Api.v1.Resources.Support
{
	public static class ExtendedAssertions
	{
		 public static PropertyConstraint Ok(this Must.NotBeEntryPoint entry, HttpStatusCode statusCode)
		 {
			 return new LambdaPropertyConstraint<HttpResponse>(r => r.StatusCode, Is.EqualTo(statusCode));
		 }

		 public static PropertyConstraint Ok(this Must.BeEntryPoint entry)
		 {
			 return new LambdaPropertyConstraint<HttpResponse>(r => r.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		 }
	}
}