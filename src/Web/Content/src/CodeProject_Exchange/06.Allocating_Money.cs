using NMoneys;
using NMoneys.Allocations;
using NMoneys.Extensions;

public class Allocating_Money
{
	public Allocating_Money()
	{
		Allocation fair = 40m.Eur().Allocate(4);
		// fair.IsComplete --> true
		// fair.Remainder --> 0€
		// fair --> < 10€, 10€, 10€ >

		Allocation unfair = 40m.Eur().Allocate(3, RemainderAllocator.LastToFirst);
		// unfair.IsComplete --> false
		// unfair.Remainder --> 0€
		// unfair --> < 13.33€, 13.33€, 13.33€, 13.34€ >

		var foemmelsConundrumSolution = .05m.Usd().Allocate(new RatioCollection(.3m, 0.7m));
		// foemmelsConundrumSolution --> < $0.02, $0.03 >

		var anotherFoemmelsConundrumSolution = .05m.Usd().Allocate(new RatioCollection(.3m, 0.7m), RemainderAllocator.LastToFirst);
		// anotherFoemmelsConundrumSolution --> < $0.01, $0.04 >
	}
}
