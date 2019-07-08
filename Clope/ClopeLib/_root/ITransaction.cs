namespace ClopeLib
{
	public interface ITransaction
	{
		uint Id { get; }

		int Length { get; }
		
		int[] Links { get; }
		
		bool Equals(ITransaction t);
	}
}
