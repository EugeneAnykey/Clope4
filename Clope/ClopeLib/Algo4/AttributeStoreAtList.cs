using System;
using System.Collections.Generic;
using System.Linq;
using ClopeLib.Data4;

namespace ClopeLib.Algo4
{
	public class AttributeStoreAtList : IAttributeStore
	{
		int LastId = 0;

		// fields
		readonly List<IAttribute> attributes = new List<IAttribute>();



		// init
		public AttributeStoreAtList() { }



		// PlaceAttribute
		public int PlaceAttribute(IAttribute at) => PlaceAttributeAtList(at);

		int PlaceAttributeAtList(IAttribute at)
		{
			if (!attributes.Contains(at))
			{
				attributes.Add(at);
			}

			return attributes.IndexOf(at);
		}



		// PlaceAndGetIndices
		public int[] PlaceAndGetIndices(string[] items)
		{
			// not indicies but ids
			if (items == null)
				throw new ArgumentNullException();

			if (items.Length == 0)
				throw new EmptyArrayException();

			var res = new int[items.Length];

			for (int col = 0; col < items.Length; col++)
			{
				IAttribute att = new TransactionAttribute4(LastId++, col, items[col]);
				//res[col] = PlaceAttribute(att);
				PlaceAttribute(att);
				res[col] = att.Id;
			}

			return res;
		}



		// GetAttributes
		public IAttribute[] GetAttributes(int position) => (from a in attributes where a.Position == position select a).ToArray();

		public IAttribute GetAttribute(int index) => 0 <= index && index < attributes.Count ? attributes[index] : null;

		//public IAttribute GetAttributeById(int id) => (from a in attributes where a.Id == id select a).FirstOrDefault();
		public IAttribute GetAttributeById(int id) => attributes.FirstOrDefault(a => a.Id == id);
	}
}
