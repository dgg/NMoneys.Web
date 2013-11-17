using System;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Datatypes.Hypermedia
{
	public class Link
	{
		public Link() { }

		public Link(string rel, IReturn request, string method)
		{
			Rel = rel;
			HRef = new Uri(request.ToUrl(method), UriKind.Relative);
			Method = method;

		}

		public string Method { get; set; }
		public string Rel { get; set; }
		public Uri HRef { get; set; }

		public static Link Self(IReturn request, string method)
		{
			return new Link("self", request, method);
		}

		public static Link Parent(IReturn request, string method)
		{
			return new Link("parent", request, method);
		}

		public static Link Self(IReturn request)
		{
			return new Link("_self", request, "OPTIONS");
		}
	}

	
}