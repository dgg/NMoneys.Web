using NMoneys.Web.Api.v1.Datatypes.Hypermedia;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages.Hypermedia
{
	[Route("/v1/currencies/{isoCode}/format", "OPTIONS", Summary = "Allows formatting a monetary amount according to its currency.")]
	[Route("/v1/currencies/{isoCode}/format/{amount}", "OPTIONS", Summary = "Allows formatting a monetary amount according to its currency.")]
	[Api("Allows formatting a monetary amount according to its currency.")]
	public class Format : IReturn<FormatResponse>
	{
		[ApiMember(IsRequired = true, ParameterType = "path, body", Verb = "GET, POST",
			Description = "Three-letter ISO code of the currency to use for formatting.")]
		public CurrencyIsoCode IsoCode { get; set; }

		[ApiMember(IsRequired = true, ParameterType = "path, body", Verb = "GET, POST",
			Description = "Amount of the monetary quantity to be formatted.")]
		public decimal Amount { get; set; }
	}

	public class FormatResponse
	{
		public Link[] _Links { get; set; }
	}
}