using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingHelper
{
	public static class PrimeNumber
	{
		// IsPrime
		public static bool IsPrime(int number)
		{
			if (number == 1) return false;
			if (number == 2) return true;

			var limit = Math.Ceiling(Math.Sqrt(number)); //hoisting the loop limit

			for (int i = 2; i <= limit; ++i)
			{
				if (number % i == 0) return false;
			}

			return true;
		}



		// GetList
		public static List<int> GetList(int count)
		{
			var res = new List<int> { 1, 2 };

			for (int i = 3; i < count; i += 2)
				if (IsPrime(i))
					res.Add(i);

			return res;
		}



		// Example
		public static void ShowExampleOnConsole()
		{
			var list = GetList(10000).Select(i => i.ToString()).ToArray();
			Console.WriteLine(string.Join(", ", list));
		}
	}
}
