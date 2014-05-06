using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using Microsoft.CSharp;
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

		private CompilerResults compile(params DirectoryInfo[] directories)
		{
			var provider = new CSharpCodeProvider();
			var parameters = new CompilerParameters();
			parameters.ReferencedAssemblies.Add("NMoneys.dll");
			parameters.ReferencedAssemblies.Add("NMoneys.Exchange.dll");
			parameters.ReferencedAssemblies.Add("NUnit.Framework.dll");
			parameters.ReferencedAssemblies.Add("System.Xml.dll");
			parameters.ReferencedAssemblies.Add("System.Core.dll");
			parameters.GenerateInMemory = true;

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
	}
}