namespace NMoneys.Web.ApiModel.v1.Messages.Discovery
{
	public interface IFormat
	{
		CurrencyIsoCode IsoCode { get; set; }

		decimal Amount { get; set; }
	}
}