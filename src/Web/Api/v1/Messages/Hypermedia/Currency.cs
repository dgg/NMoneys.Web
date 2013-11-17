using NMoneys.Web.Api.v1.Datatypes.Hypermedia;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages.Hypermedia
{
	[Route("/v1/currencies/{isoCode}", "OPTIONS", Summary = "Get detailed information about a currency.")]
	[Api("Get detailed information about a currency.")]
	public class Currency : IReturn<CurrencyResponse>
	{
		[ApiMember(IsRequired = true, ParameterType = "path", Description = "Three-letter ISO code of the currency to return.")]
		public CurrencyIsoCode IsoCode { get; set; }
	}

	public class CurrencyResponse
	{
		public Link[] _Links { get; set; }
	}
}