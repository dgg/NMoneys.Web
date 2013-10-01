using System;
using System.Globalization;

namespace NMoneys.Web.Api.v1.Infrastructure
{
	public class ThrottlingConfiguration
	{
		public ushort NumberOfRequests { get; set; }
		public TimeSpan Period { get; set; }

		public bool ThrottlingEnabled { get { return NumberOfRequests > default(ushort); } }

		public static ThrottlingConfiguration Empty()
		{
			return new ThrottlingConfiguration();
		}

		public string FormattedSeconds
		{
			get
			{
				string seconds = Period.TotalSeconds.ToString(CultureInfo.InvariantCulture);
				return seconds;
			}
		}

		public string ErrorMessage()
		{
			string message = string.Format("Too many requests. Only {0} allowed every {1} seconds.",
				NumberOfRequests,
				FormattedSeconds);
			return message;
		}
	}
}