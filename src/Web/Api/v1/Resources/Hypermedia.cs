using System;
using System.Linq;
using NMoneys.Web.Api.v1.Messages.Hypermedia;
using NMoneys.Web.Api.v1.Datatypes.Hypermedia;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using dtFormat = NMoneys.Web.Api.v1.Messages.Hypermedia.Format;
using reqCurrencies = NMoneys.Web.Api.v1.Messages.Hypermedia.Currencies;
using reqCurrency = NMoneys.Web.Api.v1.Messages.Hypermedia.Currency;

namespace NMoneys.Web.Api.v1.Resources
{
	public class Hypermedia : Service, IOptions<Root>, IOptions<reqCurrencies>, IOptions<reqCurrency>, IOptions<dtFormat>
	{
		public object Options(Root request)
		{
			var response = new RootResponse
			{
				_links = new[]
				{
					Link.Self(request),
					new Link("currencies", new Messages.Currencies(), "GET")
				}
			};

			return response;
		}

		public object Options(reqCurrencies request)
		{
			var currencyLinks = Currency.FindAll()
				.OrderBy(c => c.AlphabeticCode, StringComparer.OrdinalIgnoreCase)
				.Where(c => !c.IsObsolete)
				.Select(c => new Link("currency", new Messages.Currency { IsoCode = c.IsoCode }, "GET"));

			var response = new CurrenciesResponse
			{
				_Links = new[]
				{
					Link.Self(request), 
					Link.Self(new Messages.Currencies(), "GET")
				}.Concat(
					currencyLinks)
				.ToArray()
			};

			return response;
		}

		public object Options(reqCurrency request)
		{
			var response = new CurrencyResponse
			{
				_Links = new[]
				{
					Link.Self(request),
					Link.Self(new Messages.Currency{IsoCode = request.IsoCode}, "GET"),
					Link.Parent(new Messages.Currencies(), "GET"),
					new Link("format", new Messages.Format{ IsoCode = request.IsoCode }, "GET"), 
					new Link("format", new Messages.Format{ IsoCode = request.IsoCode }, "POST")
				}
			};

			return response;
		}

		public object Options(dtFormat request)
		{
			var response = new CurrencyResponse
			{
				_Links = new[]
				{
					Link.Self(request),
					Link.Self(new Messages.Format{ IsoCode = request.IsoCode, Amount = request.Amount}, "GET"),
					Link.Self(new Messages.Format{ IsoCode = request.IsoCode, Amount = request.Amount}, "POST"),
					Link.Parent(new Messages.Currency{IsoCode = request.IsoCode}, "GET")
				}
			};

			return response;
		}
	}
}