namespace ClopeLib.Data4
{
	public class TransactionAttribute4 : IAttribute
	{
		public int Id { get; }

		public int Index { get; }

		public string Name { get; }



		// init
		public TransactionAttribute4(int id, int index, string name)
		{
			Id = id;
			Index = index;
			Name = name;
		}

		public bool Equals(IAttribute other) => Index == other.Index && Name == other.Name;
	}
}
