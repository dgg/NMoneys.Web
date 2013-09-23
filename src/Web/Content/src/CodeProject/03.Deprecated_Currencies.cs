using System;
using NMoneys;
using NMoneys.Web.Content.src.CodeProject;
using NUnit.Framework;

[TestFixture]
public class Deprecated_Currencies
{
	[Test]
	public void instances_of_deprecated_currencies_can_still_be_obtained()
	{
		Currency deprecated = Currency.Get("EEK");
		Assert.That(deprecated, Is.Not.Null.And.InstanceOf<Currency>());
		Assert.That(deprecated.IsObsolete, Is.True);
	}

	[Test]
	public void whenever_a_deprecated_currency_is_obtained_an_event_is_raised()
	{
		bool called = false;
		var obsolete = CurrencyIsoCode.XXX;
		EventHandler<ObsoleteCurrencyEventArgs> callback = (sender, e) =>
		{
			called = true;
			obsolete = e.Code;
		};

		try
		{
			Currency.ObsoleteCurrency += callback;
			Currency.Get("EEK");
			Assert.That(called, Is.True);
			Assert.That(obsolete.ToString(), Is.EqualTo("EEK"));
			Assert.That(obsolete.AsAttributeProvider(), Has.Attribute<ObsoleteAttribute>());
		}
		// DO unsubscribe from global events whenever listening isnot needed anymore
		finally
		{
			Currency.ObsoleteCurrency -= callback;
		}
	}
}

