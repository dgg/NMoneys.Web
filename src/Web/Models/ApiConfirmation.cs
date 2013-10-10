using System.ComponentModel.DataAnnotations;
using Microsoft.Web.Mvc;

namespace NMoneys.Web.Models
{
	public class ApiConfirmation
	{
		[Required, EmailAddress]
		public string Email { get; set; }
		[Required]
		public string ApiKey { get; set; }
	}
}