using NMoneys.Web.ApiModel.v1.Datatypes;

namespace NMoneys.Web.ApiModel.v1.Messages
{
	public interface IFormatResponse
	{
		FormattedMoney Money { get; set; }
	}
}