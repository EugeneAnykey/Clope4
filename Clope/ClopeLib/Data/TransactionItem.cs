namespace ClopeLib.Data
{
	public class TransactionItem
	{
		public static bool PreciseComparing { get; set; }
		
		public int ColId { get; }
		public int Link { get; }
		public string Name { get; }

		readonly int hashCode = 0;



		// init
		public TransactionItem(int colId, int link, string name)
		{
			ColId = colId;
			Link = link;
			Name = name;
			hashCode = MakeHashCode();
		}



		// Equals
		public override bool Equals(object obj)
		{
			return Equals(obj as TransactionItem);
		}

		public bool Equals(TransactionItem t)
		{
			return ColId == t.ColId && Link == t.Link /* && Name == t.Name*/;
		}



		// HashCode
		public override int GetHashCode() => hashCode;

		int MakeHashCode() => (ColId + 1) * Link;
	}
}
