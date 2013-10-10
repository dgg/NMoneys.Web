using System;
using MongoDB.Bson;

namespace NMoneys.Web.Infrastructure.Data
{
	// shared schema for persistent api keys
	public class ApiKeyDocument
	{
		public static readonly string Collection = "keys";

		public ObjectId Id { get; set; }
		public string OwnerEmail { get; set; }
		public DateTime Requested { get; set; }
		public DateTime? Confirmed { get; set; }
	}
}