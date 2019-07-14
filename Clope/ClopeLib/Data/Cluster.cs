﻿using System;
using ClopeLib.Helpers;

namespace ClopeLib.Data
{
	public class Cluster : ICluster
	{
		// field
		public int Area { get; private set; }

		public bool IsEmpty => TransactionsCount == 0;

		public int TransactionsCount { get; private set; }

		double currentCost;

		public int Width { get => counter.Positives; }

		readonly IIndexCounter counter = new IndexCounterAtArray();

		readonly MathPower mathPower;

		public int Occurrence(int link) => counter[link];



		// init
		public Cluster(ref MathPower mathPower)
		{
			this.mathPower = mathPower ?? throw new ArgumentNullException();
			Area = 0;
		}



		void RecalcCurrentCost() => currentCost = Area / mathPower[Width] * TransactionsCount;



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

			return NewWidth == 0 ?
				0 :
				(Area + t.Length) / mathPower[NewWidth] * (TransactionsCount + 1)  - currentCost;
		}

		public double GetRemCost(ITransaction t)
		{
			// res = -1 * (Snew- * (TransCount - 1) / Power(newWidth, repulsion) - currentCost).
			var NewWidth = Width;
			foreach (var link in t.Links)
				if (counter[link] == 1)
					NewWidth--;

			return NewWidth == 0 ?
				0 :
				currentCost - (Area - t.Length) / mathPower[NewWidth] * (TransactionsCount - 1);
		}
	}
}