#define cou

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

		public bool IsEmpty => TransactionsCount == 0;

		public int TransactionsCount { get; private set; }

		double currentCost;

#if cou
		public int Width { get => counter.Positives; }

		//readonly IIndexCounter counter = new IndexCounterAtDic();
		readonly IIndexCounter counter = new IndexCounterAtArray();

		public int Occurrence(int link) => counter[link];

#else
		
		public int Width { get => attributesLinksCounts.Count; }
		
		readonly Dictionary<int, int> attributesLinksCounts = new Dictionary<int, int>();
		
		public int Occurrence(int link) => attributesLinksCounts.TryGetValue(link, out int val) ? val : 0;
#endif



		// init
		public Cluster4(float repulsion)
		{
			Repulsion = repulsion;
			Id = latestId++;
			Area = 0;
		}



		void RecalcCurrentCost() => currentCost = Area * TransactionsCount / Math.Pow(Width, Repulsion);



		public void Add(ITransaction t)
		{
			TransactionsCount++;

#if cou
			Area += t.Links.Length;
			counter.Inc(t.Links);
#else
			AlterItems(t, 1);
#endif

			RecalcCurrentCost();
		}

		public void Remove(ITransaction t)
		{
			TransactionsCount--;

#if cou
			Area -= t.Links.Length;
			counter.Dec(t.Links);
#else
			AlterItems(t, -1);
#endif

			RecalcCurrentCost();
		}



#if !cou
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
#endif



		public double GetAddCost(ITransaction t)
		{
			// res = Snew+ * (TransCount + 1) / Power(newWidth, repulsion) - currentCost.
			var NewWidth = Width;
			foreach (var link in t.Links)
#if cou
				if (counter[link] == 0)
#else
				if (Occurrence(link) == 0)
#endif
					NewWidth++;

			return (Area + t.Length) * (TransactionsCount + 1) / Math.Pow(NewWidth, Repulsion) - currentCost;
		}
	}
}
