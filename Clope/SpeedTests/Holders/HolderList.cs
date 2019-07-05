#define struct1

using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeedTests.Holders
{
	public class HolderList : IHolder
	{
		IItem _GetItem(int index, string name) =>
#if struct
		new StructItem(index, name);
#else
		new ClassItem(index, name);
			
#endif



		// fields
		readonly List<IItem> items = new List<IItem>();



		// init
		public HolderList() { }



		int PlaceItem(IItem at)
		{
			var index = 0;
			if (!items.Contains(at))
			{
				index = items.Count;
				items.Add(at);
			}
			else
				index = items.IndexOf(at);

			return index;
		}



		public IItem[] Retrieve(int id) => items.Where(a => a.Id == id).ToArray();

		public IItem RetrieveByIndex(int index) => 0 <= index && index < items.Count ? items[index] : null;

		public int[] PlaceAndGetIndicies(params string[] items)
		{
			if (items == null)
				throw new ArgumentNullException();

			if (items.Length == 0)
				return new int[0];

			var result = new int[items.Length];

			for (int col = 0; col < items.Length; col++)
			{
				IItem item = _GetItem(col, items[col]);
				result[col] = PlaceItem(item);
			}

			return result;
		}
	}
}
