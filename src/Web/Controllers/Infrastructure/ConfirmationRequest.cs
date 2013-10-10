using System;
using System.Net.Mail;

namespace NMoneys.Web.Controllers.Infrastructure
{
	public class ConfirmationRequest
	{
		public string key { get; private set; }
		public string template_name { get; private set; }

		public class TemplateContent
		{
			public string name { get; set; }
			public string content { get; set; }
		}
		public TemplateContent[] template_content { get; set; }

		public class Message
		{
			public Message(MailAddress confirmer, ApiKey key, Uri confirmUrl)
			{
				to = new[] { new To(confirmer) };
				global_merge_vars = new[]
				{
					new MergeVar("API_KEY", key.ToString()),
					new MergeVar("CONFIRM_URL", confirmUrl.ToString())
				};
			}

			public To[] to { get; set; }
			public class To
			{
				public string email { get; private set; }
				public string name { get; set; }

				public To(MailAddress email)
				{
					this.email = email.Address;
				}
			}

			public bool track_opens { get { return true; } }
			public bool track_clicks { get { return true; } }

			public class MergeVar
			{
				public MergeVar(string name, string content)
				{
					this.name = name;
					this.content = content;
				}

				public string name { get; set; }
				public string content { get; set; }
			}

			public MergeVar[] global_merge_vars { get; set; }
		}
		public Message message { get; private set; }

		public ConfirmationRequest(string apiKey, string templateName, MailAddress confirmer, ApiKey toBeConfirmed, Uri confirmationUrl)
		{
			key = apiKey;
			template_name = templateName;
			template_content = new TemplateContent[0];
			message = new Message(confirmer, toBeConfirmed, confirmationUrl);
		}
	}
}