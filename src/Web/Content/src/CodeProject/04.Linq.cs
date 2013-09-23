using System.Linq;
using NMoneys;
using NUnit.Framework;

[TestFixture]
public class Linq
{
	[Test]
	public void all_currencies_can_be_obtained_and_linq_operators_applied()
	{
		Assert.That(Currency.FindAll(), Is.Not.Null.And.All.InstanceOf<Currency>());
		var allCurrenciesWithoutMinorUnits =
			Currency.FindAll().Where(c => c.SignificantDecimalDigits == 0);
		Assert.That(allCurrenciesWithoutMinorUnits, Is.Not.Empty.And.Contains(Currency.Jpy));
	}
}
