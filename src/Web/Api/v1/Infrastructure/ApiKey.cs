using MongoDB.Bson;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Infrastructure
{
	public class ApiKey
	{
		public static readonly string ParameterName = "api_key";

		private readonly bool _isMissing;
		private readonly ObjectId _oid;

		private ApiKey(ObjectId? oid)
		{
			_isMissing = !oid.HasValue;

			_oid = oid.GetValueOrDefault(ObjectId.Empty);
		}

		public override string ToString()
		{
			return _oid.ToString();
		}

		public ObjectId AsId()
		{
			return _oid;
		}

		public bool IsMissing { get { return _isMissing; } }

		public static ApiKey ExtractFrom(IHttpRequest request)
		{
			string apiKey = request.Headers[ParameterName];
			if (missing(apiKey))
			{
				apiKey = request.QueryString[ParameterName];
			}
			return new ApiKey(tryParse(apiKey));
		}

		private static ObjectId? tryParse(string apiKey)
		{
			ObjectId oid;
			return ObjectId.TryParse(apiKey, out oid) ? oid : default(ObjectId?);
		}

		private static bool missing(string apiKey)
		{
			return string.IsNullOrEmpty(apiKey);
		}
	}
}