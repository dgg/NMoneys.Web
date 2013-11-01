using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace NMoneys.Web.Models
{
	public class Snapshot
	{
		public Snapshot(Currency currency)
		{
			PaddedNumericCode = currency.PaddedNumericCode;
			AlphabeticCode = currency.AlphabeticCode;
			EnglishName = currency.EnglishName;
			NativeName = currency.NativeName;
			Symbol = currency.Symbol;
			Obsolete = currency.IsObsolete;

			Details = new Detail(currency);
		}

		[Display(Description = "Padded Numeric Code")]
		public string PaddedNumericCode { get; set; }

		[Display(Description = "Alphabetic Code")]
		public string AlphabeticCode { get; set; }

		[Display(Description = "English Name")]
		public string EnglishName { get; set; }

		[Display(Description = "Native Name")]
		public string NativeName { get; set; }

		public string Symbol { get; set; }

		public bool Obsolete { get; set; }

		public Detail Details { get; set; }

		public class Detail
		{

			public class Formatted
			{
				public Formatted(Money money)
				{
					Positive = money.ToString();
					Negative = (-money).ToString();
				}

				public string Positive { get; set; }
				public string Negative { get; set; }
			}

			decimal integral = 42m, fractional = 42.33333m, bigIntegral = 123456789m, bigFractional = 123456789.54321m;

			public Detail(Currency currency)
			{
				NumericCode = currency.NumericCode.ToString(CultureInfo.InvariantCulture);

				Money integer = new Money(integral, currency),
				fraction = new Money(fractional, currency),
				bigInteger = new Money(bigIntegral, currency),
				bigFraction = new Money(bigFractional, currency);

				Integral = new Formatted(integer);
				Fractional = new Formatted(fraction);
				BigIntegral = new Formatted(bigInteger);
				BigFractional = new Formatted(bigFraction);
			}

			[Display(Description = "Numeric Code")]
			public string NumericCode { get; set; }

			public Formatted Integral { get; set; }

			public Formatted Fractional { get; set; }

			public Formatted BigIntegral { get; set; }

			public Formatted BigFractional { get; set; }
		}
	}
}