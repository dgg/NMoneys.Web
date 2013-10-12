using System.ComponentModel.DataAnnotations;
using Microsoft.Web.Mvc;
using NMoneys.Web.Controllers.Infrastructure;

namespace NMoneys.Web.Models
{
	public class ApiConfirmation
	{
		[Required, EmailAddress]
		public string Email { get; set; }
		[Required, ApiKey("The value is  not an API key.")]
		public string ApiKey { get; set; }
	}
}