using System;
using System.Net.Mail;

namespace NMoneys.Web.Controllers.Infrastructure
{
	public interface IApiKeyRepository
	{
		void SavePending(ApiKey toBeConfirmed, MailAddress keyOwner, DateTimeOffset requested);
		void Confirm(ApiKey toBeConfirmed, DateTimeOffset confirmed);
	}
}