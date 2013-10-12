using System.ComponentModel.DataAnnotations;

namespace NMoneys.Web.Controllers.Infrastructure
{
	public class ApiKeyAttribute : ValidationAttribute
	{
		public ApiKeyAttribute(string errorMessage): base(errorMessage) { }

		public override bool IsValid(object value)
		{
			bool valid = ApiKey.CanParse(value as string);
			return valid;
		}
	}
}