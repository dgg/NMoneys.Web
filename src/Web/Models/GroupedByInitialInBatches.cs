using System;
using System.Collections.Generic;
using System.Linq;

namespace NMoneys.Web.Models
{
	public class GroupedByInitialInBatches
	{
		public GroupedByInitialInBatches(IGrouping<char, Snapshot> group, int size)
		{
			Initial = group.Key;
			BatchedSnapshots = group.Batch(size);
		}
		public char Initial { get; private set; }
		public IEnumerable<Snapshot[]> BatchedSnapshots { get; private set; }

		public static IEnumerable<GroupedByInitialInBatches> Collection(IEnumerable<Snapshot> snapshots, Func<Snapshot, string> text, int batchSize)
		{
			return snapshots.GroupBy(i => text(i)[0])
				.OrderBy(g => g.Key, Comparer<char>.Default)
				.Select(g => new GroupedByInitialInBatches(g, batchSize));
		}
	}
}