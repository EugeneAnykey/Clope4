using System;

namespace ClopeLib.Data
{
	public class Cluster4 : ICluster
	{
		// field
		public float Repulsion { get; }

		public int Area { get; private set; }

		public bool IsEmpty => TransactionsCount == 0;

		public int TransactionsCount { get; private set; }

		double currentCost;

		public int Width { get => counter.Positives; }

		readonly IIndexCounter counter = new IndexCounterAtArray();

		public int Occurrence(int link) => counter[link];



		// init
		public Cluster4(float repulsion)
		{
			Repulsion = repulsion;
			Area = 0;
		}



		void RecalcCurrentCost() => currentCost = Area * TransactionsCount / Math.Pow(Width, Repulsion);



		public void Add(ITransaction t)
		{
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
			// res = Snew+ * (TransCount + 1) / Power(newWidth, repulsion) - currentCost.
			var NewWidth = Width;
			foreach (var link in t.Links)
				if (counter[link] == 0)
					NewWidth++;

			return (Area + t.Length) * (TransactionsCount + 1) / Math.Pow(NewWidth, Repulsion) - currentCost;
		}
	}
}
