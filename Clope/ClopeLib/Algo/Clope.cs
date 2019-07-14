#define simul1	// сделал для тестов
				// simul(taneous load) - порция данных передается на обработку сразу, по мере поступления
				// в противном случае, сначала сохраняем все данные,
				//  а после получения обрабатываем (для теста скорости обработки).

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
#if !simul
		readonly Queue<ITransaction> newTrans;
#endif
		public List<ITransaction> Transactions { get; }
		public List<ICluster> Clusters { get; private set; }
		Dictionary<ITransaction, ICluster> keys;



		// init
		public Clope()
		{
			Transactions = new List<ITransaction>();
			Clusters = new List<ICluster>();
			keys = new Dictionary<ITransaction, ICluster>();
			Repulsion = 2;
#if simul
			stepChanges = 0;
#else
			newTrans = new Queue<ITransaction>();
#endif
		}



		bool NeedSpec(int changesCount) => ((double)stepChanges / keys.Count) > specThreshold && LatestStep < maxSteps;



#if simul
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



		// Clear
		public void Clear()
		{
#if !simul
			newTrans.Clear();
#endif
			Transactions.Clear();
			Clusters.Clear();
			keys.Clear();
		}



		// Run
		public void Run()
		{
			LatestStep = 0;
#if simul
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

				foreach (var t in Transactions)
					if (SpecifyCluster(t))
						stepChanges++;

				OnStepDone(LatestStep++, stepChanges);
			} while (NeedSpec(stepChanges));
		}



		void RemoveEmptyClusters() => Clusters = (from c in Clusters where !c.IsEmpty select c).ToList();



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
				Clusters.Add(bestCluster = new Cluster(ref MathPower));

			bestCluster.Add(t);
			Transactions.Add(t);
			keys.Add(t, bestCluster);
		}



		void CheckingForAtLeastOneEmptyCluster()
		{
			var exists = Clusters.Where(c => c.IsEmpty).Count() > 0;
			if (!exists)
				Clusters.Add(new Cluster(ref MathPower));
		}



		bool SpecifyCluster(ITransaction t)
		{
			CheckingForAtLeastOneEmptyCluster();

			var currentCluster = keys[t];
			ICluster bestCluster = currentCluster;
			double maxCost = currentCluster.GetRemCost(t);

			foreach (ICluster c in Clusters)
			{
				if (c == currentCluster)
					continue;

				double addCost = c.GetAddCost(t);

				if (maxCost < addCost)
				{
					maxCost = addCost;
					bestCluster = c;
				}
			}

			if (bestCluster != currentCluster)
			{
				currentCluster.Remove(t);
				bestCluster.Add(t);
				keys[t] = bestCluster;
				return true;
			}

			return false;
		}



		public IEnumerable<ITransaction> GetTransactions_Axe() => keys.Select(t => t.Key).ToArray();
	}
}
