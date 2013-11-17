using System;
using System.Linq;
using NMoneys.Web.Api.v1.Messages.Hypermedia;
using NMoneys.Web.Api.v1.Datatypes.Hypermedia;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace NMoneys.Web.Api.v1.Resources
{
	/*public class Hypermedia : Service, IOptions<root>, IOptions<currencies>, IOptions<currency>, IOptions<format>
	{
		public object Options(root request)
		{
			var response = new rootResponse
			{
				_links = new[]
				{
					Link.Self(request),
					new Link("currencies", new Messages.Currencies(), "GET")
				}
			};

			return response;
		}

		public object Options(currencies request)
		{
			var currencyLinks = Currency.FindAll()
				.OrderBy(c => c.AlphabeticCode, StringComparer.OrdinalIgnoreCase)
				.Where(c => !c.IsObsolete)
				.Select(c => new Link("currency", new Messages.Currency { IsoCode = c.IsoCode }, "GET"));

			var response = new currenciesResponse
			{
				_links = new[]
				{
					Link.Self(request), 
					Link.Self(new Messages.Currencies(), "GET")
				}.Concat(
					currencyLinks)
				.ToArray()
			};

			return response;
		}

		public object Options(currency request)
		{
			var response = new currencyResponse
			{
				_links = new[]
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

		public object Options(format request)
		{
			var response = new currencyResponse
			{
				_links = new[]
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
	 [Route("/v1/currencies", "OPTIONS", Summary = "List current supported currencies.", Notes = "Obsolete currencies are not returned.")]
	[Api("List current supported currencies.")]
	public class currencies : IReturn<currenciesResponse> { }

	public class currenciesResponse : IReturn<currenciesResponse>
	{
		public Link[] _links { get; set; }
	}
	 * 
	 * [Route("/v1/currencies/{isoCode}", "OPTIONS", Summary = "Get detailed information about a currency.")]
	[Api("Get detailed information about a currency.")]
	public class currency : IReturn<currencyResponse>
	{
		[ApiMember(IsRequired = true, ParameterType = "path", Description = "Three-letter ISO code of the currency to return.")]
		public CurrencyIsoCode IsoCode { get; set; }
	}

	public class currencyResponse
	{
		public Link[] _links { get; set; }
	}
	 * 
	 * [Route("/v1/currencies/{isoCode}/format", "OPTIONS", Summary = "Allows formatting a monetary amount according to its currency.")]
	[Route("/v1/currencies/{isoCode}/format/{amount}", "OPTIONS", Summary = "Allows formatting a monetary amount according to its currency.")]
	[Api("Allows formatting a monetary amount according to its currency.")]
	public class format : IReturn<formatResponse>
	{
		[ApiMember(IsRequired = true, ParameterType = "path, body", Verb = "GET, POST",
			Description = "Three-letter ISO code of the currency to use for formatting.")]
		public CurrencyIsoCode IsoCode { get; set; }

		[ApiMember(IsRequired = true, ParameterType = "path, body", Verb = "GET, POST",
			Description = "Amount of the monetary quantity to be formatted.")]
		public decimal Amount { get; set; }
	}

	public class formatResponse
	{
		public Link[] _links { get; set; }
	}
	 * 
	 * [Route("/v1", "OPTIONS", Summary = "List operations available through the API.")]
	[Api("List operations available through the API.")]
	public class root : IReturn<rootResponse> { }

	public class rootResponse
	{
		public Link[] _links { get; set; }
	}
	 
	 */
}