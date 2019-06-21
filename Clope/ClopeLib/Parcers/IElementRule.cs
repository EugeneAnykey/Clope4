namespace ClopeLib.Parcers
{
	public interface IElementRule
	{
		string[] Invalids { get; set; }
		string Replacement { get; set; }
		
		bool ElementIsValid(string line);
		
		string Validate(string line);
		void Validate(ref string line);
	}
}
