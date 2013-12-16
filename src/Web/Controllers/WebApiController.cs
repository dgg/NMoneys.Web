using System;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Routing;
using NMoneys.Web.Controllers.Infrastructure;
using NMoneys.Web.Models;

namespace NMoneys.Web.Controllers
{
	[ConfigurableRequireHttps]
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
			var model = new ApiRequest();
			return View(model);
		}

		[HttpPost]
		public ActionResult Index(ApiRequest model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var confirmer = new MailAddress(model.Email);
			ApiKey key = ApiKey.GenerateNew();

			sendConfirmationEmail(key, confirmer);
			storeRequest(key, confirmer);

			model.Requested = true;
			return View(model);
		}

		private void sendConfirmationEmail(ApiKey key, MailAddress confirmer)
		{
			Uri confirmUrl = writeConfirmationUrl(key, confirmer);

			_sender.SendConfirmation(confirmer, key, confirmUrl);
		}

		private void storeRequest(ApiKey key, MailAddress confirmer)
		{
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
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			ApiKey toBeConfirmed = ApiKey.Parse(model.ApiKey);
			// TODO: make async
			_apiKeys.Confirm(toBeConfirmed, DateTimeOffset.UtcNow);

			return View(model);
		}
	}
}