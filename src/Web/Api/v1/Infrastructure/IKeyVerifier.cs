namespace NMoneys.Web.Api.v1.Infrastructure
{
	public interface IKeyVerifier
	{
		bool Verify(ApiKey apiKey);
	}
}