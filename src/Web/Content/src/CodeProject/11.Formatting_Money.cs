using System.Globalization;
using NMoneys;
using NMoneys.Extensions;
using NUnit.Framework;

[TestFixture]
public class Formatting_Money
{
	[Test]
	public void moneys_are_to_be_displayed()
	{
		Assert.That(10.536m.Eur().ToString(), Is.EqualTo("10,54 €"),
			"default currency formatting according to instance's currency");
		Assert.That(3.2m.Usd().ToString("N"), Is.EqualTo("3.20"),
			"alternative formatting according to instance's currency");
	}

	[Test]
	public void using_different_styles_for_currencies_taht_span_multiple_countries()
	{
		Assert.That(3000.5m.Eur().ToString(), Is.EqualTo("3.000,50 €"), "default euro formatting");

		// in French the group separator is neither the dot or the space
		CultureInfo french = CultureInfo.GetCultureInfo("fr-FR");
		string threeThousandAndTheHaldInFrench = string.Format("3{0}000,50 €",
			french.NumberFormat.CurrencyGroupSeparator);
		Assert.That(3000.5m.Eur().ToString(french),
			Is.EqualTo(threeThousandAndTheHaldInFrench));
	}

	[Test]
	public void more_complex_formatting()
	{
		Assert.That(3m.Usd().Format("{0:00.00} {2}"), Is.EqualTo("03.00 USD"),
			"formatting placeholders for code and amount");
		Assert.That(2500m.Eur().Format("> {1} {0:#,#.00}"), Is.EqualTo("> € 2.500,00"),
			"rich amount formatting");
	}
}
