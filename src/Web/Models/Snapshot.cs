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

		public string PaddedNumericCode { get; set; }

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

			decimal integral = 42m, fractional = 42.33333m, bigIntegral = 123456789m, bigFractional = 123456789.54321m;

			public Detail(Currency currency)
			{
				NumericCode = currency.NumericCode.ToString(CultureInfo.InvariantCulture);

				Money integer = new Money(integral, currency),
				fraction = new Money(fractional, currency),
				bigInteger = new Money(bigIntegral, currency),
				bigFraction = new Money(bigFractional, currency);

				Integral = integer.ToString();
				NegativeIntegral = (-integer).ToString();

				Fractional = fraction.ToString();
				NegativeFractional = (-fraction).ToString();

				BigIntegral = bigInteger.ToString();
				BigNegativeIntegral = (-bigInteger).ToString();

				BigFractional = bigFraction.ToString();
				BigNegativeFractional = (-bigFraction).ToString();
			}

			public string NumericCode { get; set; }

			public string Integral { get; set; }

			public string NegativeIntegral { get; set; }

			public string Fractional { get; set; }

			public string NegativeFractional { get; set; }

			public string BigIntegral { get; set; }

			public string BigNegativeIntegral { get; set; }

			public string BigFractional { get; set; }

			public string BigNegativeFractional { get; set; }
		}
	}
}