namespace NMoneys.Web.Content.src
{
	public class Instantiating_Money
	{
		public Instantiating_Money()
		{
			var threeDollars = new Money(3m, Currency.Usd);
			var twoandAHalfPounds = new Money(2.5m, CurrencyIsoCode.GBP);
			var tenEuro = new Money(10m, "EUR");
			var thousandWithMissingCurrency = new Money(1000m);
		}
	}
}
