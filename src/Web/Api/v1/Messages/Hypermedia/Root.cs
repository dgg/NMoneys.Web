using NMoneys.Web.Api.v1.Datatypes.Hypermedia;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages.Hypermedia
{
	[Route("/v1", "OPTIONS", Summary = "List operations available through the API.")]
	[Api("List operations available through the API.")]
	public class root : IReturn<rootResponse> { }

	public class rootResponse
	{
		public Link[] _links { get; set; }
	}
}