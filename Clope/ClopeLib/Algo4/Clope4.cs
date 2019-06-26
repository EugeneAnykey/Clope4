using System.Collections.Generic;
using System.Linq;
using ClopeLib.Data4;
using ClopeLib.Helpers;
using EugeneAnykey.DebugLib.Loggers;

namespace ClopeLib.Algo4
{
	public class Clope4 : IClustering
	{
		const int maxSteps = 3;
		const float specThreshold = 0.01f;



		// Logger
		static ILogger logger;
		public static ILogger Logger { get => logger; set => logger = value ?? new ConsoleLogger(); }



		int stepIndex;
		public int LatestStepIndex => stepIndex;

		int stepChanges = 0;


		// field
		float repulsion;
		public float Repulsion
		{
			get { return repulsion; }
			set { needSpecify |= Utils.TrySetValue(ref repulsion, value, 1, 5); }
		}



		bool needSpecify;

		public List<ICluster> Clusters { get; private set; }
		public List<ITransaction> Transactions { get; private set; }
		Dictionary<ITransaction, ICluster> keys;
		readonly Queue<ITransaction> newTrans;



		// init
		public Clope4()
		{
			Clusters = new List<ICluster>();
			newTrans = new Queue<ITransaction>();
			Transactions = new List<ITransaction>();
			keys = new Dictionary<ITransaction, ICluster>();

			Clear();
		}



		// IClustering: AddNewTransactions
		public void AddNewTransactions(ITransaction[] newTransactions)
		{
			// check for Unique here is not right
			foreach (var t in newTransactions)
			{
				newTrans.Enqueue(t);
			}
		}



		// IClustering: Clear
		public void Clear()
		{
			Clusters.Clear();
			newTrans.Clear();
			Transactions.Clear();
			keys.Clear();

			needSpecify = false;
			Cluster4.ResetId();
		}



		// IAlgo: Run
		public void Run()
		{
			stepIndex = 0;

			while (newTrans.Count > 0)
			{
				Start();
			}

			while (needSpecify && stepIndex++ < maxSteps)
			{
				Specify();
			}

			RemoveEmptyClusters();
		}



		// Start
		void Start()
		{
			needSpecify = false;
			stepChanges = 0;

			while (newTrans.Count > 0)
			{
				var t = newTrans.Dequeue();
				if (IsUnique(t))
				{
					PlaceIntoCluster(t);
					stepChanges++;
				}
			}

			needSpecify = NeedSpec(stepChanges);
		}



		// Specify
		bool Specify()
		{
			needSpecify = false;
			stepChanges = 0;

			foreach (var t in Transactions)
				if (SpecifyCluster(t))
					stepChanges++;

			return needSpecify = NeedSpec(stepChanges);
		}



		bool NeedSpec(int changesCount) => ((double)stepChanges / Transactions.Count) > specThreshold;



		// private: RemoveEmptyClusters
		void RemoveEmptyClusters() => Clusters = (from c in Clusters where !c.IsEmpty select c).ToList();

		bool IsUnique(ITransaction t) => !keys.ContainsKey(t);



		#region private: PlaceIntoCluster, SpecifyCluster.
		void PlaceIntoCluster(ITransaction t)
		{
			ICluster bestCluster = null;

			double maxCost = 0;

			foreach (ICluster c in Clusters)
			{
				double addCost = c.AddCost(t);
				if (maxCost < addCost)
				{
					maxCost = addCost;
					bestCluster = c;
				}
			}

			if (bestCluster == null)
				Clusters.Add(bestCluster = new Cluster4(repulsion));

			bestCluster.Add(t);
			Transactions.Add(t);
			keys.Add(t, bestCluster);
		}



		bool SpecifyCluster(ITransaction t)
		{
			const double minCostToStart = 0;// 0.5f;
			
			var currentCluster = keys[t];
			var bestCluster = currentCluster;
			//ICluster bestCluster = null;
			double maxCost = minCostToStart;
			double remCost = currentCluster.RemoveCost(t);

			foreach (ICluster c in Clusters)
			{
				if (c == currentCluster) // except active (current) cluster.  
					continue;

				double addCost = c.AddCost(t);
				if (maxCost < addCost + remCost)
				{
					// [?] remCost + or - [?]
					maxCost = addCost + remCost;
					bestCluster = c;
				}
			}

			//if (maxCost > 0) {	// bestCluster != activeCluster.
			if (bestCluster != null && bestCluster != currentCluster)
			{
				if (bestCluster.IsEmpty)
					Clusters.Add(new Cluster4(Repulsion));

				currentCluster.Remove(t);
				bestCluster.Add(t);

				//keys[t] = currentCluster;    // [!]
				keys[t] = bestCluster;

				return true;
			}
			return false;
		}
		#endregion
	}
}
