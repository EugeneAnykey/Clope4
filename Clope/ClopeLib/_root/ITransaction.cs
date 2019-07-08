namespace ClopeLib
{
	public interface ITransaction
	{
		int Id { get; }

		int Length { get; }
		
		int[] Links { get; }
		
		bool Equals(ITransaction t);
	}
}
