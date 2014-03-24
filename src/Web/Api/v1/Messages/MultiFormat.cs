using NMoneys.Web.ApiModel.v1.Datatypes;
using NMoneys.Web.ApiModel.v1.Messages;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Messages
{
	[Route("/v1/currencies/format", "POST", Summary = "")]
	[Api("Allows formatting monetary amounts according to their currencies.")]
	public class MultiFormat : IReturn<MultiFormatResponse>
	{
		[ApiMember(IsRequired = true, ParameterType = "body", Verb = "POST",
			Description = "Quantities to be formatted.")]
		public FormatableQuantity[] Quantities { get; set; }
	}

	public class MultiFormatResponse : IMultiFormatResponse
	{
		public FormattedMoney[] Moneys { get; set; }
	}
}