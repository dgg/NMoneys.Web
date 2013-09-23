using NMoneys;

public class Dealing_with_CurrencyIsoCode_
{
	public Dealing_with_CurrencyIsoCode_()
	{
		CurrencyIsoCode usd = Currency.Code.Cast(840);
		CurrencyIsoCode eur = Currency.Code.Parse("eur");

		CurrencyIsoCode? maybeAud;
		Currency.Code.TryCast(36, out maybeAud);
		Currency.Code.TryParse("036", out maybeAud);

		short thirtySix = CurrencyIsoCode.AUD.NumericCode();
		string USD = CurrencyIsoCode.USD.AlphabeticCode();
		string zeroThreeSix = CurrencyIsoCode.AUD.PaddedNumericCode();
	}
}
