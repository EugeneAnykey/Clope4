using System.Linq;

namespace TestingHelper.UnitTests
{
	public static class HasherCounter
	{
		public static int CountEqualsTableElems(HasherTestItem[] items)
		{
			int res = 0;

			var vec = items.OrderBy(h => h.Hash).ToArray();

			for (int i = 1; i < vec.Length; i++)
			{
				if (vec[i].Hash == vec[i - 1].Hash)
					res++;
			}

			return res;
		}
	}
}
