﻿using System;
using System.Collections.Generic;
using System.Text;
using ClopeLib.Data;
using EugeneAnykey.DebugLib.Loggers;

namespace ClopeLib.Algo
{
	public class Clope : IAlgo, IClustering, IClopeInfo
	{
		// event
		public event EventHandler StepDone;
		protected virtual void OnStepDone() => StepDone?.Invoke(this, EventArgs.Empty);



		// const
		readonly bool test = true;

		const string CurrentStepNameInit = "Just initialized.";
		const string CurrentStepNameMask = "step {0}";
		const string CurrentStepNameDone = "Job done.";
		const int maxSteps = 10;



		// field Logger
		static ILogger logger;
		public static ILogger Logger { get => logger; set => logger = value ?? new ConsoleLogger(); }



		//field CurrentStep
		int stepIndex;
		public string CurrentStepName { get; private set; }



		// field
		float repulsion;
		public float Repulsion
		{
			get { return repulsion; }
			set
			{
				const float eps = 0.000001f;
				value = value >= 1 ? value : 1;
				needSpecify |= Math.Abs(repulsion - value) < eps;
				repulsion = value;
			}
		}

		bool needSpecify;

		public List<ICluster> Clusters { get; private set; }
		public List<ITransaction> Transactions { get; private set; }

		readonly Queue<ITransaction> newTrans;

		Dictionary<ITransaction, ICluster> keys;



		// init
		public Clope()
		{
			Clusters = new List<ICluster>();
			newTrans = new Queue<ITransaction>();
			Transactions = new List<ITransaction>();
			keys = new Dictionary<ITransaction, ICluster>();

			Clear();
		}



		#region interface: AddNewTransactions, MakeOutput, Run.
		public void AddNewTransactions(ITransaction[] newTransactions)
		{
			foreach (var t in newTransactions)
			{
				newTrans.Enqueue(t);
			}
		}

		public void Clear()
		{
			Clusters.Clear();
			newTrans.Clear();
			Transactions.Clear();
			keys.Clear();

			stepIndex = 0;
			CurrentStepName = "Just initialized.";
			needSpecify = false;
			Cluster.ResetId();
		}

		public string CurrentOutput()
		{
			var sb = new StringBuilder();
			foreach (var c in Clusters)
				sb.AppendLine(c.OutputContent());

			return sb.ToString();
		}

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

		public void Run()
		{
			Transaction.PreciseComparing = true;

			while (newTrans.Count > 0)
			{
				Start();
			}

			Transaction.PreciseComparing = false;

			while (needSpecify && stepIndex < maxSteps)
			{
				Specify();
			}

			RemoveEmptyClusters();

			CurrentStepName = CurrentStepNameDone;
			OnStepDone();
		}
		#endregion



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

			CurrentStepName = CurrentStepNameInit;
			OnStepDone();
		}



		// Specify
		bool Specify()
		{
			needSpecify = false;

			foreach (var t in Transactions)
				SpecifyCluster(t);

			CurrentStepName = string.Format(CurrentStepNameMask, stepIndex++);
			OnStepDone();

			return needSpecify;
		}



		#region private: IsUnique, IntermediateOutput, PlaceTransaction, RemoveEmptyClusters, Specifying, Start.
		bool IsUnique(ITransaction t)
		{
			if (test)
				return true;

			foreach (var k in keys)
			{
				if (t.Equals(k.Key))
					return false;
			}
			return true;
		}

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
				Clusters.Add(bestCluster = new Cluster(repulsion));

			bestCluster.Add(t);
			Transactions.Add(t);
			keys.Add(t, bestCluster);
		}

		void RemoveEmptyClusters()
		{
			int i = 0;
			while (Clusters.Count > 0 && Clusters.Count > i)
				if (Clusters[i].IsEmpty)
					Clusters.RemoveAt(i);
				else
					i++;
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
					Clusters.Add(new Cluster(Repulsion));

				activeCluster.Remove(t);
				bestCluster.Add(t);

				keys[t] = activeCluster;    // [!]
				needSpecify = true;
			}
		}
		#endregion
	}
}
