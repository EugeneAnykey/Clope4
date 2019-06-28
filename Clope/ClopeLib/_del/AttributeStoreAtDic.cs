﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ClopeLib.Data
{
	public class AttributeStoreAtDic : IAttributeStore
	{
		// fields
		IDictionary<IAttribute, int> Dic { get; } = new Dictionary<IAttribute, int>();



		// init
		public AttributeStoreAtDic() { }



		// PlaceAttribute
		public int PlaceAttribute(IAttribute at) => PlaceAttributeAtDic(at);

		int PlaceAttributeAtDic(IAttribute at)
		{
			if (Dic.ContainsKey(at))
			{
				return Dic[at];
			}
			else
			{
				int index = Dic.Count;
				Dic.Add(at, index);
				return index;
			}
		}



		// GetIndices
		public int[] PlaceAndGetLinks(string[] items)
		{
			if (items == null)
				throw new ArgumentNullException();

			if (items.Length == 0)
				throw new EmptyArrayException();

			var res = new int[items.Length];

			for (int i = 0; i < items.Length; i++)
			{
				IAttribute at = new Attribute4(i, items[i]);
				res[i] = PlaceAttribute(at);
			}

			return res;
		}



		// GetAttributes
		public IAttribute[] GetAttributes(int index) => (from a in Dic where a.Key.Position == index select a.Key).ToArray();

		public IAttribute GetAttributeByLink(int id) => Dic.FirstOrDefault(a => a.Key.Link == id).Key;
	}
}