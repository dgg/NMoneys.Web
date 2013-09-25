using MongoDB.Bson;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Infrastructure
{
	public class ApiKey
	{
		public static readonly string ParameterName = "api_key";

		private readonly string _apiKey;

		private ApiKey(string apiKey)
		{
			_apiKey = apiKey;
		}

		public override string ToString()
		{
			return _apiKey;
		}

		public ObjectId? AsId()
		{
			ObjectId oid;
			return ObjectId.TryParse(_apiKey, out oid) ?
				oid :
				default(ObjectId?);
		}

		public bool IsMissing { get { return missing(_apiKey); } }

		public static ApiKey ExtractFrom(IHttpRequest request)
		{
			string apiKey = request.Headers[ParameterName];
			if (missing(apiKey))
			{
				apiKey = request.QueryString[ParameterName];
			}
			return new ApiKey(apiKey);
		}

		private static bool missing(string apiKey)
		{
			return string.IsNullOrEmpty(apiKey);
		}
	}
}