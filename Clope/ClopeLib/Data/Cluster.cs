using System;
using ClopeLib.Helpers;

namespace ClopeLib.Data
{
	public class Cluster : ICluster
	{
		// field
		public long Area { get; private set; }

		public bool IsEmpty => TransactionsCount == 0;

		public long TransactionsCount { get; private set; }

		public int Width { get => counter.Positives; }

		readonly IIndexCounter counter =
			//new IndexCounterAtArray();
			new IndexCounter();

		double currentCost;
		readonly MathPower mathPower;



		// init
		public Cluster(MathPower mathPower)
		{
			this.mathPower = mathPower ?? throw new ArgumentNullException();
			Area = 0;
		}



		public int Occurrence(int link) => counter[link];

		void RecalcCurrentCost() => currentCost = Width != 0 ? Area / mathPower[Width] * TransactionsCount : 0;



		public void Add(ITransaction t)
		{
			if (t == null)
				throw new ArgumentNullException();

			TransactionsCount++;

			Area += t.Links.Length;
			counter.Inc(t.Links);

			RecalcCurrentCost();
		}

		public void Remove(ITransaction t)
		{
			TransactionsCount--;

			Area -= t.Links.Length;
			counter.Dec(t.Links);

			RecalcCurrentCost();
		}



		public double GetAddCost(ITransaction t)
		{
			if (t == null || t.Length == 0)
				return currentCost;

			// res = Snew+ * (TransCount + 1) / Power(newWidth, repulsion) - currentCost.
			var NewWidth = Width;
			foreach (var link in t.Links)
				if (counter[link] == 0)
					NewWidth++;

			return (Area + t.Length) / mathPower[NewWidth] * (TransactionsCount + 1) - currentCost;
		}

		public double GetRemCost(ITransaction t)
		{
			// res = -1 * (Snew- * (TransCount - 1) / Power(newWidth, repulsion) - currentCost).
			var NewWidth = Width;
			foreach (var link in t.Links)
				if (counter[link] == 1)
					NewWidth--;

			return NewWidth == 0 ?
				currentCost :
				currentCost - (Area - t.Length) / mathPower[NewWidth] * (TransactionsCount - 1);
		}
	}
}
