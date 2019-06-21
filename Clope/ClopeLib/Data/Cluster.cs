using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ClopeLib.Helpers;

namespace ClopeLib.Data
{
	public class Cluster : ICluster
	{
		// static
		static int lastId = 1;
		public static void ResetId()
		{
			lastId = 1;
		}

		public int Id { get; }

		public int Width
		{
			get { return hash.Count; }
		}

		int square;
		public int Square
		{
			get { return square; }
		}

		public int TransactionsCount
		{   // C.N
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

		readonly Dictionary<string, int> hash = new Dictionary<string, int>();  // unique items (currently - strings) with occurence count.



		// init
		public Cluster(float repulsion)
		{
			Repulsion = repulsion;
			Id = lastId++;
		}



		#region private: RecalcCurrentCost.
		/// <summary>
		/// Calculates cluster's current cost after add/remove the transaction.
		/// </summary>
		void RecalcCurrentCost() => currentCost = Square * TransactionsCount / Math.Pow(Width, Repulsion);

		int GetSquare() => hash.Select(e => e.Value).Sum();
		#endregion



		#region interfaced
		public void Add(ITransaction t)
		{
			AddTransaction(t);
			AddItems(t);
			RecalcCurrentCost();
		}

		public int Occurrence(string s)
		{
			if (s == null)
				return 0;

			int val = 0;

			if (hash.ContainsKey(s))
			{
				hash.TryGetValue(s, out val);
			}

			return val;
		}

		public void Remove(ITransaction t)
		{
			RemoveTransaction(t);
			RemoveItems(t);
			RecalcCurrentCost();
		}
		#endregion



		#region private: (Add/Remove) (Transaction/Items).
		void AddTransaction(ITransaction t)
		{
			if (!trans.Contains(t))
				trans.Add(t);
			// else <-- this is currently not possible. Equal transactions are not allowed.
		}

		void RemoveTransaction(ITransaction t)
		{
			if (trans.Contains(t))
				trans.Remove(t);
		}

		void AddItems(ITransaction t)
		{
			foreach (string s in t.Items)
				if (s != null)
					ChangeObjectCount(s, 1);

			square = GetSquare();
		}

		void RemoveItems(ITransaction t)
		{
			foreach (string s in t.Items)
				if (s != null)
					ChangeObjectCount(s, -1);

			square = GetSquare();
		}
		#endregion



		#region private: ChangeObjectCount.
		internal void ChangeObjectCount(string s, int by)
		{
			if (s == null)
				return;

			if (hash.ContainsKey(s))
				hash[s] += by;
			else
				hash.Add(s, by);
		}
		#endregion



		#region public: AddCost, RemoveCost.
		public double AddCost(ITransaction t)
		{
			// res = Snew+ * (TransCount + 1) / Power(newWidth, repulsion) - currentCost.
			var NewWidth = Width;
			foreach (string s in t.Items)
				if (Occurrence(s) == 0)
					NewWidth++;

			return (Square + t.Length) * (TransactionsCount + 1) / Math.Pow(NewWidth, Repulsion) - currentCost;
		}

		public double RemoveCost(ITransaction t)
		{
			// res = Snew- * (TransCount - 1) / Power(newWidth, repulsion) - currentCost.
			var NewWidth = Width;
			foreach (string s in t.Items)
				if (Occurrence(s) == 1)
					NewWidth--;

			return (Square - t.Length) * (TransactionsCount - 1) / Math.Pow(NewWidth, Repulsion) - currentCost;
		}
		#endregion



		#region public: MakeOutput.
		public string OutputContent()
		{
			const string startMsg = " <* Cluster <{0}>:\r\n";
			const string mask = "\t{0}\r\n";
			const string endMsg = " *>\r\n";
			string name = Id.ToString();

			var ss = new StringBuilder();

			ss.AppendFormat(startMsg, name);

			foreach (var t in trans)
				ss.AppendFormat(mask, ConvertHelper.ConvertToString(t.Items, ", "));

			ss.AppendFormat(endMsg, name);

			return ss.ToString();
		}
		#endregion
	}
}
