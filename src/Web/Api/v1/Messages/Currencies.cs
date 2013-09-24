using NMoneys.Web.Api.v1.Datatypes;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages
{
	[Route("/v1/currencies", "GET")]
	public class Currencies { }

	public class CurrenciesResponse
	{
		public CurrencySnapshot[] Snapshots { get; set; }
	}
}