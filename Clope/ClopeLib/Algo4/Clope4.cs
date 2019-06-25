using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClopeLib.Data4;
using ClopeLib.Helpers;
using EugeneAnykey.DebugLib.Loggers;

namespace ClopeLib.Algo4
{
	public class Clope4 : IAlgo
	{
		const int maxSteps = 10;



		// Logger
		static ILogger logger;
		public static ILogger Logger { get => logger; set => logger = value ?? new ConsoleLogger(); }



		int stepIndex;



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

		readonly Queue<ITransaction> newTrans;

		Dictionary<ITransaction, ICluster> keys;
		readonly bool doNotCheckForUniques;

		readonly AttributeStoreAtList store = new AttributeStoreAtList();



		// init
		public Clope4()
		{
			Clusters = new List<ICluster>();
			newTrans = new Queue<ITransaction>();
			Transactions = new List<ITransaction>();
			keys = new Dictionary<ITransaction, ICluster>();

			doNotCheckForUniques = true;

			Clear();
		}



		// IClustering: AddNewTransactions
		public void AddNewTransactions(ITransaction[] newTransactions)
		{
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

			stepIndex = 0;
			//CurrentStepName = "Just initialized.";
			needSpecify = false;
			Cluster4.ResetId();
		}



		// CurrentInfo, CurrentOutput
		public string CurrentInfo()
		{
			const string transactionsInfoMask = "Transactions: {0}.\r\n";
			const string clustersInfoMask = "Clusters: {0}.\r\n";
			const string keysInfoMask = "Keys: {0}.\r\n";

			var sb = new StringBuilder();
			sb.AppendFormat(transactionsInfoMask, Transactions.Count);
			sb.AppendFormat(clustersInfoMask, Clusters.Count);
			sb.AppendFormat(keysInfoMask, keys.Count);

			return sb.ToString();
		}



		public string CurrentOutput() => string.Join("", Clusters.Select(c => c as IPreviewable).Select(view => view.MakePreview()).ToArray());



		// IAlgo: Run
		public void Run()
		{
			bool onlyStart = true;

			Transaction4.PreciseComparing = true;

			while (newTrans.Count > 0)
			{
				Start();
			}

			Transaction4.PreciseComparing = false;

			if (onlyStart)
				return;

			while (needSpecify && stepIndex < maxSteps)
			{
				Specify();
			}

			RemoveEmptyClusters();

			//CurrentStepName = CurrentStepNameDone;
			//OnStepDone();
		}



		// Start
		void Start()
		{
			while (newTrans.Count > 0)
			{
				var t = newTrans.Dequeue();
				if (IsUnique(t))
				{
					needSpecify = true;
					PlaceTransactionIntoCluster(t);
				}
			}

			//CurrentStepName = CurrentStepNameInit;
			//OnStepDone();
		}



		// Specify
		bool Specify()
		{
			needSpecify = false;

			foreach (var t in Transactions)
				SpecifyCluster(t);

			//CurrentStepName = string.Format(CurrentStepNameMask, stepIndex++);
			//OnStepDone();

			return needSpecify;
		}



		// private: RemoveEmptyClusters
		void RemoveEmptyClusters() => Clusters = (from c in Clusters where !c.IsEmpty select c).ToList();



		#region private: IsUnique, IntermediateOutput, PlaceTransaction, Specifying, Start.
		bool IsUnique(ITransaction t) => keys.ContainsKey(t);



		void PlaceTransactionIntoCluster(ITransaction t)
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



		void SpecifyCluster(ITransaction t)
		{
			const double minMaxCost = 0.5f;
			var activeCluster = keys[t];
			//var bestCluster = activeCluster;
			ICluster bestCluster = null;

			double maxCost = minMaxCost;
			double remCost = activeCluster.RemoveCost(t);

			foreach (ICluster c in Clusters)
			{
				if (c == activeCluster) // except active (current) cluster.  
					continue;

				double addCost = c.AddCost(t);
				if (maxCost < addCost + remCost)
				{   // [?] remCost + or - [?]
					maxCost = addCost + remCost;
					bestCluster = c;
				}
			}

			//if (maxCost > 0) {	// bestCluster != activeCluster.
			if (bestCluster != null && bestCluster != activeCluster)
			{

				// if (bestCluster == null) 	<- shouldn't appear.

				if (bestCluster.IsEmpty)
					Clusters.Add(new Cluster4(Repulsion));

				activeCluster.Remove(t);
				bestCluster.Add(t);

				keys[t] = activeCluster;    // [!]
				needSpecify = true;
			}
		}
		#endregion
	}
}
