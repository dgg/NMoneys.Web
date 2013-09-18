using System.Globalization;
using NMoneys;

public class Formatting_Money
{
	public Formatting_Money()
	{
		Money tenEuro = new Money(10m, CurrencyIsoCode.EUR),
			threeDollars = new Money(3m, CurrencyIsoCode.USD);

		string s = tenEuro.ToString(); // default formatting "10,00 €"
		s = threeDollars.ToString("N"); // format applied to currency "3.00"
		s = threeDollars.ToString(CultureInfo.GetCultureInfo("es-ES")); // format provider used "3,00 €", better suited for countries with same currency and different number formatting
		s = threeDollars.Format("{1} {0:00.00}"); // formatting with placeholders for currency symbol and amount "$ 03.00"
		s = new Money(1500, Currency.Eur).Format("{1} {0:#,#.00}"); // rich formatting "€ 1.500,00"
	}
}
