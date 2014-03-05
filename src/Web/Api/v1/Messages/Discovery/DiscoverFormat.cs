using NMoneys.Web.ApiModel.v1.Messages.Discovery;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages.Discovery
{
	[Route("/v1/currencies/{isoCode}/format/{amount}", "OPTIONS", Summary = "Provides information about formatting a monetary amount according to its currency.")]
	[Api("Provides information about formatting a monetary amount according to its currency.")]
	public class DiscoverFormat : IReturn<DiscoveryResponse>, IFormat
	{
		[ApiMember(IsRequired = true, ParameterType = "path", Verb = "OPTIONS",
			Description = "Three-letter ISO code of the currency to use for formatting.")]
		public CurrencyIsoCode IsoCode { get; set; }

		[ApiMember(IsRequired = true, ParameterType = "path", Verb = "OPTIONS",
			Description = "Amount of the monetary quantity to be formatted.")]
		public decimal Amount { get; set; }
	}
}