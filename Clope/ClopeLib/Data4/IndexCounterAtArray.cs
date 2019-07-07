namespace ClopeLib.Data
{
	public class IndexCounterAtArray : IIndexCounter
	{
		int[] counter = new int[0];
		public int Positives { get; private set; } = 0;



		// interface
		public void Inc(int[] indicies)
		{
			foreach (var index in indicies)
			{
				if (index >= counter.Length)
					Expand(index);

				var temp = counter[index];
				counter[index]++;

				if (counter[index] == 1)
					Positives++;
			}
		}

		public void Dec(int[] indicies)
		{
			foreach (var index in indicies)
			{
				if (index >= counter.Length)
					Expand(index);

				var temp = counter[index];
				counter[index]--;

				if (counter[index] == 0)
					Positives--;
			}
		}



		public int this[int index] => 0 <= index && index < counter.Length ? counter[index] : 0;



		void Expand(int index)
		{
			var temp = new int[index + 1];
			counter.CopyTo(temp, 0);
			counter = temp;
		}
	}
}
