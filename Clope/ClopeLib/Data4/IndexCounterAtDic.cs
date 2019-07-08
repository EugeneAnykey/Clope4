using System.Collections.Generic;

namespace ClopeLib.Data
{
	public class IndexCounterAtDic : IIndexCounter
	{
		readonly Dictionary<int, int> counter = new Dictionary<int, int>();

		public int Positives => counter.Count;



		// interface
		public void Inc(int[] indicies)
		{
			const int incrementValue = 1;

			foreach (var index in indicies)
			{
				ChangeLinksCount(index, incrementValue);
			}
		}

		public void Dec(int[] indicies)
		{
			const int decrementValue = -1;

			foreach (var index in indicies)
			{
				ChangeLinksCount(index, decrementValue);
			}
		}



		public int this[int index] => counter.ContainsKey(index) ? counter[index] : 0;



		void ChangeLinksCount(int index, int by)
		{
			if (counter.ContainsKey(index))
			{
				counter[index] += by;
				if (counter[index] == 0)
					counter.Remove(index);
			}
			else
				counter.Add(index, by);
		}
	}
}
