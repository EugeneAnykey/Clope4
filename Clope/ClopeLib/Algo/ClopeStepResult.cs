namespace ClopeLib.Algo
{
	public class ClopeStepResult
	{
		// field
		public string Name { get; private set; }
		public string Output { get; private set; }
		public string Info { get; private set; }
		


		// init
		public ClopeStepResult(string name, string output, string info)
		{
			Name = name;
			Output = output;
			Info = info;
		}
		


		public override string ToString()
		{
			return Name;
		}
	}
}
