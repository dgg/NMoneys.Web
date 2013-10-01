using NUnit.Framework;

namespace Tests.Api.Support
{
	public abstract class SingleHostPerTest: TesterBase
	{
		[SetUp]
		public void SetUp()
		{
			StartHost();
		}

		[TearDown]
		public void TearDown()
		{
			ShutdownHost();
		}
	}
}