using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages.Discovery
{
	[Route("/v1/currencies/{isoCode}", "OPTIONS", Summary = "Provides information about a currency.")]
	[Api("Provides information about a currency.")]
	public class DiscoverCurrency : IReturn<DiscoveryResponse>
	{
		[ApiMember(IsRequired = true, ParameterType = "path", Description = "Three-letter ISO code of the currency to return.")]
		public CurrencyIsoCode IsoCode { get; set; }
	}
}