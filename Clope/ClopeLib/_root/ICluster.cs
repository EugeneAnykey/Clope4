namespace ClopeLib
{
	public interface ICluster
	{
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
		/// True when there is no transactions (and objects).
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
		/// Shows how many times an object appears
		/// </summary>
		/// <param name="s">input object</param>
		/// <returns>times that object appears</returns>
		int Occurrence(int i);



		// for preview
		int GetCount(int link);
	}
}
