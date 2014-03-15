using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages.Discovery
{
	[Route("/v1/currencies", "OPTIONS", Summary = "Provides information about supported currencies.", Notes = "Obsolete currencies are also returned.")]
	[Api("Provides information about current supported currencies.")]
	public class DiscoverCurrencies : IReturn<DiscoveryResponse>
	{ }
}