using System;
using System.Linq;
using NMoneys.Web.Api.v1.Infrastructure.UrlWriting;
using NMoneys.Web.Api.v1.Messages.Discovery;
using NMoneys.Web.ApiModel.v1.Datatypes;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.WebHost.Endpoints;

namespace NMoneys.Web.Api.v1.Resources
{
	public class Discovery : Service,
		IOptions<DiscoverRoot>,
		IOptions<DiscoverCurrencies>,
		IOptions<DiscoverCurrency>,
		IOptions<DiscoverFormat>,
		IOptions<DiscoverMultiFormat>
	{
		public object Options(DiscoverRoot request)
		{
			IAppHost host = GetAppHost();

			var response = new DiscoveryResponse
			{
				_links = new[]
				{
					host.Self(request),
					host.Link("currencies", new Messages.Currencies(), "GET")
				}
			};

			return response;
		}

		public object Options(DiscoverCurrencies request)
		{
			IAppHost host = GetAppHost();

			var currencyLinks = Currency.FindAll()
				.OrderBy(c => c.AlphabeticCode, StringComparer.OrdinalIgnoreCase)
				.Where(c => !c.IsObsolete)
				.Select(c => host.Link("currency", new Messages.Currency { IsoCode = c.IsoCode }, "GET"));

			var response = new DiscoveryResponse
			{
				_links = new[]
				{
					host.Self(request), 
					host.Self(new Messages.Currencies(), "GET")
				}
				.Concat(currencyLinks)
				.Concat(new[]{ host.Link("format", new Messages.MultiFormat { Quantities = new []{new FormatableQuantity()}}, "POST") })
				.ToArray()
			};

			return response;
		}

		public object Options(DiscoverCurrency request)
		{
			IAppHost host = GetAppHost();

			var response = new DiscoveryResponse
			{
				_links = new[]
				{
					host.Self(request),
					host.Self(new Messages.Currency{IsoCode = request.IsoCode}, "GET"),
					host.Parent(new Messages.Currencies(), "GET"),
					host.Link("format", new Messages.Format{ IsoCode = request.IsoCode }, "GET")
				}
			};

			return response;
		}

		public object Options(DiscoverFormat request)
		{
			IAppHost host = GetAppHost();
			var response = new DiscoveryResponse
			{
				_links = new[]
				{
					host.Self(request),
					host.Self(new Messages.Format{ IsoCode = request.IsoCode, Amount = request.Amount}, "GET"),
					host.Parent(new Messages.Currency{IsoCode = request.IsoCode}, "GET")
				}
			};

			return response;
		}

		public object Options(DiscoverMultiFormat request)
		{
			IAppHost host = GetAppHost();
			var response = new DiscoveryResponse
			{
				_links = new[]
				{
					host.Self(request),
					host.Self(new Messages.MultiFormat { Quantities = new []{new FormatableQuantity()}}, "POST"),
					host.Parent(new Messages.Currencies(), "GET")
				}
			};

			return response;
		}
	}
}