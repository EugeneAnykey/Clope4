#define simultaneous1
/* simultaneous (load) - (сделал для тестов) порция данных передается на обработку сразу, по мере поступления
 * в противном случае, сначала сохраняем все данные,
 *  а после получения обрабатываем (для теста скорости обработки).
 */

#define keys1

using System.Collections.Generic;
using System.Linq;
using ClopeLib.Data;
using ClopeLib.Helpers;

namespace ClopeLib.Algo
{
	public delegate void EventStepDoneHandler(int step, int changesDone);

	public class Clope
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
			set
			{
				Utils.TrySetValue(ref repulsion, value, minRepulsion, maxRepulsion);
				MathPower = new MathPower(repulsion);
			}
		}



		MathPower MathPower;
#if !simultaneous
		readonly Queue<ITransaction> newTrans;
#endif
		public List<ITransaction> Transactions { get; }
		public List<ICluster> Clusters { get; private set; }
#if keys
		Dictionary<ITransaction, ICluster> keys = new Dictionary<ITransaction, ICluster>();
#else
		List<ICluster> clusterKeys = new List<ICluster>();
#endif



		// init
		public Clope()
		{
			Transactions = new List<ITransaction>();
			Clusters = new List<ICluster>();
			Repulsion = 2;
#if simultaneous
			stepChanges = 0;
#else
			newTrans = new Queue<ITransaction>();
#endif
		}



		/// <summary>
		/// calculates whether to further refine the result
		/// </summary>
		/// <param name="changesCount"></param>
		/// <returns></returns>
#if keys
		bool NeedSpecify(int changesCount) => ((double)stepChanges / keys.Count) > specThreshold && LatestStep < maxSteps;
#else
		bool NeedSpecify(int changesCount) => ((double)stepChanges / clusterKeys.Count) > specThreshold && LatestStep < maxSteps;
#endif



#if simultaneous
		public void AddNewTransactions(IEnumerable<ITransaction> newTransactions)
		{
			foreach (var item in newTransactions)
			{
				PlaceIntoCluster(item);
				stepChanges++;
			}
		}
#else
		public void AddNewTransactions(IEnumerable<ITransaction> newTransactions) => newTrans.Enqueue(newTransactions);

		void Start()
		{
			while (newTrans.Count > 0)
			{
				var t = newTrans.Dequeue();
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
#if !simultaneous
			newTrans.Clear();
#endif
			Transactions.Clear();
			Clusters.Clear();
#if keys
			keys.Clear();
#else
			clusterKeys.Clear();
#endif
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

#if keys
				foreach (var t in Transactions)
					if (SpecifyCluster(t))
						stepChanges++;
#else
				for (int i = 0; i < Transactions.Count; i++)
					if (SpecifyClusterForTransactions(i))
						stepChanges++;
#endif

				OnStepDone(LatestStep++, stepChanges);
			} while (NeedSpecify(stepChanges));
		}



		void RemoveEmptyClusters() => Clusters = (from c in Clusters where !c.IsEmpty select c).ToList();



		void CheckingForAtLeastOneEmptyCluster()
		{
			var exists = Clusters.Where(c => c.IsEmpty).Count() > 0;
			if (!exists)
				Clusters.Add(new Cluster(ref MathPower));
		}



		void PlaceIntoCluster(ITransaction t)
		{
			ICluster bestCluster = BestClusterSearch(t, null);

			if (bestCluster == null)
				Clusters.Add(bestCluster = new Cluster(ref MathPower));

			bestCluster.Add(t);
			Transactions.Add(t);
#if keys
			keys.Add(t, bestCluster);
#else
			clusterKeys.Add(bestCluster);
#endif
		}



		ICluster BestClusterSearch(ITransaction t, ICluster initialCluster, double initialMaxCost = 0)
		{
			var bestCluster = initialCluster;
			var maxCost = initialMaxCost;

			foreach (ICluster c in Clusters)
			{
				if (c == bestCluster)
					continue;

				double addCost = c.GetAddCost(t);

				if (maxCost < addCost)
				{
					maxCost = addCost;
					bestCluster = c;
				}
			}

			return bestCluster;
		}



#if keys
		bool SpecifyCluster(ITransaction t)
		{
			CheckingForAtLeastOneEmptyCluster();

			var currentCluster = keys[t];
			ICluster bestCluster = BestClusterSearch(t, currentCluster, currentCluster.GetRemCost(t));

			if (bestCluster != currentCluster)
			{
				currentCluster.Remove(t);
				bestCluster.Add(t);
				keys[t] = bestCluster;
				return true;
			}

			return false;
		}
#else
		bool SpecifyClusterForTransactions(int index)
		{
			CheckingForAtLeastOneEmptyCluster();
			var t = Transactions[index];
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
#endif
	}
}
