using NMoneys;
using NMoneys.Exchange;
using NUnit.Framework;

public class Meaningless_Defaults
{
	[Test]
	public void Default_Conversions_DoNotBlowUpButAreNotTerriblyUseful()
	{
		var tenEuro = new Money(10m, CurrencyIsoCode.EUR);

		var tenDollars = tenEuro.Convert().To(CurrencyIsoCode.USD);
		Assert.That(tenDollars.Amount, Is.EqualTo(10m));

		var tenPounds = tenEuro.Convert().To(Currency.Gbp);
		Assert.That(tenPounds.Amount, Is.EqualTo(10m));
	}
}