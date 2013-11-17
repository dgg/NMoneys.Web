using NMoneys.Web.Api.v1.Datatypes.Hypermedia;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages.Hypermedia
{
	[Route("/v1/currencies", "OPTIONS", Summary = "List current supported currencies.", Notes = "Obsolete currencies are not returned.")]
	[Api("List current supported currencies.")]
	public class currencies : IReturn<currenciesResponse> { }

	public class currenciesResponse : IReturn<currenciesResponse>
	{
		public Link[] _links { get; set; }
	}

	
}