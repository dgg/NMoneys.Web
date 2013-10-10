using System;
using System.Net.Mail;

namespace NMoneys.Web.Controllers.Infrastructure
{
	public interface IEmailSender
	{
		void SendConfirmation(MailAddress confirmer, ApiKey toBeConfirmed, Uri confirmationUrl);
	}
}