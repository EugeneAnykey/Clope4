namespace ClopeLib.Data4
{
	public class TransactionAttribute4 : IAttribute
	{
		public int Id { get; }

		public int Position { get; }

		public string Name { get; }



		// init
		public TransactionAttribute4(int id, int index, string name)
		{
			Id = id;
			Position = index;
			Name = name;
		}

		public bool Equals(IAttribute other) => Position == other.Position && Name == other.Name;
	}
}
