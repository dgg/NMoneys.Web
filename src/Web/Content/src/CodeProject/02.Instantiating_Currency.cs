using System.Globalization;
using NMoneys;
using NUnit.Framework;

[TestFixture]
public class Instantiating_Currency
{
	[Test]
	public void popular_currency_instances_can_be_obtained_from_static_accessors()
	{
		Assert.That(Currency.Usd, Is.Not.Null.And.InstanceOf<Currency>());
		Assert.That(Currency.Eur, Is.Not.Null.And.InstanceOf<Currency>());
		Assert.That(Currency.Dkk, Is.Not.Null.And.InstanceOf<Currency>());
		Assert.That(Currency.Xxx, Is.Not.Null.And.InstanceOf<Currency>());
	}

	[Test]
	public void currency_instances_can_be_obtained_from_its_code_enum()
	{
		Assert.That(Currency.Get(CurrencyIsoCode.ZAR), Is.InstanceOf<Currency>());
	}

	[Test]
	public void currency_instances_can_be_obtained_from_its_code_string()
	{
		Assert.That(Currency.Get("eur"), Is.Not.Null);
		Assert.That(Currency.Get("EUR"), Is.Not.Null);
	}

	[Test]
	public void currency_instances_can_be_obtained_from_a_CultureInfo_instance()
	{
		CultureInfo swedish = CultureInfo.GetCultureInfo("sv-SE");
		Assert.That(Currency.Get(swedish), Is.EqualTo(Currency.Sek));
	}

	[Test]
	public void currency_instances_can_be_obtained_from_a_RegionInfo_instance()
	{
		var spain = new RegionInfo("es");
		Assert.That(Currency.Get(spain), Is.EqualTo(Currency.Eur));
	}

	[Test]
	public void currency_instances_can_be_obtained_with_a_try_do_pattern()
	{
		Currency currency;
		Assert.That(Currency.TryGet(CurrencyIsoCode.ZAR, out currency), Is.True);
		Assert.That(currency, Is.Not.Null.And.InstanceOf<Currency>());

		Assert.That(Currency.TryGet("zar", out currency), Is.True);
		Assert.That(currency, Is.Not.Null.And.InstanceOf<Currency>());
		Assert.That(Currency.TryGet(CultureInfo.GetCultureInfo("en-ZA"), out currency), Is.True);
		Assert.That(currency, Is.Not.Null.And.InstanceOf<Currency>());

		Assert.That(Currency.TryGet(new RegionInfo("ZA"), out currency), Is.True);
		Assert.That(currency, Is.Not.Null.And.InstanceOf<Currency>());
	}

	[Test]
	public void TryGet_does_not_throw_if_currency_cannot_be_found()
	{
		Currency currency;
		var notDefined = (CurrencyIsoCode)0;
		Assert.That(Currency.TryGet(notDefined, out currency), Is.False);
		Assert.That(currency, Is.Null);
		Assert.That(Currency.TryGet("notAnIsoCode", out currency), Is.False);
		Assert.That(currency, Is.Null);

		CultureInfo neutralCulture = CultureInfo.GetCultureInfo("da");
		Assert.That(Currency.TryGet(neutralCulture, out currency), Is.False);
		Assert.That(currency, Is.Null);
	}
}
