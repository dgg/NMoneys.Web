using System.ComponentModel.DataAnnotations;

namespace NMoneys.Web.Models
{
	public class Snapshot
	{
		public Snapshot(Currency currency)
		{
			NumericCode = currency.PaddedNumericCode;
			AlphabeticCode = currency.AlphabeticCode;
			EnglishName = currency.EnglishName;
			NativeName = currency.NativeName;
			Symbol = currency.Symbol;
			Obsolete = currency.IsObsolete;
		}

		public string NumericCode { get; set; }

		public string AlphabeticCode { get; set; }

		[Display(Description = "English Name")]
		public string EnglishName { get; set; }

		[Display(Description = "Native Name")]
		public string NativeName { get; set; }

		public string Symbol { get; set; }

		public bool Obsolete { get; set; }
	}
}