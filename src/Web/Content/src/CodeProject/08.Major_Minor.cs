using NMoneys;
using NMoneys.Extensions;
using NUnit.Framework;

[TestFixture]
public partial class Major_Minor
{
	[Test]
	public void what_is_with_this_Major_thing()
	{
		Assert.That(Money.ForMajor(234, Currency.Gbp), isMoneyWith(234, CurrencyIsoCode.GBP),
			"instance created from the major units, in this case the Pound");

		Assert.That(3m.Pounds().MajorAmount, Is.EqualTo(3m),
			"for whole amounts is the quantity");
		Assert.That(3.7m.Pounds().MajorAmount, Is.EqualTo(3m),
			"for fractional amounts is the number of pounds");
		Assert.That(0.7m.Pounds().MajorAmount, Is.EqualTo(0m),
			"for fractional amounts is the number of pounds");

		Assert.That(3m.Pounds().MajorIntegralAmount, Is.EqualTo(3L),
			"for whole amounts is the non-fractional quantity");
		Assert.That(3.7m.Pounds().MajorIntegralAmount, Is.EqualTo(3L),
			"for fractional amounts is the number of pounds");
		Assert.That(0.7m.Pounds().MajorIntegralAmount, Is.EqualTo(0L),
			"for fractional amounts is the number of pounds");
	}

	[Test]
	public void what_is_with_this_Minor_thing()
	{
		Assert.That(Currency.Pound.SignificantDecimalDigits, Is.EqualTo(2),
			"pounds have pence, which is a hundreth of the major unit");

		Assert.That(Money.ForMinor(234, Currency.Gbp), isMoneyWith(2.34m, CurrencyIsoCode.GBP),
			"234 pence is 2.34 pounds");
		Assert.That(Money.ForMinor(50, Currency.Gbp), isMoneyWith(0.5m, CurrencyIsoCode.GBP),
			"fifty pence is half a pound");
		Assert.That(Money.ForMinor(-5, Currency.Gbp), isMoneyWith(-0.05m, CurrencyIsoCode.GBP),
			"you owe me five pence, but keep them");

		Assert.That(3m.Pounds().MinorAmount, Is.EqualTo(300m),
			"three pounds is 300 pence");
		Assert.That(.07m.Pounds().MinorAmount, Is.EqualTo(7m),
			"for fractional amounts, the minor unit prevails");
		Assert.That(0.072m.Pounds().MinorAmount, Is.EqualTo(7m),
			"tenths of pence are discarded");

		Assert.That(3m.Pounds().MinorIntegralAmount, Is.EqualTo(300L),
			"three pounds is 300 pence");
		Assert.That(.07m.Pounds().MinorIntegralAmount, Is.EqualTo(7L),
			"for fractional amounts, the minor unit prevails");
		Assert.That(0.072m.Pounds().MinorIntegralAmount, Is.EqualTo(7L),
			"tenths of pence are discarded");
	}
}
