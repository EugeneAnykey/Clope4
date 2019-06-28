using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClopeLib.Helpers;

namespace ClopeLib.Previews
{
	public class Previewer4 : IPreview
	{
		// field
		readonly IEnumerable<ITransaction> transactions;
		readonly IEnumerable<ICluster> clusters;
		readonly IAttributeStore store;

		List<int[]> resultForClusters;
		IAttribute[] attributes;



		// init
		public Previewer4(IEnumerable<ITransaction> transactions, IEnumerable<ICluster> clusters, IAttributeStore store)
		{
			this.transactions = transactions;
			this.clusters = clusters;
			this.store = store;

			resultForClusters = new List<int[]>();
		}



		// public
		public void MakePreview(int attributeColumn)
		{
			// Attributes:
			attributes = store.GetAttributes(attributeColumn);
			// example for column 3 with names h,g,f,d: [3h, 3g, 3f, 3d].

			// now clusters:
			foreach (var c in clusters)
			{
				int[] counts = new int[attributes.Length + 2];
				for (int i = 0; i < attributes.Length; i++)
				{
					 counts[i] = CountAttributes(c, attributes[i]);
				}

				// sum and count of line:
				var sum = counts.Sum();
				var count = counts.Where(co => co > 0).Count();
				counts[counts.Length - 2] = sum;
				counts[counts.Length - 1] = count;

				resultForClusters.Add(counts);
			}

			// summary line:
			var summary = new int[attributes.Length + 2];
			foreach (var line in resultForClusters)
			{
				for (int i = 0; i < line.Length; i++)
				{
					summary[i] += line[i];
				}
			}

			resultForClusters.Add(summary);
		}



		public string GetOutput()
		{
			const string tab = "\t";
			var sb = new StringBuilder();

			// header
			sb.Append("cluster\t");
			sb.Append(string.Join(tab, (from a in attributes select a.Name).ToArray()));
			sb.AppendLine("\tsummary\ttimes");
			sb.AppendLine("");

			// items
			for (int i = 0; i < resultForClusters.Count; i++)
			{
				var line = string.Join(tab, resultForClusters[i].ToStrings());
				var lineName = i < resultForClusters.Count - 1 ? $"{i + 1}" : "summary";
				sb.AppendLine($"{lineName}{tab}{line}");
			}

			return sb.ToString();
		}
		


		int CountAttributes(ICluster cluster, IAttribute attribute) => cluster.GetCount(attribute.Link);
	}
}
