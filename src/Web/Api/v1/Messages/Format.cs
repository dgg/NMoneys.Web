using NMoneys.Web.ApiModel.v1.Datatypes;
using NMoneys.Web.ApiModel.v1.Messages;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages
{
	[Route("/v1/currencies/{isoCode}/format/{amount}", "GET", Summary = "Allows formatting a monetary amount according to its currency.")]
	[Api("Allows formatting a monetary amount according to its currency.")]
	public class Format : IReturn<FormatResponse>
	{
		[ApiMember(IsRequired = true, ParameterType = "path", Verb = "GET",
			Description = "Three-letter ISO code of the currency to use for formatting.")]
		public CurrencyIsoCode IsoCode { get; set; }
		[ApiMember(IsRequired = true, ParameterType = "path", Verb = "GET",
			Description = "Amount of the monetary quantity to be formatted.")]
		public decimal Amount { get; set; }
	}

	public class FormatResponse : IFormatResponse
	{
		public FormattedMoney Money { get; set; }
	}
}