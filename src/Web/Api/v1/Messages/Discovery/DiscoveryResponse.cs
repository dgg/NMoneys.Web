using NMoneys.Web.ApiModel.v1.Datatypes.Discovery;
using NMoneys.Web.ApiModel.v1.Messages.Discovery;

namespace NMoneys.Web.Api.v1.Messages.Discovery
{
	public class DiscoveryResponse : IDiscoveryResponse
	{
		public Link[] _links { get; set; }
	}
}