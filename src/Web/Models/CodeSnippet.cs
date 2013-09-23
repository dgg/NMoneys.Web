using System;
using System.IO;

namespace NMoneys.Web.Models
{
	public class CodeSnippet : IComparable<CodeSnippet>
	{
		private const char DOT = '.';

		private readonly byte _ordinal;

		public CodeSnippet(FileInfo snippet)
		{
			_ordinal = parseOrdinal(snippet);
			Title = titleize(snippet);
			Url = extractUrl(snippet);
		}

		private byte parseOrdinal(FileInfo snippet)
		{
			string fileName = snippet.Name;
			byte ordinal;
			byte.TryParse(fileName.Substring(0, fileName.IndexOf(DOT)), out ordinal);
			return ordinal;
		}

		private string titleize(FileInfo snippet)
		{
			string fileName = snippet.Name;
			string title = Path.GetFileNameWithoutExtension(fileName);
			int firstDot = fileName.IndexOf(DOT);
			title = fileName.Substring(firstDot + 1, title.Length - firstDot - 1);
			title = title.Replace('_', ' ');
			return title;
		}

		private Uri extractUrl(FileInfo snippet)
		{
			string dir = snippet.DirectoryName;
			int lastSeparator = dir.LastIndexOf(Path.DirectorySeparatorChar);
			string parentDir = dir.Substring(lastSeparator + 1);
			var url = new Uri(
				string.Format("/content/src/{0}/{1}", parentDir, snippet.Name),
				UriKind.Relative);
			return url;
		}

		public Uri Url { get; private set; }

		public string Title { get; private set; }

		public int CompareTo(CodeSnippet other)
		{
			if (other == null) return 1;

			return _ordinal.CompareTo(other._ordinal);
		}
	}
}