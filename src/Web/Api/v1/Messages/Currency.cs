using NMoneys.Web.Api.v1.Datatypes;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages
{
	[Route("/v1/currencies/{isoCode}", "GET")]
	public class Currency : IReturn<CurrencyResponse>
	{
		public CurrencyIsoCode IsoCode { get; set; }	 
	}

	public class CurrencyResponse
	{
		public CurrencyDetail Detail { get; set; }
	}
}