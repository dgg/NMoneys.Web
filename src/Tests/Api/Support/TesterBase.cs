using System;

namespace Tests.Api.Support
{
	public abstract class TesterBase
	{
		private TestHost _host;
		protected TestHost Host { get { return _host; } }

		private readonly Uri _baseUrl = new Uri("http://localhost:8081/");
		public Uri BaseUrl { get { return _baseUrl; } }

		protected void StartHost()
		{
			_host = new TestHost();
			_host.Init();

			_host.Start(_baseUrl.ToString());
		}

		protected void ShutdownHost()
		{
			_host.Stop();
			_host.Dispose();
			_host = null;
		}


		public TesterBase Replacing<T>(T dependency)
		{
			_host.Register(dependency);
			return this;
		}

		protected Uri Urifor(string restRelativeUri)
		{
			return new Uri(BaseUrl, restRelativeUri);
		}

		protected string UrlFor(string restRelativeUriTemplate, params string[] args)
		{
			string restRelativeUri = restRelativeUriTemplate;
			if (args != null && args.Length > 0)
			{
				restRelativeUri = string.Format(restRelativeUriTemplate, args);
			}

			return Urifor(restRelativeUri).ToString();
		}
	}
}