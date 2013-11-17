using NMoneys.Web.Api.v1.Datatypes.Hypermedia;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages.Hypermedia
{
	[Route("/v1/currencies/{isoCode}", "OPTIONS", Summary = "Provides information about a currency.")]
	[Api("Provides information about a currency.")]
	public class currency : IReturn<currencyResponse>
	{
		[ApiMember(IsRequired = true, ParameterType = "path", Description = "Three-letter ISO code of the currency to return.")]
		public CurrencyIsoCode IsoCode { get; set; }
	}

	public class currencyResponse
	{
		public Link[] _links { get; set; }
	}
}