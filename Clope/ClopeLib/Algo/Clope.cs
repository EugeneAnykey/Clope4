#define simultaneous1
/* simultaneous (load) - (сделал для тестов) порция данных передается на обработку сразу, по мере поступления
 * в противном случае, сначала сохраняем все данные,
 *  а после получения обрабатываем (для теста скорости обработки).
 */

using System;
using System.Collections.Generic;
using System.Linq;
using ClopeLib.Data;
using ClopeLib.Helpers;

namespace ClopeLib.Algo
{
	public class Clope
	{
		public event EventStepDoneHandler StepDone;
		void OnStepDone(int step, int changes) => StepDone?.Invoke(step, changes);


		const float defaultRepulsion = 2;
		const float minRepulsion = 1;
		const float maxRepulsion = 10;

		const float specThreshold = 0.001f;
		const int maxSteps = 15;



		// fields
		float repulsion;
		public float Repulsion
		{
			get { return repulsion; }
			set
			{
				Utils.TrySetValue(ref repulsion, value, minRepulsion, maxRepulsion);
				MathPower = new MathPower(repulsion);
			}
		}



		int stepChanges;
		public int LatestStep { get; private set; }

		MathPower MathPower;
		int transactionsDone = 0;
		List<ITransaction> transactions;
		List<ICluster> clusterKeys = new List<ICluster>();
		public List<ICluster> Clusters { get; private set; }



		// init
		public Clope(List<ITransaction> transactions)
		{
			this.transactions = transactions ?? throw new ArgumentNullException();
			Clusters = new List<ICluster>();
			Repulsion = defaultRepulsion;
			stepChanges = 0;
		}



		/// <summary>
		/// calculates whether to further refine the result
		/// </summary>
		/// <param name="changesCount"></param>
		/// <returns>true if further specifing is needed</returns>
		bool NeedSpecify(int changesCount) => ((double)stepChanges / clusterKeys.Count) > specThreshold && LatestStep < maxSteps;



#if simultaneous
		public void AddNewTransactions()
		{
			while (transactions.Count > transactionsDone)
			{
				var t = transactions[transactionsDone++];
				PlaceIntoCluster(t);
				stepChanges++;
			}
		}
#else
		public void AddNewTransactions()
		{
		}

		void Start()
		{
			while (transactions.Count > transactionsDone)
			{
				var t = transactions[transactionsDone++];
				PlaceIntoCluster(t);
				stepChanges++;
			}

			OnStepDone(LatestStep++, stepChanges);
		}
#endif



		/// <summary>
		/// Prepares Clope for a new clean start.
		/// </summary>
		public void Clear()
		{
			Clusters.Clear();
			clusterKeys.Clear();
		}



		/// <summary>
		/// Runs clustering algo.
		/// </summary>
		public void Run()
		{
			LatestStep = 0;
#if simultaneous
			OnStepDone(LatestStep++, stepChanges);
#else
			Start();
#endif
			Specify();
			RemoveEmptyClusters();
		}



		void Specify()
		{
			do
			{
				stepChanges = 0;

				for (int i = 0; i < transactions.Count; i++)
					if (SpecifyClusterForTransactions(i))
						stepChanges++;

				OnStepDone(LatestStep++, stepChanges);
			} while (NeedSpecify(stepChanges));
		}



		void RemoveEmptyClusters() => Clusters = (from c in Clusters where !c.IsEmpty select c).ToList();



		void CheckingForAtLeastOneEmptyCluster()
		{
			var exists = Clusters.Where(c => c.IsEmpty).Count() > 0;
			if (!exists)
				Clusters.Add(new Cluster(MathPower));
		}



		void PlaceIntoCluster(ITransaction t)
		{
			CheckingForAtLeastOneEmptyCluster();
			
			ICluster bestCluster = BestClusterSearch(t, null);

			bestCluster.Add(t);
			clusterKeys.Add(bestCluster);
		}



		ICluster BestClusterSearch(ITransaction transaction, ICluster initialCluster, double initialMaxCost = 0)
		{
			var bestCluster = initialCluster;
			var maxCost = initialMaxCost;

			foreach (ICluster c in Clusters)
			{
				if (c == bestCluster) continue;

				double addCost = c.GetAddCost(transaction);

				if (maxCost < addCost)
				{
					maxCost = addCost;
					bestCluster = c;
				}
			}

			return bestCluster;
		}



		bool SpecifyClusterForTransactions(int index)
		{
			var t = transactions[index];
			var currentCluster = clusterKeys[index];
			ICluster bestCluster = BestClusterSearch(t, currentCluster, currentCluster.GetRemCost(t));

			if (bestCluster != currentCluster)
			{
				currentCluster.Remove(t);
				bestCluster.Add(t);
				clusterKeys[index] = bestCluster;
				return true;
			}

			return false;
		}
	}
}
