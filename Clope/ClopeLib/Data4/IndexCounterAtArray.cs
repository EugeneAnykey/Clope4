#define fix

namespace ClopeLib.Data
{
	public class IndexCounterAtArray : IIndexCounter
	{
		int[] counter = new int[0];
		public int Positives { get; private set; } = 0;



		// interface
		public void Inc(int[] indicies)
		{
#if fix
			var max = Max(indicies);
			if (max >= counter.Length)
				Expand(max);
#endif

			foreach (var index in indicies)
			{
#if !fix
				if (index >= counter.Length)
					Expand(index);
#endif

				var temp = counter[index];
				counter[index]++;

				if (counter[index] == 1)
					Positives++;
			}
		}

		public void Dec(int[] indicies)
		{
#if fix
			var max = Max(indicies);
			if (max >= counter.Length)
				Expand(max);
#endif

			foreach (var index in indicies)
			{
#if !fix
				if (index >= counter.Length)
					Expand(index);
#endif

				var temp = counter[index];
				counter[index]--;

				if (counter[index] == 0)
					Positives--;
			}
		}



		public int this[int index] => 0 <= index && index < counter.Length ? counter[index] : 0;

		#if fix
		int Max(int[] values)
		{
			var max = values[0];
			for (int i = 1; i < values.Length; i++)
			{
				if (max < values[i])
					max = values[i];
			}
			return max;
		}
		#endif

		void Expand(int index)
		{
			var temp = new int[index + 1];
			counter.CopyTo(temp, 0);
			counter = temp;
		}
	}
}
