namespace ClopeLib
{
	public interface IIndexCounter
	{
		/// <summary>
		/// Counts indicies (adds 1 into index position)
		/// </summary>
		/// <param name="indicies">array with indicies</param>
		void Inc(int[] indicies);


		/// <summary>
		/// Counts indicies (remove 1 from index position)
		/// </summary>
		/// <param name="indicies">array with indicies</param>
		void Dec(int[] indicies);



		/// <summary>
		/// Shows count of positive elements
		/// </summary>
		int Positives { get; }


		/// <summary>
		/// Returns counts at index position
		/// </summary>
		/// <param name="index">position to be checked</param>
		/// <returns>counts that index is found</returns>
		int this[int index] { get; }
	}
}
