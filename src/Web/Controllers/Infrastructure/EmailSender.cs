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

			jsonClient.PostAsync<dynamic>("messages/send-template", 
				new ConfirmationRequest(emailApiKey, "api-key-confirmation", confirmer, toBeConfirmed, confirmationUrl),
					success =>
					{
						if (!rejected(success) && hasId(success))
						{
							Debug.Write(success[0]._id);
						}
					},
					(error, ex) => Debug.Fail(error.message));
		 }

		private bool rejected(dynamic response)
		{
			return !string.IsNullOrEmpty(response[0].reject_reason);
		}

		private bool hasId(dynamic response)
		{
			return !string.IsNullOrEmpty(response[0]._id);
		}
	}
}