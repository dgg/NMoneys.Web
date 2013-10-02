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

		 public static PropertyConstraint Ok(this Must.NotBeEntryPoint entry, int notDefinedStatusCode)
		 {
			 return new LambdaPropertyConstraint<HttpResponse>(r => r.StatusCode, Is.EqualTo((HttpStatusCode)notDefinedStatusCode));
		 }

		 public static PropertyConstraint Ok(this Must.BeEntryPoint entry)
		 {
			 return new LambdaPropertyConstraint<HttpResponse>(r => r.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		 }

		 public static HeaderConstraint Header(this Must.HaveEntryPoint entry, string header, Constraint constraint)
		 {
			 return new HeaderConstraint(header, constraint);
		 }

		 public static HeaderConstraint LimitHeader(this Must.HaveEntryPoint entry, Constraint constraint)
		 {
			 return new HeaderConstraint("X-Rate-Limit-Limit", constraint);
		 }

		 public static HeaderConstraint RemainingHeader(this Must.HaveEntryPoint entry, Constraint constraint)
		 {
			 return new HeaderConstraint("X-Rate-Limit-Remaining", constraint);
		 }

		 public static HeaderConstraint RetryHeader(this Must.HaveEntryPoint entry, Constraint constraint)
		 {
			 return new HeaderConstraint("Retry-After", constraint);
		 }

		 public static HeaderConstraint ResetHeader(this Must.HaveEntryPoint entry, Constraint constraint)
		 {
			 return new HeaderConstraint("X-Rate-Limit-Reset", constraint);
		 }
	}
}