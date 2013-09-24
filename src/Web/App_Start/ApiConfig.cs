using NMoneys.Web.Api;

namespace NMoneys.Web.App_Start
{
	public class ApiConfig
	{
		public static void RegisterApi()
		{
			new AppHost().Init();
		}
	}
}