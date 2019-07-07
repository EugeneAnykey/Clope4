﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClopeLib.Helpers;

namespace ClopeLib.Previews
{
	public class Previewer4
	{
		// field
		readonly IEnumerable<ITransaction> transactions;
		readonly IEnumerable<ICluster> clusters;
		readonly IAttributeStore store;

		List<int[]> resultForClusters;
		IAttribute[] attributes;
		int[] links;



		// init
		public Previewer4(IEnumerable<ITransaction> transactions, IEnumerable<ICluster> clusters, IAttributeStore store)
		{
			this.transactions = transactions;
			this.clusters = clusters;
			this.store = store;

			resultForClusters = new List<int[]>();

			attributes = new IAttribute[0];
			links = new int[0];
		}



		// public
		public void MakePreview(int attributeColumn)
		{
			attributes = store.GetAttributes(attributeColumn);
			links = store.GetAttributesLinks(attributeColumn);

			// now checking clusters:
			foreach (var c in clusters)
			{
				int[] counts = new int[links.Length + 2];
				for (int i = 0; i < links.Length; i++)
				{
					counts[i] = CountAttributes(c, links[i]);
				}

				UpdateLineSummary(ref counts);

				resultForClusters.Add(counts);
			}

			resultForClusters.Add(GetTotalSummary());
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



		// private
		void UpdateLineSummary(ref int[] counts)
		{
			var sum = counts.Sum();
			var count = counts.Where(co => co > 0).Count();
			counts[counts.Length - 2] = sum;
			counts[counts.Length - 1] = count;
		}

		int[] GetTotalSummary()
		{
			var summary = new int[links.Length + 2];
			foreach (var line in resultForClusters)
			{
				for (int i = 0; i < line.Length; i++)
				{
					summary[i] += line[i];
				}
			}
			return summary;
		}

		int CountAttributes(ICluster cluster, int attributeLink) => cluster.Occurrence(attributeLink);
	}
}
