using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EugeneAnykey.DebugLib.Loggers;

namespace ClopeLib.Data
{
	public class Cluster4 : ICluster, IPreviewable
	{
		// static
		static readonly ILogger log1 = new FileLogger("cluster.log1.txt");

		static int UnexpectedOnAdd = 0;
		static int UnexpectedOnRemove = 0;

		static int latestId = 1;

		public static void ResetId() => latestId = 1;



		// field
		public int Id { get; }

		public float Repulsion { get; }

		public int Area { get; private set; }

		public bool IsEmpty => trans.Count == 0;

		public int TransactionsCount => trans.Count;  // C.N

		public int Width { get => attributesLinksCounts.Count; }

		double currentCost;



		// field 2
		readonly List<ITransaction> trans = new List<ITransaction>();
		public List<ITransaction> Transactions { get { return trans; } }

		readonly Dictionary<int, int> attributesLinksCounts = new Dictionary<int, int>();



		// init
		public Cluster4(float repulsion)
		{
			Repulsion = repulsion;
			Id = latestId++;
			Area = 0;
		}



		// private: GetArea, RecalcCurrentCost.
		//int GetArea() => attributesLinksCounts.Select(pair => pair.Value).Sum();

		void RecalcCurrentCost() => currentCost = Area * TransactionsCount / Math.Pow(Width, Repulsion);



		// ICluster: Add, Occurrence, Remove
		public int Occurrence(int link) => attributesLinksCounts.TryGetValue(link, out int val) ? val : 0;

		public void Add(ITransaction t)
		{
			AddTransaction(t);
			AddItems(t);
			RecalcCurrentCost();
		}

		public void Remove(ITransaction t)
		{
			RemoveTransaction(t);
			RemoveItems(t);
			RecalcCurrentCost();
		}



		#region private: (Add/Remove) (Transaction/Items).
		void AddTransaction(ITransaction t)
		{
			if (!trans.Contains(t))
			{
				trans.Add(t);
			}
			else
			{
				log1.Write($"add unexpected {++UnexpectedOnAdd}");
			}
		}

		void RemoveTransaction(ITransaction t)
		{
			if (trans.Contains(t))
			{
				trans.Remove(t);
			}
			else
			{
				log1.Write($"remove unexpected {++UnexpectedOnRemove}");
			}
		}

		void AddItems(ITransaction t)
		{
			foreach (var links in t.Links)
			{
				ChangeLinksCount(links, 1);
				Area++;
			}
		}

		void RemoveItems(ITransaction t)
		{
			foreach (var links in t.Links)
			{
				ChangeLinksCount(links, -1);
				Area--;
			}
		}
		#endregion



		internal void ChangeLinksCount(int link, int by)
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



		#region public: AddCost, RemoveCost.
		public double AddCost(ITransaction t)
		{
			// res = Snew+ * (TransCount + 1) / Power(newWidth, repulsion) - currentCost.
			var NewWidth = Width;
			foreach (var link in t.Links)
				if (Occurrence(link) == 0)
					NewWidth++;

			return (Area + t.Length) * (TransactionsCount + 1) / Math.Pow(NewWidth, Repulsion) - currentCost;
		}



		public double RemoveCost(ITransaction t)
		{
			// res = Snew- * (TransCount - 1) / Power(newWidth, repulsion) - currentCost.
			var NewWidth = Width;
			foreach (var link in t.Links)
				if (Occurrence(link) > 0)
					NewWidth--;

			return (Area - t.Length) * (TransactionsCount - 1) / Math.Pow(NewWidth, Repulsion) - currentCost;
		}
		#endregion



		// IPreviewable:
		public string MakePreview()
		{
			const string separator = ", ";
			const string startMsg = " *** Cluster <{0}>:\r\n";
			const string mask = "\t{0}\r\n";
			string name = Id.ToString();

			var ss = new StringBuilder();

			ss.AppendFormat(startMsg, name);
			ss.AppendFormat(mask, string.Join(separator, trans));
			ss.AppendLine();

			return ss.ToString();
		}



		public int GetCount(int link) => attributesLinksCounts.ContainsKey(link) ? attributesLinksCounts[link] : 0;
	}
}
