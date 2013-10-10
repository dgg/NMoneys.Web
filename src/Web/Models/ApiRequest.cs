using System.ComponentModel.DataAnnotations;
using Microsoft.Web.Mvc;

namespace NMoneys.Web.Models
{
	public class ApiRequest
	{
		[Required, EmailAddress]
		public string Email { get; set; }
		public string RequestedApi { get; set; }
	}
}