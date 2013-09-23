using System.Globalization;
using NMoneys;

public class Instantiating_Currency
{
	public Instantiating_Currency()
	{
		Currency euro = Currency.Eur;

		Currency cad = Currency.Get(CurrencyIsoCode.CAD);
		Currency australianDollar = Currency.Get("AUD");
		euro = Currency.Get(CultureInfo.GetCultureInfo("es-ES"));

		Currency itMightNotBe;
		string isoSymbol = "usd";
		CurrencyIsoCode isoCode = CurrencyIsoCode.USD;
		CultureInfo culture = CultureInfo.GetCultureInfo("da-DK");
		bool wasFound = Currency.TryGet(isoSymbol, out itMightNotBe);
		wasFound = Currency.TryGet(isoCode, out itMightNotBe);
		wasFound = Currency.TryGet(culture, out itMightNotBe);
	}
}