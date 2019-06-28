#define DEL

#if !DEL

namespace ClopeLib.Data
{
	public class Cluster4
	{
		public double RemoveCost(ITransaction t)
		{
			// res = Snew- * (TransCount - 1) / Power(newWidth, repulsion) - currentCost.
			var NewWidth = Width;
			foreach (var link in t.Links)
				if (Occurrence(link) > 0)
					NewWidth--;

			return (Area - t.Length) * (TransactionsCount - 1) / Math.Pow(NewWidth, Repulsion) - currentCost;
		}
	}
}



namespace ClopeLib.Algo
{
	public class Clope4
	{
		bool SpecifyCluster(ITransaction t)
		{
			var currentCluster = keys[t];
			ICluster bestCluster = currentCluster;
			double remCost = currentCluster.RemoveCost(t);

			double maxCost = 0;

			foreach (ICluster c in Clusters)
			{
				if (c == currentCluster)
				{
					continue;
				}
				
				//times++;

				double addCost = c.AddCost(t);
				if (maxCost < addCost - remCost)
				{
					maxCost = addCost - remCost;
					bestCluster = c;
				}
			}

			if (bestCluster != null && bestCluster != currentCluster)
			{
				if (bestCluster.IsEmpty)
					Clusters.Add(new Cluster4(Repulsion));

				currentCluster.Remove(t);
				bestCluster.Add(t);

				keys[t] = bestCluster;

				return true;
			}
			return false;
		}
	}
}

#endif
