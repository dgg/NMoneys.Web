using NMoneys;
using NUnit.Framework;

[TestFixture]
public class Inside_Money
{
	[Test]
	public void what_is_in_a_money()
	{
		var threeCads = new Money(3m, "CAD");

		Assert.That(threeCads.Amount, Is.EqualTo(3m));
		Assert.That(threeCads.CurrencyCode, Is.EqualTo(CurrencyIsoCode.CAD));
		Assert.That(threeCads.HasDecimals, Is.False);
		Assert.That(threeCads.IsNegative(), Is.False);
		Assert.That(threeCads.IsNegativeOrZero(), Is.False);
		Assert.That(threeCads.IsPositive(), Is.True);
		Assert.That(threeCads.IsPositiveOrZero(), Is.True);
		Assert.That(threeCads.IsZero(), Is.False);
	}
}
