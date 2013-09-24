using NMoneys.Web.Api.v1.Datatypes;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages
{
	[Route("/v1/currencies", "GET", Summary = "List current supported currencies.", Notes = "Obsolete currencies are not returned.")]
	[Api("List current supported currencies.")]
	public class Currencies : IReturn<CurrencyResponse> { }

	public class CurrenciesResponse
	{
		[ApiMember(Description = "desc")]
		public CurrencySnapshot[] Snapshots { get; set; }
	}
}