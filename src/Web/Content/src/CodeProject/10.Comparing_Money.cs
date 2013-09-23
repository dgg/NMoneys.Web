using NMoneys;
using NMoneys.Extensions;
using NUnit.Framework;

[TestFixture]
public class Comparing_Money
{
	[Test]
	public void moneys_can_be_compared()
	{
		Assert.That(3m.Usd().Equals(CurrencyIsoCode.USD.ToMoney(3m)), Is.True);
		Assert.That(3m.Usd() != CurrencyIsoCode.USD.ToMoney(3m), Is.False);
		Assert.That(3m.Usd().CompareTo(CurrencyIsoCode.USD.ToMoney(5m)), Is.LessThan(0));
		Assert.That(3m.Usd() < CurrencyIsoCode.USD.ToMoney(5m), Is.True);
	}

	[Test]
	public void comparisons_only_possible_if_they_have_the_same_currency()
	{
		Assert.That(3m.Usd().Equals(3m.Gbp()), Is.False);
		Assert.That(3m.Usd() != CurrencyIsoCode.GBP.ToMoney(3m), Is.True);

		Assert.That(() => 3m.Usd().CompareTo(CurrencyIsoCode.GBP.ToMoney(5m)),
			Throws.InstanceOf<DifferentCurrencyException>());
		Assert.That(() => 3m.Usd() < CurrencyIsoCode.GBP.ToMoney(5m),
			Throws.InstanceOf<DifferentCurrencyException>());
	}
}
