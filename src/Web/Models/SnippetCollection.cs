using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NMoneys.Web.Models
{
	public class SnippetCollection
	{
		public SnippetCollection(DirectoryInfo directory)
		{
			Snippets = directory.EnumerateFiles("*.cs")
				.Select(fi => new Snippet(fi))
				.OrderBy(s => s);
		}

		public IEnumerable<Snippet> Snippets { get; private set; }
	}
}