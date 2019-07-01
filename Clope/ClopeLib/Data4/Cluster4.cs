using System;
using System.Collections.Generic;

namespace ClopeLib.Data
{
	public class Cluster4 : ICluster
	{
		// static
		static int latestId = 1;

		public static void ResetId() => latestId = 1;



		// field
		public int Id { get; }

		public float Repulsion { get; }

		public int Area { get; private set; }

		public bool IsEmpty => Transactions.Count == 0;

		public int TransactionsCount => Transactions.Count;

		public int Width { get => attributesLinksCounts.Count; }

		double currentCost;



		// field
		public List<ITransaction> Transactions { get; } = new List<ITransaction>();

		readonly Dictionary<int, int> attributesLinksCounts = new Dictionary<int, int>();



		// init
		public Cluster4(float repulsion)
		{
			Repulsion = repulsion;
			Id = latestId++;
			Area = 0;
		}



		// for preview
		public int GetCount(int link) => attributesLinksCounts.ContainsKey(link) ? attributesLinksCounts[link] : 0;



		// RecalcCurrentCost, Occurrence, Add, Remove
		void RecalcCurrentCost() => currentCost = Area * TransactionsCount / Math.Pow(Width, Repulsion);

		public int Occurrence(int link) => attributesLinksCounts.TryGetValue(link, out int val) ? val : 0;

		public void Add(ITransaction t)
		{
			if (!Transactions.Contains(t))
				Transactions.Add(t);
			AlterItems(t, 1);
			RecalcCurrentCost();
		}

		public void Remove(ITransaction t)
		{
			Transactions.Remove(t);
			AlterItems(t, -1);
			RecalcCurrentCost();
		}



		// private: AlterItems
		void AlterItems(ITransaction t, int value)
		{
			foreach (var links in t.Links)
			{
				ChangeLinksCount(links, value);
				Area += value;
			}
		}



		void ChangeLinksCount(int link, int by)
		{
			if (attributesLinksCounts.ContainsKey(link))
			{
				attributesLinksCounts[link] += by;
				if (attributesLinksCounts[link] == 0)
					attributesLinksCounts.Remove(link);
			}
			else
				attributesLinksCounts.Add(link, by);
		}



		// public: GetAddCost
		public double GetAddCost(ITransaction t)
		{
			// res = Snew+ * (TransCount + 1) / Power(newWidth, repulsion) - currentCost.
			var NewWidth = Width;
			foreach (var link in t.Links)
				if (Occurrence(link) == 0)
					NewWidth++;

			return (Area + t.Length) * (TransactionsCount + 1) / Math.Pow(NewWidth, Repulsion) - currentCost;
		}
	}
}
