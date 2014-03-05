using NMoneys.Web.ApiModel.v1.Datatypes;
using NMoneys.Web.ApiModel.v1.Messages;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages
{
	[Route("/v1/currencies/{isoCode}", "GET", 
		Summary = "Get detailed information about a currency.")]
	[Api("Get detailed information about a currency.")]
	public class Currency : IReturn<CurrencyResponse>
	{
		[ApiMember(IsRequired = true, ParameterType = "path", Description = "Three-letter ISO code of the currency to return.")]
		public CurrencyIsoCode IsoCode { get; set; }	 
	}

	public class CurrencyResponse : ICurrencyResponse
	{
		public CurrencyDetail Detail { get; set; }
	}
}