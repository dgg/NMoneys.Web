﻿using MongoDB.Bson;
using MongoDB.Driver;

namespace NMoneys.Web.Api.v1.Infrastructure
{
	public class KeyVerifier : IKeyVerifier
	{
		private readonly MongoDatabase _db;

		public KeyVerifier()
		{
			MongoUrl url = new MongoUrlBuilder().Build();

			_db = new MongoClient(url)
				.GetServer()
				.GetDatabase(url.DatabaseName);
		}

		 public bool Verify(ApiKey apiKey)
		 {			 
			 MongoCollection<BsonDocument> keys = _db.GetCollection("keys");
			 ObjectId? id = apiKey.AsId();
			 return (id.HasValue && 
				 keys.FindOneById(id.Value) != null);
		 }
	}
}