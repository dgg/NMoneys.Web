using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages.Discovery
{
	[Route("/v1/currencies/format", "OPTIONS", Summary = "Provides information about formatting multiple monetary amounts according to their currency.")]
	[Api("Provides information about formatting multiple monetary amounts according to their currency.")]
	public class DiscoverMultiFormat : IReturn<DiscoveryResponse>
	{
		
	}
}