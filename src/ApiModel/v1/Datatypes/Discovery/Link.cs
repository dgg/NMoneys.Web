using System;

namespace NMoneys.Web.ApiModel.v1.Datatypes.Discovery
{
	public class Link
	{
		public string Method { get; set; }
		public string Rel { get; set; }
		public Uri Href { get; set; }
	}
}