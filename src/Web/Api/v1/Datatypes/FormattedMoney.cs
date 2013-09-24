namespace NMoneys.Web.Api.v1.Datatypes
{
	public class FormattedMoney
	{
		public decimal Amount { get; set; }
		public CurrencyIsoCode IsoCode { get; set; }
		public string Representation { get; set; } 
		public string AmountRepresentation { get; set; }
	}
}