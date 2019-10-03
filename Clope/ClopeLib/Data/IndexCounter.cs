using System.Collections.Generic;

namespace ClopeLib.Data
{
	public class IndexCounter : IIndexCounter
	{
		readonly Dictionary<int, int> counter = new Dictionary<int, int>(); // link = count



		public int Positives { get; private set; } = 0;

		public int this[int index] => counter.ContainsKey(index) ? counter[index] : 0;



		public void Inc(int[] indicies)
		{
			foreach (var index in indicies)
			{
				if (counter.ContainsKey(index))
				{
					counter[index] += 1;
				}
				else
				{
					counter.Add(index, 1);
					Positives++;    // i.e. changed from 0 to 1 only.
				}
			}
		}


		public void Dec(int[] indicies)
		{
			foreach (var index in indicies)
			{
				// counter[index] should be always >= 0
				if (counter.ContainsKey(index))
				{
					if (counter[index] == 1)
					{
						counter.Remove(index);
						Positives--;    // i.e. changed from 1 to 0 only.
					}
					else
					{
						counter[index] -= 1;
					}
				}
				else
				{
					throw new System.Exception("Very strange behavior");
				}
			}
		}
	}
}
