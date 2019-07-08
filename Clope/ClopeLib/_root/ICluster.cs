namespace ClopeLib
{
	public interface ICluster
	{
		/// <summary>
		/// Unique objects.
		/// </summary>
		int Width { get; }

		/// <summary>
		/// All attributes count.
		/// </summary>
		int Area { get; }

		/// <summary>
		/// Unique transactions count.
		/// </summary>
		int TransactionsCount { get; }

		/// <summary>
		/// True when there is no transactions (and attributes).
		/// </summary>
		bool IsEmpty { get; }



		/// <summary>
		/// Calculates the cost for adding a transaction.
		/// </summary>
		/// <param name="t">unique transaction</param>
		/// <returns>cost</returns>
		double GetAddCost(ITransaction t);



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
		/// Shows how many times an attribute appears
		/// </summary>
		/// <param name="index">input attribute index</param>
		/// <returns>times that attribute appears</returns>
		int Occurrence(int index);
	}
}
