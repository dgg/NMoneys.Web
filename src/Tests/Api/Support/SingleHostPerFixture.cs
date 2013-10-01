using NUnit.Framework;

namespace Tests.Api.Support
{
	public abstract class SingleHostPerFixture : TesterBase
	{
		[TestFixtureSetUp]
		public void SetUp()
		{
			StartHost();
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			ShutdownHost();
		}
	}
}