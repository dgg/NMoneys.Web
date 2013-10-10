using System;
using System.Net.Mail;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NMoneys.Web.Infrastructure.Data;

namespace NMoneys.Web.Controllers.Infrastructure
{
	public class ApiKeyRepository : IApiKeyRepository
	{
		private readonly MongoDatabase _db;

		public ApiKeyRepository()
		{
			MongoUrl url = new MongoUrlCreator().Create();

			_db = new MongoClient(url)
				.GetServer()
				.GetDatabase(url.DatabaseName);
		}

		public void SavePending(ApiKey toBeConfirmed, MailAddress keyOwner, DateTimeOffset requested)
		{
			assertUtc(requested, "requested");

			var apiKey = new ApiKeyDocument
			{
				Id = toBeConfirmed.AsId(),
				Requested = requested.UtcDateTime,
				OwnerEmail = keyOwner.Address,
			};

			_db.GetCollection<ApiKeyDocument>(ApiKeyDocument.Collection).Save(apiKey);
		}

		private void assertUtc(DateTimeOffset time, string paramName)
		{
			if (!time.Offset.Equals(TimeSpan.Zero)) throw new ArgumentException("Dates must be specified as UTC.", paramName);
		}

		public void Confirm(ApiKey toBeConfirmed, DateTimeOffset confirmed)
		{
			assertUtc(confirmed, "confirmed");

			_db.GetCollection(ApiKeyDocument.Collection).Update(
				Query<ApiKeyDocument>.EQ(d => d.Id, toBeConfirmed.AsId()),
				Update<ApiKeyDocument>.Set(d => d.Confirmed, confirmed.UtcDateTime));
		}
	}
}