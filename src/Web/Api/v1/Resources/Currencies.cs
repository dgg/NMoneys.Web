using System;
using System.Linq;
using NMoneys.Web.Api.v1.Datatypes;
using NMoneys.Web.ApiModel.v1.Datatypes;
using ServiceStack.ServiceHost;

namespace NMoneys.Web.Api.v1.Resources
{
	public class Currencies : IService
	{
		public object Get(Messages.Currencies request)
		{
			CurrencySnapshot[] snapshots = Currency.FindAll()
				.OrderBy(c => c.AlphabeticCode, StringComparer.OrdinalIgnoreCase)
				.Where(c => !c.IsObsolete)
				.Select(c =>
				{
					var snapshot = new CurrencySnapshot
					{
						IsoCode = c.IsoCode,
						NumericCode = c.NumericCode,
						EnglishName = c.EnglishName,
					};
					return snapshot;
				})
				.ToArray();

			return new Messages.CurrenciesResponse
			{
				Snapshots = snapshots
			};
		}

		public object Get(Messages.Currency request)
		{
			CurrencyIsoCode code = request.IsoCode;
			Currency currency = Currency.Get(code);

			var detail = new CurrencyDetail
			{
				IsoCode = currency.IsoCode,
				NumericCode = currency.NumericCode,
				EnglishName = currency.EnglishName,
				NativeName = currency.NativeName,
				Symbol = currency.Symbol,
				IsObsolete = currency.IsObsolete
			};
			return new Messages.CurrencyResponse
			{
				Detail = detail
			};
		}

		/*public object Get(Messages.Format request)
		{
			return format(request);
		}*/

		public object Put(Messages.Format request)
		{
			return format(request);
		}

		private Messages.FormatResponse format(Messages.Format request)
		{
			var money = new Money(request.Amount, request.IsoCode);
			var formatted = new FormattedMoney
			{
				Amount = money.Amount,
				IsoCode = money.CurrencyCode,
				AmountRepresentation = money.Format("{0:N}"),
				Representation = money.ToString()
			};
			return new Messages.FormatResponse
			{
				Money = formatted
			};
		}
	}
}