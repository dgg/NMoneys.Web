using System;
using System.Linq;
using NMoneys.Web.Api.v1.Infrastructure.UrlWriting;
using NMoneys.Web.Api.v1.Messages.Hypermedia;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.WebHost.Endpoints;

namespace NMoneys.Web.Api.v1.Resources
{
	public class Hypermedia : Service, IOptions<root>, IOptions<currencies>, IOptions<currency>, IOptions<format>
	{
		public object Options(root request)
		{
			IAppHost host = GetAppHost();

			var response = new rootResponse
			{
				_links = new[]
				{
					host.Self(request),
					host.Link("currencies", new Messages.Currencies(), "GET")
				}
			};

			return response;
		}

		public object Options(currencies request)
		{
			IAppHost host = GetAppHost();

			var currencyLinks = Currency.FindAll()
				.OrderBy(c => c.AlphabeticCode, StringComparer.OrdinalIgnoreCase)
				.Where(c => !c.IsObsolete)
				.Select(c => host.Link("currency", new Messages.Currency { IsoCode = c.IsoCode }, "GET"));

			var response = new currenciesResponse
			{
				_links = new[]
				{
					host.Self(request), 
					host.Self(new Messages.Currencies(), "GET")
				}.Concat(
					currencyLinks)
				.ToArray()
			};

			return response;
		}

		public object Options(currency request)
		{
			IAppHost host = GetAppHost();

			var response = new currencyResponse
			{
				_links = new[]
				{
					host.Self(request),
					host.Self(new Messages.Currency{IsoCode = request.IsoCode}, "GET"),
					host.Parent(new Messages.Currencies(), "GET"),
					host.Link("format", new Messages.Format{ IsoCode = request.IsoCode }, "GET"), 
					host.Link("format", new Messages.Format{ IsoCode = request.IsoCode }, "POST")
				}
			};

			return response;
		}

		public object Options(format request)
		{
			IAppHost host = GetAppHost();
			var response = new currencyResponse
			{
				_links = new[]
				{
					host.Self(request),
					host.Self(new Messages.Format{ IsoCode = request.IsoCode, Amount = request.Amount}, "GET"),
					host.Self(new Messages.Format{ IsoCode = request.IsoCode, Amount = request.Amount}, "POST"),
					host.Parent(new Messages.Currency{IsoCode = request.IsoCode}, "GET")
				}
			};

			return response;
		}
	}
}