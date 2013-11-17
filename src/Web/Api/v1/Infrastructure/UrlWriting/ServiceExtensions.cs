using System;
using NMoneys.Web.Api.v1.Datatypes.Hypermedia;
using ServiceStack.Common;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

namespace NMoneys.Web.Api.v1.Infrastructure.UrlWriting
{
	internal static class ServiceExtensions
	{
		public static Link Link(this IAppHost host, string rel, IReturn dto, string method)
		{
			string path = host.Config.ServiceStackHandlerFactoryPath;

			return new Link
			{
				Rel = rel, 
				Method = method,
				Href = new Uri("/".CombineWith(path, dto.ToUrl(method)), UriKind.Relative)
			};
		}

		public static Link Self(this IAppHost host, IReturn dto, string method)
		{
			return host.Link("self", dto, method);
		}

		public static Link Parent(this IAppHost host, IReturn dto, string method)
		{
			return host.Link("parent", dto, method);
		}

		public static Link Self(this IAppHost host, IReturn dto)
		{
			return host.Link("_self", dto, "OPTIONS");
		}
	}
}