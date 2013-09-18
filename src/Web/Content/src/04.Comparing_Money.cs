using NMoneys;

public class Comparing_Money
{
	int isPositive = new Money(3m, Currency.Nok).CompareTo(new Money(2m, CurrencyIsoCode.NOK));
	bool isFalse = new Money(2m) > new Money(3m);
	bool areEqual = new Money(1m, Currency.Xxx).Equals(new Money(1m, Currency.Xxx));
	bool areNotEqual = new Money(2m) != new Money(5m);
}
