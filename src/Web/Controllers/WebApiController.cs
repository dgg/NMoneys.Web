using System;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Routing;
using NMoneys.Web.Controllers.Infrastructure;
using NMoneys.Web.Models;

namespace NMoneys.Web.Controllers
{
	public class WebApiController : Controller
	{
		private readonly IEmailSender _sender;
		private readonly IApiKeyRepository _apiKeys;

		// poor man's injection. Good enough for this controller
		public WebApiController() : this(new EmailSender(), new ApiKeyRepository()) { }

		public WebApiController(IEmailSender sender, IApiKeyRepository apiKeys)
		{
			_sender = sender;
			_apiKeys = apiKeys;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Index(ApiRequest model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			sendConfirmationEmail(model);

			return View();
		}

		private void sendConfirmationEmail(ApiRequest model)
		{
			var confirmer = new MailAddress(model.Email);
			ApiKey key = ApiKey.GenerateNew();
			Uri confirmUrl = writeConfirmationUrl(key, confirmer);

			_sender.SendConfirmation(confirmer, key, confirmUrl);
			// TODO: make async
			_apiKeys.SavePending(key, confirmer, DateTimeOffset.UtcNow);
		}

		private Uri writeConfirmationUrl(ApiKey key, MailAddress confirmer)
		{
			var routeValues = new RouteValueDictionary(
				new ApiConfirmation
				{
					ApiKey = key.ToString(),
					Email = confirmer.Address,
				});
			Uri currentUrl = Request.Url;
			string confirmUrl = Url.Action("Confirm", "WebApi", routeValues, currentUrl.Scheme, currentUrl.Host);
			return new Uri(confirmUrl, UriKind.Absolute);
		}

		public ActionResult Confirm(ApiConfirmation model)
		{


			return View();
		}
	}
}