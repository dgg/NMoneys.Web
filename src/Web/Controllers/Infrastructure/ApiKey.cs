using MongoDB.Bson;

namespace NMoneys.Web.Controllers.Infrastructure
{
	public class ApiKey
	{
		private readonly ObjectId _oid;

		private ApiKey(ObjectId oid)
		{
			_oid = oid;
		}

		public ObjectId AsId()
		{
			return _oid;
		}
		
		public static ApiKey GenerateNew()
		{
			return new ApiKey(ObjectId.GenerateNewId());
		}

		public static ApiKey Parse(string toBeParsed)
		{
			ObjectId oid = ObjectId.Parse(toBeParsed);
			return new ApiKey(oid);
		}

		public static bool CanParse(string s)
		{
			ObjectId id;
			bool canParse = ObjectId.TryParse(s, out id);
			return canParse;
		}

		public override string ToString()
		{
			return _oid.ToString();
		}
	}
}