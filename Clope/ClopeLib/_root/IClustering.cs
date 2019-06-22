namespace ClopeLib
{
	public interface IClustering : IAlgo
	{
		// field
		float Repulsion { get; set; }



		// method
		void Clear();

		void AddNewTransactions(ITransaction[] newTransactions);
	}
}
