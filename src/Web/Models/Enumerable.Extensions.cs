using System.Collections.Generic;
using System.Linq;

namespace NMoneys.Web.Models
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T[]> Batch<T>(this IEnumerable<T> source, int size)
		{
			T[] bucket = null;
			var count = 0;

			foreach (var item in source)
			{
				if (bucket == null)
				{
					bucket = new T[size];
				}

				bucket[count++] = item;

				// The bucket is fully buffered before it's yielded
				if (count != size)
				{
					continue;
				}

				// Select is necessary so bucket contents are streamed too
				yield return bucket;

				bucket = null;
				count = 0;
			}

			// Return the last bucket with all remaining elements
			if (bucket != null && count > 0)
			{
				yield return bucket.Take(count).ToArray();
			}
		}
	}
}