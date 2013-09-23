using System;
using NMoneys;
using NMoneys.Extensions;
using NUnit.Framework;

public partial class Money_Arithmetic
{
	[Test]
	public void moneys_are_to_be_operated_with_arithmetic_operators()
	{
		Money fivePounds = 2m.Pounds().Plus(3m.Pounds());
		Assert.That(fivePounds, isMoneyWith(5m, CurrencyIsoCode.GBP));

		Money fiftyPence = 3m.Pounds() - 2.5m.Pounds();
		Assert.That(fiftyPence, isMoneyWith(.5m, CurrencyIsoCode.GBP));

		Money youOweMeThreeEuros = -3m.Eur();
		Assert.That(youOweMeThreeEuros, isMoneyWith(-3m, CurrencyIsoCode.EUR));

		Money nowIHaveThoseThreeEuros = youOweMeThreeEuros.Negate();
		Assert.That(nowIHaveThoseThreeEuros, isMoneyWith(3m, CurrencyIsoCode.EUR));

		Money youOweMeThreeEurosAgain = -nowIHaveThoseThreeEuros;
		Assert.That(youOweMeThreeEurosAgain, isMoneyWith(-3m, CurrencyIsoCode.EUR));
	}

	[Test]
	public void basic_arithmetic_operations_can_be_extended()
	{
		Money halfMyDebt = -60m.Eur().Perform(amt => amt / 2);
		Assert.That(halfMyDebt, isMoneyWith(-30m, CurrencyIsoCode.EUR));

		Money convolutedWayToCancelDebt = (-50m).Eur().Perform(-1m.Eur(),
			(amt1, amt2) => decimal.Multiply(amt1, decimal.Negate(amt2)) - amt1);
		Assert.That(convolutedWayToCancelDebt,
			isMoneyWith(decimal.Zero, CurrencyIsoCode.EUR));
	}

	[Test]
	public void binary_operations_only_possible_if_they_have_the_same_currency()
	{
		Assert.That(() => 2m.Gbp().Minus(3m.Eur()),
			Throws.InstanceOf<DifferentCurrencyException>());
		Assert.That(() => 2m.Cad() + 3m.Aud(), Throws.InstanceOf<DifferentCurrencyException>());
		Assert.That(() => 3m.Usd().Perform(3m.Aud(), (x, y) => x + y),
			Throws.InstanceOf<DifferentCurrencyException>());
	}

	[Test]
	public void several_unary_operations_can_be_performed()
	{
		Assert.That(3m.Xxx().Negate(), isMoneyWith(-3m), "-1 * amount");
		Assert.That((-3m).Xxx().Abs(), isMoneyWith(3m), "|amount|");

		Money twoThirds = new Money(2m / 3);
		Assert.That(twoThirds.Amount, Is.Not.EqualTo(0.66m),
			"not exactly equals as it has more decimals");
		Assert.That(twoThirds.TruncateToSignificantDecimalDigits().Amount, Is.EqualTo(0.66m),
			"XXX has two significant decimals");

		Money fractional = 123.456m.ToMoney();
		Assert.That(fractional.Truncate(), isMoneyWith(123m), "whole amount");

		Assert.That(.5m.ToMoney().RoundToNearestInt(), isMoneyWith(0m));
		Assert.That(.599999m.ToMoney().RoundToNearestInt(), isMoneyWith(1m));
		Assert.That(1.5m.ToMoney().RoundToNearestInt(), isMoneyWith(2m));
		Assert.That(1.4999999m.ToMoney().RoundToNearestInt(), isMoneyWith(1m));

		Assert.That(.5m.ToMoney().RoundToNearestInt(MidpointRounding.ToEven),
			isMoneyWith(0m), "closest even number is 0");
		Assert.That(.5m.ToMoney().RoundToNearestInt(MidpointRounding.AwayFromZero),
			isMoneyWith(1m), "closest number away from zero is 1");
		Assert.That(1.5m.ToMoney().RoundToNearestInt(MidpointRounding.ToEven),
			isMoneyWith(2m), "closest even number is 2");
		Assert.That(1.5m.ToMoney().RoundToNearestInt(MidpointRounding.AwayFromZero),
			isMoneyWith(2m), "closest number away from zero is 2");

		Assert.That(2.345m.Usd().Round(), isMoneyWith(2.34m), "round to two decimals");
		Assert.That(2.345m.Jpy().Round(), isMoneyWith(2m), "round to no decimals");
		Assert.That(2.355m.Usd().Round(), isMoneyWith(2.36m), "round to two decimals");
		Assert.That(2.355m.Jpy().Round(), isMoneyWith(2m), "round to no decimals");

		Assert.That(2.345m.Usd().Round(MidpointRounding.ToEven), isMoneyWith(2.34m));
		Assert.That(2.345m.Usd().Round(MidpointRounding.AwayFromZero), isMoneyWith(2.35m));
		Assert.That(2.345m.Jpy().Round(MidpointRounding.ToEven), isMoneyWith(2m));
		Assert.That(2.345m.Jpy().Round(MidpointRounding.AwayFromZero), isMoneyWith(2m));

		Assert.That(123.456m.ToMoney().Floor(), isMoneyWith(123m));
		Assert.That((-123.456m).ToMoney().Floor(), isMoneyWith(-124m));
	}
}
