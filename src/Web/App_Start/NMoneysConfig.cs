namespace NMoneys.Web.App_Start
{
	public class NMoneysConfig
	{
		public static void Configure()
		{
			Currency.InitializeAllCurrencies();
		}
	}
}