namespace NMoneys.Web.ApiModel.v1.Datatypes
{
	public class CurrencyDetail
	{
		public CurrencyIsoCode IsoCode { get; set; }
		public short NumericCode { get; set; }
		public string EnglishName { get; set; }
		public string NativeName { get; set; }
		public string Symbol { get; set; }
		public bool IsObsolete { get; set; }
	}
}