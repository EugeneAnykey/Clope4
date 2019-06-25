using System.Collections.Generic;

namespace ClopeLib
{
	/// <summary>
	/// ICluster.
	/// </summary>
	public interface ICluster
	{
		#region ?
		int Id { get; }

		List<ITransaction> Transactions { get; }
		#endregion



		/// <summary>
		/// Unique objects.
		/// </summary>
		int Width { get; }

		/// <summary>
		/// All objects.
		/// </summary>
		int Area { get; }

		/// <summary>
		/// Unique transactions count.
		/// </summary>
		int TransactionsCount { get; }

		/// <summary>
		/// When there is no transactions (and objects).
		/// </summary>
		bool IsEmpty { get; }

		/// <summary>
		/// Calculates the cost for adding a transaction.
		/// </summary>
		/// <param name="t">unique transaction</param>
		/// <returns>cost</returns>
		double AddCost(ITransaction t);

		/// <summary>
		/// Calculates removing cost for a transaction.
		/// </summary>
		/// <param name="t">transaction from current list</param>
		/// <returns>cost</returns>
		double RemoveCost(ITransaction t);

		/// <summary>
		/// Adding a unique transaction
		/// </summary>
		/// <param name="t">a unique transaction</param>
		void Add(ITransaction t);

		/// <summary>
		/// Removing current transaction
		/// </summary>
		/// <param name="t">current transaction</param>
		void Remove(ITransaction t);

		/// <summary>
		/// Shows how many times an object appears
		/// </summary>
		/// <param name="s">input object</param>
		/// <returns>times that object appears</returns>
		int Occurrence(int i);
	}
}
