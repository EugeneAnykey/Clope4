﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EugeneAnykey.DebugLib.Loggers;

namespace ClopeLib.Data4
{
	public class Cluster4 : ICluster, IPreviewable
	{
		static readonly ILogger log = new FileLogger("cluster.log.txt");

		static int UnexpectedOnAdd = 0;
		static int UnexpectedOnRemove = 0;

		// static
		static int latestId = 1;
		public static void ResetId()
		{
			latestId = 1;
		}

		public int Id { get; }

		public int Width { get => attributesCounts.Count; }

		public int Area { get; private set; }

		public int TransactionsCount
		{
			// C.N
			get { return trans.Count; }
		}

		public bool IsEmpty
		{
			get { return trans.Count == 0; }
		}

		public float Repulsion { get; }

		double currentCost;



		// field 2
		readonly List<ITransaction> trans = new List<ITransaction>();
		public List<ITransaction> Transactions { get { return trans; } }

		readonly Dictionary<int, int> attributesCounts = new Dictionary<int, int>();



		// init
		public Cluster4(float repulsion)
		{
			Repulsion = repulsion;
			Id = latestId++;
		}



		// private: RecalcCurrentCost.
		/// <summary>
		/// Calculates cluster's current cost after add/remove the transaction.
		/// </summary>
		void RecalcCurrentCost() => currentCost = Area * TransactionsCount / Math.Pow(Width, Repulsion);



		int GetArea() => attributesCounts.Select(pair => pair.Value).Sum();



		// ICluster: Add, Occurrence, Remove
		public void Add(ITransaction t)
		{
			AddTransaction(t);
			AddItems(t);
			RecalcCurrentCost();
		}

		public int Occurrence(int id)
		{
			if (id < 0)
				return 0;

			int val = 0;

			if (attributesCounts.ContainsKey(id))
			{
				attributesCounts.TryGetValue(id, out val);
			}

			return val;
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
				// else <-- this is currently not possible. Equal transactions are not allowed.
				log.Write($"add unexpected {++UnexpectedOnAdd}");
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
				log.Write($"remove unexpected {++UnexpectedOnRemove}");
			}
		}

		void AddItems(ITransaction t)
		{
			foreach (var index in t.Links)
				ChangeObjectCount(index, 1);

			Area = GetArea();
		}

		void RemoveItems(ITransaction t)
		{
			foreach (var index in t.Links)
				ChangeObjectCount(index, -1);

			Area = GetArea();
		}
		#endregion



		// private: ChangeObjectCount.
		internal void ChangeObjectCount(int id, int by)
		{
			if (id < 0)
				return;

			if (attributesCounts.ContainsKey(id))
				attributesCounts[id] += by;
			else
				attributesCounts.Add(id, by);
		}



		#region public: AddCost, RemoveCost.
		public double AddCost(ITransaction t)
		{
			// res = Snew+ * (TransCount + 1) / Power(newWidth, repulsion) - currentCost.
			var NewWidth = Width;
			foreach (var i in t.Links)
				if (Occurrence(i) == 0)
					NewWidth++;

			return (Area + t.Length) * (TransactionsCount + 1) / Math.Pow(NewWidth, Repulsion) - currentCost;
		}

		public double RemoveCost(ITransaction t)
		{
			// res = Snew- * (TransCount - 1) / Power(newWidth, repulsion) - currentCost.
			var NewWidth = Width;
			foreach (var i in t.Links)
				if (Occurrence(i) == 1)
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



		public int GetCount(int id) => attributesCounts.ContainsKey(id) ? attributesCounts[id] : 0;
	}
}
