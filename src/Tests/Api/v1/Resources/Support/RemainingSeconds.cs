using System.Collections.Generic;

namespace Tests.Api.v1.Resources.Support
{
	public class RemainingSeconds: IComparer<string>
	{
		public int Compare(string x, string y)
		{
			ushort xValue = ushort.Parse(x), yValue = ushort.Parse(y);

			return xValue.CompareTo(yValue);
		}

		public static RemainingSeconds InPeriod = new RemainingSeconds();
	}
}