using ServiceStack.Text;

namespace NMoneys.Web.Controllers.Infrastructure
{
	public class ConfirmationResponse
	{
		public string status { get; set; }

		// success members
		public string email { get; set; }
		public string reject_reason { get; set; }
		public string _id { get; set; }

		// error members
		public string name { get; set; }
		public string message { get; set; }

		internal bool IsRejected
		{
			get { return !string.IsNullOrWhiteSpace(reject_reason); }
		}

		internal bool HasId
		{
			get { return !string.IsNullOrWhiteSpace(_id); }
		}

		internal string Failure
		{
			get { return JsonSerializer.SerializeToString(this); }
		}
	}
}