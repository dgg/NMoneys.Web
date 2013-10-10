using System.Configuration;
using MongoDB.Driver;

namespace NMoneys.Web.Infrastructure.Data
{
	public class MongoUrlCreator
	{
		public MongoUrl Create()
		{
			// try local first
			ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["MongoDb"];
			if (connectionString != null)
			{
				return new MongoUrl(connectionString.ConnectionString);
			}
			// try AppHarbor later
			string setting = ConfigurationManager.AppSettings["MONGOLAB_URI"];
			return new MongoUrl(setting);
		}
	}
}