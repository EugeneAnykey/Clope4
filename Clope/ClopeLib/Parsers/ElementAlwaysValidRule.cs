namespace ClopeLib.Parsers
{
	public class ElementAlwaysValidRule : IElementRule
	{
		// field
		public string[] Invalids { get => null; set => value = null; }  // do nothing.
		public string Replacement { get => null; set => value = null; }  // do nothing.



		// IElementRule
		public bool ElementIsValid(string line) => true;	// always good.
		public string Validate(string line) => line;		// do nothing.
		public void Validate(ref string line) { }			// do nothing.



		// init
		public ElementAlwaysValidRule()
		{
		}
	}
}
