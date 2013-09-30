using System.Configuration;
using MongoDB.Driver;

namespace NMoneys.Web.Api.v1.Infrastructure
{
	public class MongoUrlBuilder
	{
		public MongoUrl Build()
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