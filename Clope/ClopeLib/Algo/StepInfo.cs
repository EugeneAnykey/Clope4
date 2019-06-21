namespace ClopeLib.Algo
{
	public class StepInfo
	{
		// field
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Output { get; private set; }
		public string Info { get; private set; }
		


		// init
		public StepInfo(int id, string name, string output, string info)
		{
			Id = id;
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
