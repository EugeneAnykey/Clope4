using System.Collections.Generic;
using System.Text;
using ClopeLib.Data;
using ClopeLib.Helpers;

namespace ClopeLib.Previews
{
	public class Previewer : IPreview
	{
		// field
		readonly List<ITransaction> transactions;
		readonly List<ICluster> clusters;

		string[] itemsNames;
		List<int[]> result;



		// init
		public Previewer(List<ITransaction> transactions, List<ICluster> clusters)
		{
			this.transactions = transactions;
			this.clusters = clusters;
		}



		#region public
		public void PrepareView(int transactionColumn)
		{
			// transactions:
			var diffs = GetDifferentItemsInColumn(transactions, transactionColumn);
			itemsNames = diffs.ToArray();
			// example: e 2500, p 4000, s 1500.

			// now clusters:
			var res = new List<int[]>();

			foreach (var c in clusters)
			{
				var tt = c.Transactions;
				res.Add(CountInColumn(tt, transactionColumn, itemsNames));
			}

			result = res;
		}

		public string Output()
		{
			const string tab = "\t";
			var sb = new StringBuilder();

			// header
			sb.Append("cluster\t");
			sb.AppendLine(string.Join(tab, itemsNames));
			sb.AppendLine("");

			// items
			for (int i = 0; i < result.Count; i++)
			{
				var line = string.Join(tab, result[i].ToStrings());
				sb.AppendLine($"{i + 1}{tab}{line}");
			}

			return sb.ToString();
		}
		#endregion



		// private: DifferentItemsInColumn.
		List<string> GetDifferentItemsInColumn(List<ITransaction> trans, int column)
		{
			var names = new List<string>();

			for (int i = 0; i < trans.Count; i++)
			{
				var t = trans[i];
				var s = t.Items[column];
				if (!names.Contains(s))
					names.Add(s);
			}

			return names;
		}



		// private: CountInColumn.
		int[] CountInColumn(List<ITransaction> trans, int column, string[] items)
		{
			var res = new int[items.Length];
			var p = -1;

			for (int i = 0; i < trans.Count; i++)
			{
				var t = trans[i];
				var s = t.Items[column];
				if ((p = items.Position(s)) > -1)
					res[p]++;
			}

			return res;
		}
	}
}
