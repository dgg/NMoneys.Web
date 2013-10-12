using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NMoneys.Web.Infrastructure.Data;

namespace NMoneys.Web.Api.v1.Infrastructure
{
	public class KeyVerifier : IKeyVerifier
	{
		private readonly MongoDatabase _db;

		public KeyVerifier()
		{
			MongoUrl url = new MongoUrlCreator().Create();

			_db = new MongoClient(url)
				.GetServer()
				.GetDatabase(url.DatabaseName);
		}

		 public bool Verify(ApiKey apiKey)
		 {
			 MongoCollection<ApiKeyDocument> keys = _db.GetCollection<ApiKeyDocument>(ApiKeyDocument.Collection);
			 ObjectId id = apiKey.AsId();

			 MongoCursor<ApiKeyDocument> findOne = keys.Find(
				 Query.And(
					Query<ApiKeyDocument>.EQ(d => d.Id, id),
					Query<ApiKeyDocument>.NE(d => d.Confirmed, null)
				 ))
				 .SetFields(Fields<ApiKeyDocument>.Include(d => d.Id))
				 .SetLimit(1);
			 
			 return findOne.SingleOrDefault() != null;
		 }
	}
}