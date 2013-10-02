using EasyHttp.Http;
using NUnit.Framework.Constraints;

namespace Tests.Api.v1.Resources.Support
{
	public class HeaderConstraint :  Constraint
	{
		private readonly string _name;
		private readonly Constraint _headerConstraint;

		public HeaderConstraint(string name, Constraint headerConstraint)
		{
			_name = name;
			_headerConstraint = headerConstraint;
		}

		public override bool Matches(object obj)
		{
			actual = obj;

			var current = (HttpResponse) obj;
			return _headerConstraint.Matches(@current.RawHeaders[_name]);
		}

		public override void WriteDescriptionTo(MessageWriter writer)
		{
			writer.Write("Response with header ");
			writer.WriteExpectedValue(_name);
			writer.Write(": ");
			_headerConstraint.WriteDescriptionTo(writer);
		}

		public override void WriteActualValueTo(MessageWriter writer)
		{
			var current = (HttpResponse)actual;
			writer.WriteValue(@current.RawHeaders[_name]);
		}
	}
}