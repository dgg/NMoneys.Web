using System.Configuration;
using System.Web;
using System.Web.Mvc;
using NMoneys.Web.Infrastructure.Web;

namespace NMoneys.Web.Controllers.Infrastructure
{
	public class ConfigurableRequireHttpsAttribute : RequireHttpsAttribute
	{
		private readonly SecureRequestSpecification _secureSpec;
		private readonly NonStandardSecurePortRedirector _redirector;

		public ConfigurableRequireHttpsAttribute()
		{
			_secureSpec = new SecureRequestSpecification();
			_redirector = new NonStandardSecurePortRedirector(ConfigurationManager.AppSettings["https_port"]);
		}

		protected override void HandleNonHttpsRequest(AuthorizationContext filterContext)
		{
			_redirector.RedirectIfPortConfigured(filterContext, base.HandleNonHttpsRequest);
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			HttpRequestBase request = filterContext.HttpContext.Request;
			if (!_secureSpec.IsSatisfiedBy(request))
			{
				HandleNonHttpsRequest(filterContext);
			}
		}
	}
}