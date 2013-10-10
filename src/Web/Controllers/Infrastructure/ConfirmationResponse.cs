namespace NMoneys.Web.Controllers.Infrastructure
{
	public class ConfirmationResponse
	{
		public string email { get; set; }
		public string status { get; set; }
		public string reject_reason { get; set; }
		public string _id { get; set; }
	}
}