namespace ClopeLib
{
	public interface ITransaction
	{
		int Length {get;}
		
		string[] Items { get; }
		int[] Links { get; }
		
		bool Equals(ITransaction t);
	}
}
