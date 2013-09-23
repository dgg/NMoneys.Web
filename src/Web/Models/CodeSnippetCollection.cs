using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NMoneys.Web.Models
{
	public class CodeSnippetCollection
	{
		public CodeSnippetCollection(DirectoryInfo quickStart,
			DirectoryInfo codeProject,
			DirectoryInfo codeProject_Exchange)
		{
			QuickStart = codeSnippets(quickStart);
			CodeProject = codeSnippets(codeProject);
			CodeProject_Exchange = codeSnippets(codeProject_Exchange);
		}

		private IEnumerable<CodeSnippet> codeSnippets(DirectoryInfo directory)
		{
			return directory.EnumerateFiles("*.cs")
				.Select(fi => new CodeSnippet(fi))
				.OrderBy(s => s);
		}

		public IEnumerable<CodeSnippet> QuickStart { get; private set; }
		public IEnumerable<CodeSnippet> CodeProject { get; private set; }
		public IEnumerable<CodeSnippet> CodeProject_Exchange { get; private set; }
	}
}