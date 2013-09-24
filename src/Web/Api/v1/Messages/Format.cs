using NMoneys.Web.Api.v1.Datatypes;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages
{
	[Route("/v1/currencies/{isoCode}/format/{amount}", "GET")]
	[Route("/v1/currencies/{isoCode}/format", "POST")]
	public class Format
	{
		public CurrencyIsoCode IsoCode { get; set; }
		public decimal Amount { get; set; }
	}

	public class FormatResponse
	{
		public FormattedMoney Money { get; set; }
	}
}