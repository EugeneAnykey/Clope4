namespace ClopeLib.Parsers
{
	public interface IParser
	{
		char[] Splitter {get; set;}
		
		string[] Parse(string line);
	}
}
