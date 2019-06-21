namespace ClopeLib.Parcers
{
	public interface IParcer
	{
		char[] Splitter {get; set;}
		
		string[] Parce(string line);
	}
}
