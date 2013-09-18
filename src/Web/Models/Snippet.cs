using System;
using System.IO;

namespace NMoneys.Web.Models
{
	public class Snippet : IComparable<Snippet>
	{
		private const char DOT = '.';

		private readonly byte _ordinal;

		public Snippet(FileInfo snippetSource)
		{
			string fileName = snippetSource.Name;
			byte.TryParse(fileName.Substring(0, fileName.IndexOf(DOT)), out _ordinal);

			Title = titleize(fileName);

			Url = new Uri("/content/src/" +  fileName, UriKind.Relative);
		}

		private string titleize(string fileName)
		{
			char dot = '.';
			string title = Path.GetFileNameWithoutExtension(fileName);
			int firstDot = fileName.IndexOf(DOT);
			title = fileName.Substring(firstDot + 1, title.Length - firstDot - 1);
			title = title.Replace('_', ' ');
			return title;
		}

		public Uri Url { get; private set; }

		public string Title { get; private set; }

		public int CompareTo(Snippet other)
		{
			if (other == null) return 1;

			return _ordinal.CompareTo(other._ordinal);
		}
	}
}