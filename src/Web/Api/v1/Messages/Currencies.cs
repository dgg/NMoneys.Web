using NMoneys.Web.ApiModel.v1.Datatypes;
using NMoneys.Web.ApiModel.v1.Messages;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages
{
	[Route("/v1/currencies", "GET", Summary = "List current supported currencies.", Notes = "Obsolete currencies are not returned.")]
	[Api("List current supported currencies.")]
	public class Currencies : IReturn<CurrencyResponse>, ICurrencies
	{ }

	public class CurrenciesResponse : ICurrenciesResponse
	{
		public CurrencySnapshot[] Snapshots { get; set; }
	}
}