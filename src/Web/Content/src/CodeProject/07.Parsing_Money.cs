using NMoneys;
using NUnit.Framework;

public partial class Parsing_Money
{
	[Test]
	public void moneys_can_be_parsed_to_a_known_currency()
	{
		Assert.That(Money.Parse("$1.5", Currency.Dollar),
		   isMoneyWith(1.5m, CurrencyIsoCode.USD), "one-and-the-half dollars");
		Assert.That(Money.Parse("10 €", Currency.Euro),
		   isMoneyWith(10m, CurrencyIsoCode.EUR), "ten euros");

		Assert.That(Money.Parse("kr -100", Currency.Dkk),
		   isMoneyWith(-100m, CurrencyIsoCode.DKK), "owe hundrede kroner");
		Assert.That(Money.Parse("(¤1.2)", Currency.None),
		   isMoneyWith(-1.2m, CurrencyIsoCode.XXX), "owe one point two, no currency");
	}
}
