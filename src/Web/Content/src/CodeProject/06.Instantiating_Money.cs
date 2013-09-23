using System.Globalization;
using NMoneys;
using NMoneys.Extensions;
using NUnit.Framework;

[TestFixture]
public class Instantiating_Money
{
	[Test]
	public void a_Money_represents_a_monetary_quantity()
	{
		Money tenDollars = new Money(10m, Currency.Dollar);
		Money twoFiftyEuros = new Money(2.5m, CurrencyIsoCode.EUR);
		Money tenYen = new Money(10m, "JPY");
		Money zeroWithNoCurrency = new Money();
	}

	[Test, SetCulture("da-DK")]
	public void environment_dependencies_are_explicit()
	{
		Money fiveKrona = Money.ForCurrentCulture(5m);
		Assert.That(fiveKrona.CurrencyCode, Is.EqualTo(CurrencyIsoCode.DKK));

		Money currencyLessMoney = new Money(1);
		Assert.That(currencyLessMoney.CurrencyCode, Is.EqualTo(CurrencyIsoCode.XXX));

		Money zeroEuros = Money.ForCulture(0m, CultureInfo.GetCultureInfo("es-ES"));
		Assert.That(zeroEuros.CurrencyCode, Is.EqualTo(CurrencyIsoCode.EUR));
	}

	[Test]
	public void moneys_can_be_quickly_created_for_testing_scenarios_with_extension_methods()
	{
		// Money --> threeNoCurrencies
		3m.Xxx();
		3m.ToMoney();

		// Money --> threeAndAHalfAustralianDollars
		3.5m.Aud();
		3.5m.ToMoney(Currency.Aud);
		3.5m.ToMoney(CurrencyIsoCode.AUD);
		CurrencyIsoCode.AUD.ToMoney(3.5m);
	}
}
