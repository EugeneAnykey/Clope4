using System;
using System.Collections.Generic;
using ClopeLib.Data4;

namespace ClopeLib.Algo4
{
	// not for use
	public class AttributeStoreAtDicReversed : IAttributeStore
	{
		// fields
		readonly IDictionary<int, IAttribute> Dic1 = new Dictionary<int, IAttribute>();



		// init
		public AttributeStoreAtDicReversed() { }



		// PlaceAttribute
		public int PlaceAttribute(IAttribute at) => PlaceAttributeAtDic(at);

		int PlaceAttributeAtDic(IAttribute at)
		{
			// if contains value
			// otherwise add
			// return key
			throw new NotImplementedException();
		}



		/*
		int GetValue<TKey>(IDictionary<TKey, int> dic, TKey key)
		{
			if (dic.ContainsKey(key))
				return dic[key];
			else
			{
				int pos = dic.Count;
				dic.Add(key, pos);
				return pos;
			}
		}



		TValue GetValue<TKey, TValue>(IDictionary<TKey, TValue> dic, TKey key, TValue val)
		{
			if (dic.ContainsKey(key))
				return dic[key];
			else
			{
				dic.Add(key, val);
				return val;
			}
		}
		*/



		// GetIndices
		public int[] GetIndices(string[] items)
		{
			if (items == null)
				throw new ArgumentNullException();

			if (items.Length == 0)
				throw new EmptyArrayException();

			var res = new int[items.Length];

			for (int i = 0; i < items.Length; i++)
			{
				IAttribute at = new TransactionAttribute4(0, i, items[i]);
				res[i] = PlaceAttribute(at);
			}

			return res;
		}
	}
}
