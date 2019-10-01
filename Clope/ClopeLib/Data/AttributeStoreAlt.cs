using System;
using System.Collections.Generic;
using System.Linq;

namespace ClopeLib.Data
{
	public class AttributeStoreAlt<T> : IAttributeStore<T>
	{
		// fields
		int latestNewLink = 1;

		readonly List<Dictionary<AttributeAlt<T>, int>> store = new List<Dictionary<AttributeAlt<T>, int>>();



		// init
		public AttributeStoreAlt() { }



		int PlaceAttribute(int pos, T at)
		{
			var att = new AttributeAlt<T>(at);

			if (!store[pos].ContainsKey(att))
				store[pos].Add(att, latestNewLink++);

			return store[pos][att];
		}



		void CheckListLength(int pos)
		{
			while (store.Count <= pos)
				store.Add(new Dictionary<AttributeAlt<T>, int>());
		}



		public T GetAttributeByLink(int link)
		{
			foreach (var columnDic in store)
			{
				if (columnDic.ContainsValue(link))
					return columnDic.First(p => p.Value == link).Key.Value;
			}
			return default(T);
		}

		public T[] GetAttributes(int position) => store[position].Select(p => p.Key.Value).ToArray();

		public int[] GetAttributesLinks(int position) => store[position].Select(p => p.Value).ToArray();



		public int[] PlaceAndGetLinks(params T[] items)
		{
			if (items == null)
				throw new ArgumentNullException();

			if (items.Length == 0)
				throw new EmptyArrayException();

			CheckListLength(items.Length);

			var res = new int[items.Length];

			for (int col = 0; col < items.Length; col++)
				res[col] = PlaceAttribute(col, items[col]);

			return res;
		}
	}
}
