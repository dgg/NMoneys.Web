using NMoneys;
using NMoneys.Extensions;

public class Extensions
{
	public Extensions()
	{
		3m.Gbp();
		1000m.Pounds();
		CurrencyIsoCode.NOK.ToMoney(3m);
	}
}
