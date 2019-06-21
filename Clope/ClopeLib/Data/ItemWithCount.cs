namespace ClopeLib.Data
{
	/// <summary>
	/// not implemented. ItemWithCount.
	/// </summary>
	public class ItemWithCount<T>
	{
		/*
		 * TODO (maybe) class ListCollection<T> : Collection<T>
		 * {
		 *		Add, Remove, ....		// AddOne ( if not exists - add, else inc count).
		 *		Add { if Contains() -> Inc/Dec }
		 *		
		 *		MakeHash, Search, Contains.
		 * }
		 * ListCollection<ItemWithCount<int>> list = new List<ItemWithCount<int>>();
		*/



		// field
		public int Count { get; private set;  }
		public T Item { get; }
		public bool IsEmpty { get => Count < 1; }



		// init
		public ItemWithCount(T item)
		{
			Count = 1;
			Item = item;
		}



		// Inc, Dec.
		public void Inc() => Count++;

		public void Dec() => Count--;
	}
}
