namespace ClopeLib
{
	public interface ICluster
	{
		/// <summary>
		/// Unique attributes.
		/// </summary>
		int Width { get; }

		/// <summary>
		/// All attributes count.
		/// </summary>
		long Area { get; }

		/// <summary>
		/// Added transactions count.
		/// </summary>
		long TransactionsCount { get; }

		/// <summary>
		/// True when there is no transactions (and attributes).
		/// </summary>
		bool IsEmpty { get; }



		/// <summary>
		/// Calculates the cost for adding a transaction.
		/// </summary>
		/// <param name="t">transaction</param>
		/// <returns>the cost with transaction</returns>
		double GetAddCost(ITransaction t);

		/// <summary>
		/// Calculates the cluster's cost in case of transaction been removed.
		/// </summary>
		/// <param name="t">transaction</param>
		/// <returns>the cost without transaction</returns>
		double GetRemCost(ITransaction t);



		/// <summary>
		/// Adding a unique transaction
		/// </summary>
		/// <param name="t">a transaction</param>
		void Add(ITransaction t);

		/// <summary>
		/// Removing current transaction
		/// </summary>
		/// <param name="t">current transaction</param>
		void Remove(ITransaction t);

		/// <summary>
		/// Shows how many times an attribute appears
		/// </summary>
		/// <param name="index">input attribute index</param>
		/// <returns>times that attribute appears</returns>
		int Occurrence(int index);
	}
}
