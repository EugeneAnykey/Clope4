namespace ClopeLib
{
	public interface IClustering : IAlgo
	{
		// property
		float Repulsion { get; set; }



		// methods
		void Clear();

		void AddNewTransactions(ITransaction[] newTransactions);
	}
}
