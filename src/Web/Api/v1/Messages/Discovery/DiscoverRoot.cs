using NMoneys.Web.ApiModel.v1.Messages.Discovery;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages.Discovery
{
	[Route("/v1", "OPTIONS", Summary = "Provides information about resources available through the API.")]
	[Api("Provides information about resources available through the API.")]
	public class DiscoverRoot : IReturn<DiscoveryResponse>, IRoot
	{ }
}