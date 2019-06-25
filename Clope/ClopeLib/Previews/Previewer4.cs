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

		List<int[]> result;
		IAttribute[] attributes;



		// init
		public Previewer4(IEnumerable<ITransaction> transactions, IEnumerable<ICluster> clusters, IAttributeStore store)
		{
			this.transactions = transactions;
			this.clusters = clusters;
			this.store = store;

			result = new List<int[]>();
		}



		// public
		public void MakePreview(int attributeColumn)
		{
			// Attributes:
			attributes = store.GetAttributes(attributeColumn);
			// example for column 3 with names h,g,f,d: [3h, 3g, 3f, 3d].

			// clusters ids.
			var cc = clusters.Select(c => c.Id).ToArray();

			// now clusters:
			foreach (var c in clusters)
			{
				int[] re = new int[attributes.Length];
				for (int i = 0; i < attributes.Length; i++)
				{
					 re[i] = CountAttributes(c, attributes[i]);
				}
				result.Add(re);
			}
		}



		public string GetOutput()
		{
			const string tab = "\t";
			var sb = new StringBuilder();

			// header
			sb.Append("cluster\t");
			sb.AppendLine(string.Join(tab, (from a in attributes select a.Name).ToArray()));
			sb.AppendLine("");

			// items
			for (int i = 0; i < result.Count; i++)
			{
				var line = string.Join(tab, result[i].ToStrings());
				sb.AppendLine($"{i + 1}{tab}{line}");
			}

			return sb.ToString();
		}



		int CountAttributes(ICluster cluster, IAttribute attribute) => (cluster as IPreviewable).GetCount(attribute.Id);
	}
}
