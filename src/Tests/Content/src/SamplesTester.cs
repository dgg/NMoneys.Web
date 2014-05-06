using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using Microsoft.CSharp;
using NUnit.Core;
using NUnit.Framework;

namespace Tests.Content.src
{
	[TestFixture]
	public class SamplesTester
	{
		[Test]
		public void QuickStart_Compiles()
		{
			CompilerResults results = compile(
				dir("Content\\src\\QuickStart"));

			Assert.That(results.Errors, Is.Empty);
		}

		[Test]
		public void CodeProject_Compiles()
		{
			CompilerResults results = compile(
				dir("Content\\src"),
				dir("Content\\src\\CodeProject"));

			Assert.That(results.Errors, Is.Empty);
		}

		[Test]
		public void CodeProject_Exchange_Compiles()
		{
			CompilerResults results = compile(
				dir("Content\\src"),
				dir("Content\\src\\CodeProject_Exchange"));

			Assert.That(results.Errors, Is.Empty);
		}

		[Test]
		public void CodeProject_TestsPass()
		{
			CompilerResults results = compile(false,
				dir("Content\\src"),
				dir("Content\\src\\CodeProject"));

			var testResults = test(results);

			Assert.That(testResults.IsFailure, Is.False);
		}

		[Test]
		public void CodeProject_Exchange_TestsPass()
		{
			CompilerResults results = compile(false,
				dir("Content\\src"),
				dir("Content\\src\\CodeProject_Exchange"));

			var testResults = test(results);

			Assert.That(testResults.IsFailure, Is.False);
		}

		private CompilerResults compile(params DirectoryInfo[] directories)
		{
			return compile(true, directories);
		}

		private CompilerResults compile(bool inMemory, params DirectoryInfo[] directories)
		{
			var provider = new CSharpCodeProvider();
			var parameters = new CompilerParameters();
			parameters.ReferencedAssemblies.Add("NMoneys.dll");
			parameters.ReferencedAssemblies.Add("NMoneys.Exchange.dll");
			parameters.ReferencedAssemblies.Add("NUnit.Framework.dll");
			parameters.ReferencedAssemblies.Add("System.Xml.dll");
			parameters.ReferencedAssemblies.Add("System.Core.dll");
			parameters.GenerateInMemory = inMemory;

			CompilerResults results = provider.CompileAssemblyFromFile(
				parameters,
				directories
					.SelectMany(d => d.EnumerateFiles("*.cs"))
					.Select(fi => fi.FullName)
					.ToArray());

			return results;
		}

		private DirectoryInfo dir(string path)
		{
			string fullPath = Path.Combine(TestContext.CurrentContext.TestDirectory, path);
			return new DirectoryInfo(fullPath);
		}

		private TestResult test(CompilerResults results)
		{
			CoreExtensions.Host.InitializeService();
			var package = new TestPackage(results.PathToAssembly);
			var builder = new TestSuiteBuilder();
			TestSuite suite = builder.Build(package);
			TestResult test = suite.Run(NullListener.NULL, TestFilter.Empty);

			return test;

		}
	}
}