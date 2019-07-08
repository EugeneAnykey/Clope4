using System.Collections.Generic;

namespace ClopeLib.Data
{
	public class IndexCounterAtList : IIndexCounter
	{
		List<int> counter = new List<int>();
		
		public int Positives { get; private set; } = 0;



		// interface
		public void Inc(int[] indicies)
		{
			var max = Max(indicies);
			if (max >= counter.Count)
				Expand(max);

			foreach (var index in indicies)
			{
				var temp = counter[index];
				counter[index]++;

				if (counter[index] == 1)
					Positives++;
			}
		}

		public void Dec(int[] indicies)
		{
			var max = Max(indicies);
			if (max >= counter.Count)
				Expand(max);

			foreach (var index in indicies)
			{
				var temp = counter[index];
				counter[index]--;

				if (counter[index] == 0)
					Positives--;
			}
		}



		public int this[int index] => 0 <= index && index < counter.Count ? counter[index] : 0;

		int Max(IEnumerable<int> values)
		{
			int max = 0;
			foreach (var item in values)
			{
				if (max < item)
					max = item;
			}
			return max;
		}

		void Expand(int index)
		{
			while (counter.Count <= index)
				counter.Add(0);
		}
	}
}
