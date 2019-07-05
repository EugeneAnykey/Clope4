#define struct

using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeedTests.Holders
{
	public sealed class IdentityEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		public int GetHashCode(T value)
		{
			return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(value);
		}

		public bool Equals(T left, T right)
		{
			return left.Equals(right);
		}
	}



	public class HolderDic : IHolder
	{
		IItem _GetItem(int index, string name) =>
#if struct
		new StructItem(index, name);
#else
		new ClassItem(index, name);
#endif



		// fields
		readonly Dictionary<IItem, int> dic;



		// init
		public HolderDic(bool useComparer = false)
		{
			dic = useComparer ?
				new Dictionary<IItem, int>(new IdentityEqualityComparer<IItem>()) :
				new Dictionary<IItem, int>();
		}



		// PlaceAttribute
		int PlaceItem(IItem at)
		{
			if (!dic.ContainsKey(at))
			{
				dic.Add(at, dic.Count);
			}

			return dic[at];
		}



		public int[] PlaceAndGetIndicies(params string[] items)
		{
			if (items == null)
				throw new ArgumentNullException();

			if (items.Length == 0)
				return new int[0];

			var res = new int[items.Length];

			for (int col = 0; col < items.Length; col++)
			{
				IItem att = _GetItem(col, items[col]);
				res[col] = PlaceItem(att);
			}

			return res;
		}



		public IItem[] Retrieve(int id) => dic.Where(p => p.Key.Id == id).Select(p => p.Key).ToArray();

		public int[] RetrieveIndicies(int id) => dic.Where(p => p.Key.Id == id).Select(p => p.Value).ToArray();
		
		public IItem RetrieveByIndex(int index) => dic.FirstOrDefault(p => p.Value == index).Key;
	}
}
