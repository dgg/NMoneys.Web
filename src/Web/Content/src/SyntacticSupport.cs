using System.Reflection;
using NMoneys;
using NUnit.Framework;
using NUnit.Framework.Constraints;

// outside the CodeProject folder in order to not be a snippet itself
namespace NMoneys.Web.Content.src.CodeProject
{
	internal static class CurrencyIsoCodeExtensions
	{
		internal static ICustomAttributeProvider AsAttributeProvider(this CurrencyIsoCode code)
		{
			return typeof(CurrencyIsoCode).GetField(code.ToString());
		}
	}
}

public abstract class Assertive
{
	protected IResolveConstraint isMoneyWith(decimal amount, CurrencyIsoCode currency)
	{
		return Has.Property("Amount").EqualTo(amount).And.Property("CurrencyCode").EqualTo(currency);
	}

	protected IResolveConstraint isMoneyWith(decimal amount)
	{
		return Has.Property("Amount").EqualTo(amount);
	}
}

public partial class Parsing_Money : Assertive { }
public partial class Major_Minor : Assertive { }
public partial class Money_Arithmetic : Assertive { }