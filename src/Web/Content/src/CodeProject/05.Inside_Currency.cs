using NMoneys;
using NUnit.Framework;

public class Inside_Currency
{
	[Test]
	public void whats_in_a_currency_anyway()
	{
		Currency euro = Currency.Eur;

		Assert.That(euro.IsObsolete, Is.False);
		Assert.That(euro.IsoCode, Is.EqualTo(CurrencyIsoCode.EUR));
		Assert.That(euro.IsoSymbol, Is.EqualTo("EUR"));
		Assert.That(euro.NativeName, Is.EqualTo("Euro"),
		  "capitalized in the default instance");
		Assert.That(euro.NumericCode, Is.EqualTo(978));
		Assert.That(euro.PaddedNumericCode, Is.EqualTo("978"),
		  "a string of 3 characters containing the numeric code and zeros if needed");
		Assert.That(euro.Symbol, Is.EqualTo("€"));
	}

	[Test]
	public void some_currencies_have_an_character_reference_for_angly_bracket_languages()
	{
		Currency qatariRial = Currency.Get(CurrencyIsoCode.QAR);
		CharacterReference reference = qatariRial.Entity;
		Assert.That(reference, Is.Not.Null.And.Property("IsEmpty").True,
			"the Rial does not have an reference, but a 'null' object");

		Currency euro = Currency.Euro;
		reference = euro.Entity;
		Assert.That(reference, Is.Not.Null.And.Property("IsEmpty").False,
					"the euro, does");
		Assert.That(reference.Character, Is.EqualTo("€"));
		Assert.That(reference.CodePoint, Is.EqualTo(8364));
		Assert.That(reference.EntityName, Is.EqualTo("&euro;"));
		Assert.That(reference.EntityNumber, Is.EqualTo("&#8364;"));
		Assert.That(reference.SimpleName, Is.EqualTo("euro"));
	}
}
