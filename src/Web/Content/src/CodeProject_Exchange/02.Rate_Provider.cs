using NMoneys;
using NMoneys.Exchange;
using NUnit.Framework;

public class Rate_Provider
{
	[Test]
	public void Configuring_Provider()
	{
		var customProvider = new TabulatedExchangeRateProvider();
		customProvider.Add(CurrencyIsoCode.EUR, CurrencyIsoCode.USD, 0);

		ExchangeRateProvider.Factory = () => customProvider;

		var tenEuro = new Money(10m, CurrencyIsoCode.EUR);
		var zeroDollars = tenEuro.Convert().To(CurrencyIsoCode.USD);

		// go back to default
		ExchangeRateProvider.Factory = ExchangeRateProvider.Default;
	}
}
