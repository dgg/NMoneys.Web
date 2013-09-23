using NMoneys;

public class Money_Arithmetic
{
	public void Test()
	{
		var three = new Money(3m);
		var two = new Money(2m);
		Money ten = three.Plus(three).Plus(two) + two;
		Money oneOwed = two - three;
		Money one = oneOwed.Abs();

		// extended operations
		Money alsoTwo = two.Perform(ten, (amt1, amt2) => amt1 % amt2);
		three = new Money(3.9m).Perform(System.Math.Floor);
	}
}
