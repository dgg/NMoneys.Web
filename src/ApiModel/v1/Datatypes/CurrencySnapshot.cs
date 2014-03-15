namespace NMoneys.Web.ApiModel.v1.Datatypes
{
	public class CurrencySnapshot
	{
		public CurrencyIsoCode IsoCode { get; set; }
		public short NumericCode { get; set; }
		public string EnglishName { get; set; }
		public bool? IsObsolete { get; set; }
	}
}