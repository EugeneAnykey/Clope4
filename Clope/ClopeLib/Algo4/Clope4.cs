﻿using System.Collections.Generic;
using System.Linq;
using ClopeLib.Data;
using ClopeLib.Helpers;

namespace ClopeLib.Algo
{
	public delegate void EventStepDoneHandler(int step, int changesDone);

	public class Clope4
	{
		public event EventStepDoneHandler StepDone;
		void OnStepDone(int step, int changes) => StepDone?.Invoke(step, changes);



		const float minRepulsion = 1;
		const float maxRepulsion = 10;

		const float specThreshold = 0.001f;

		const int maxSteps = 15;



		int stepChanges;
		public int LatestStep { get; private set; }



		// field
		float repulsion;
		public float Repulsion
		{
			get { return repulsion; }
			set { Utils.TrySetValue(ref repulsion, value, minRepulsion, maxRepulsion); }
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



		// AddNewTransactions
		public void AddNewTransactions(ITransaction[] newTransactions) => newTrans.Enqueue(newTransactions);



		// Clear
		public void Clear()
		{
			newTrans.Clear();
			Transactions.Clear();
			Clusters.Clear();
			keys.Clear();

			Cluster4.ResetId();
		}



		// Run
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



		void RemoveEmptyClusters() => Clusters = (from c in Clusters where !c.IsEmpty select c).ToList();

		bool IsUnique(ITransaction t) => !keys.ContainsKey(t);



		#region private: PlaceIntoCluster, SpecifyCluster, CheckingForAtLeastOneEmptyCluster.
		void PlaceIntoCluster(ITransaction t)
		{
			ICluster bestCluster = null;

			double maxCost = 0;

			foreach (ICluster c in Clusters)
			{
				double addCost = c.GetAddCost(t);
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



		void CheckingForAtLeastOneEmptyCluster()
		{
			var exists = Clusters.Where(c => c.IsEmpty).Count() > 0;
			if (!exists)
				Clusters.Add(new Cluster4(Repulsion));
		}



		bool SpecifyCluster(ITransaction t)
		{
			CheckingForAtLeastOneEmptyCluster();

			var currentCluster = keys[t];
			currentCluster.Remove(t);

			ICluster bestCluster = currentCluster;

			double maxCost = 0;

			foreach (ICluster c in Clusters)
			{
				double addCost = c.GetAddCost(t);
				if (maxCost < addCost)
				{
					maxCost = addCost;
					bestCluster = c;
				}
			}

			bestCluster.Add(t);
			keys[t] = bestCluster;

			return currentCluster != bestCluster;
		}
		#endregion
	}
}
