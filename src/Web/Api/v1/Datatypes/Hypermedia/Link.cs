using System;

namespace NMoneys.Web.Api.v1.Datatypes.Hypermedia
{
	public class Link
	{
		public string Method { get; set; }
		public string Rel { get; set; }
		public Uri Href { get; set; }
	}
}