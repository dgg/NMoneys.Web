using System;
using System.Linq;
using NMoneys.Web.ApiModel.v1.Datatypes;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace NMoneys.Web.Api.v1.Resources
{
	public class Currencies : Service,
		IGet<Messages.Currencies>,
		IGet<Messages.Currency>,
		IGet<Messages.Format>,
		IPost<Messages.MultiFormat>
	{
		public object Get(Messages.Currencies request)
		{
			CurrencySnapshot[] snapshots = Currency.FindAll()
				.OrderBy(c => c.AlphabeticCode, StringComparer.OrdinalIgnoreCase)
				.Select(c =>
				{
					var snapshot = new CurrencySnapshot
					{
						IsoCode = c.IsoCode,
						NumericCode = c.NumericCode,
						EnglishName = c.EnglishName,
						IsObsolete = c.IsObsolete ? true : default(bool?)
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

		public object Get(Messages.Format request)
		{
			var money = new Money(request.Amount, request.IsoCode);
			FormattedMoney formatted = format(money);
			return new Messages.FormatResponse
			{
				Money = formatted
			};
		}

		private FormattedMoney format(Money money)
		{
			var formatted = new FormattedMoney
			{
				Amount = money.Amount,
				IsoCode = money.CurrencyCode,
				AmountRepresentation = money.Format("{0:N}"),
				Representation = money.ToString()
			};
			return formatted;
		}


		public object Post(Messages.MultiFormat request)
		{
			FormattedMoney[] formatted = request.Quantities
				.Select(q => format(new Money(q.Amount, q.IsoCode)))
				.ToArray();
			return new Messages.MultiFormatResponse
			{
				Moneys = formatted
			};
		}
	}
}