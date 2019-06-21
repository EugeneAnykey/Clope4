namespace ClopeLib.Data
{
	public interface ITransaction
	{
		int Length {get;}
		
		string[] Items { get; }
		
		bool Equals(ITransaction t);
	}
}
