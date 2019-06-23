namespace ClopeLib
{
	public interface ITransaction
	{
		int Length { get; }
		
		int[] Links { get; }
		
		bool Equals(ITransaction t);
	}
}
