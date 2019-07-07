namespace ClopeLib
{
	public interface IIndexCounter
	{
		void Inc(int[] indicies);

		void Dec(int[] indicies);

		int Positives { get; }

		int this[int index] { get; }
	}
}
