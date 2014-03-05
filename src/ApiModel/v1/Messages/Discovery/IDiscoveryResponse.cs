using NMoneys.Web.ApiModel.v1.Datatypes.Discovery;

namespace NMoneys.Web.ApiModel.v1.Messages.Discovery
{
	public interface IDiscoveryResponse
	{
		Link[] _links { get; set; }
	}
}