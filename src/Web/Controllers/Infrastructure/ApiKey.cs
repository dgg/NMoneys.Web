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

		public override string ToString()
		{
			return _oid.ToString();
		}
	}
}