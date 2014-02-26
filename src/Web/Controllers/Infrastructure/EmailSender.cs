using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Mail;
using ServiceStack.ServiceClient.Web;

namespace NMoneys.Web.Controllers.Infrastructure
{
	public class EmailSender : IEmailSender
	{
		public void SendConfirmation(MailAddress confirmer, ApiKey toBeConfirmed, Uri confirmationUrl)
		{
			string emailApiKey = ConfigurationManager.AppSettings["email_api_key"];

			var jsonClient = new JsonServiceClient("https://mandrillapp.com/api/1.0/");

			jsonClient.PostAsync<ConfirmationResponse[]>("messages/send-template", 
				new ConfirmationRequest(emailApiKey, "api-key-confirmation", confirmer, toBeConfirmed, confirmationUrl),
					success =>
					{
						if (!isRejected(success) && hasId(success))
						{
							Debug.Write(success[0]._id);
						}
					},
					(error, ex) => Debug.Fail(error[0].Failure));
		 }

		private bool isRejected(ConfirmationResponse[] response)
		{
			return response[0].IsRejected;
		}

		private bool hasId(ConfirmationResponse[] response)
		{
			return response[0].HasId;
		}
	}
}