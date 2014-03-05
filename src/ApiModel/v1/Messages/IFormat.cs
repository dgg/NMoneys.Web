namespace NMoneys.Web.ApiModel.v1.Messages
{
	public interface IFormat
	{
		CurrencyIsoCode IsoCode { get; set; }

		decimal Amount { get; set; }
	}
}