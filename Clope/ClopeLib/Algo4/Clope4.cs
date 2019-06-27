﻿using System.Collections.Generic;
using System.Linq;
using ClopeLib.Data;
using ClopeLib.Helpers;
using EugeneAnykey.DebugLib.Loggers;

namespace ClopeLib.Algo
{
	public delegate void EventStepDoneHandler(int step, int changesDone);

	public class Clope4 : IClustering
	{
		public event EventStepDoneHandler StepDone;
		void OnStepDone(int step, int changes) => StepDone?.Invoke(step, changes);



		const int maxSteps =
			3;
			//15;

		const float specThreshold = 0.001f;



		// Logger
		static ILogger logger;
		public static ILogger Logger { get => logger; set => logger = value ?? new ConsoleLogger(); }



		int stepChanges;
		public int LatestStep { get; private set; }



		// field
		float repulsion;
		public float Repulsion
		{
			get { return repulsion; }
			set { Utils.TrySetValue(ref repulsion, value, 1, 5); }
		}



		readonly Queue<ITransaction> newTrans;
		public List<ITransaction> Transactions { get; }
		public List<ICluster> Clusters { get; private set; }
		Dictionary<ITransaction, ICluster> keys;



		// init
		public Clope4()
		{
			newTrans = new Queue<ITransaction>();
			Transactions = new List<ITransaction>();
			Clusters = new List<ICluster>();
			keys = new Dictionary<ITransaction, ICluster>();

			Cluster4.ResetId();
		}



		bool NeedSpec(int changesCount) => ((double)stepChanges / Transactions.Count) > specThreshold && LatestStep < maxSteps;



		// IClustering: AddNewTransactions
		public void AddNewTransactions(ITransaction[] newTransactions) => newTrans.Enqueue(newTransactions);



		// IClustering: Clear
		public void Clear()
		{
			newTrans.Clear();
			Transactions.Clear();
			Clusters.Clear();
			keys.Clear();

			Cluster4.ResetId();
		}



		// IAlgo: Run
		public void Run()
		{
			LatestStep = 0;
			Start();
			Specify();
			RemoveEmptyClusters();
		}



		void Start()
		{
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

			OnStepDone(LatestStep++, stepChanges);
		}



		void Specify()
		{
			do
			{
				stepChanges = 0;

				foreach (var t in Transactions)
					if (SpecifyCluster(t))
						stepChanges++;

				OnStepDone(LatestStep++, stepChanges);
			} while (NeedSpec(stepChanges));
		}



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
			const double minStartCost = 0;

			var currentCluster = keys[t];
			var bestCluster = currentCluster;
			double maxCost = minStartCost;
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
		#endregion
	}
}
