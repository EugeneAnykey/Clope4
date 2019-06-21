namespace ClopeLib.Parcers
{
	public class ElementRule : IElementRule
	{
		// field
		string[] invalids;
		public string[] Invalids { get => invalids; set => invalids = value ?? new string[0]; }

		public string Replacement { get; set; }



		// init
		public ElementRule()
		{
			Invalids = null;
		}

		public ElementRule(string[] possibleInvalids, string replacement)
		{
			Invalids = possibleInvalids;
			Replacement = replacement;
		}



		// public: ElementIsValid, Validate.
		public bool ElementIsValid(string line)
		{
			foreach (var invalid in Invalids)
				//if (line.Contains(invalid))
				if (line.Equals(invalid))
					return false;
			return true;
		}

		public string Validate(string line)
		{
			return
				ElementIsValid(line) ?
				line :
				Replacement;
		}

		public void Validate(ref string line)
		{
			if (!ElementIsValid(line))
				line = Replacement;
		}
	}
}
