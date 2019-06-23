using System;
using System.Collections.Generic;
using System.Linq;
using ClopeLib.Data4;

namespace ClopeLib.Algo4
{
	public class AttributeStoreAtList : IAttributeStore
	{
		// fields
		readonly List<IAttribute> attributes = new List<IAttribute>();



		// init
		public AttributeStoreAtList() { }



		// PlaceAttribute
		public int PlaceAttribute(IAttribute at) => PlaceAttributeAtList(at);

		int PlaceAttributeAtList(IAttribute at)
		{
			if (!attributes.Contains(at))
				attributes.Add(at);

			//if (attributes.Any(a => a.Index == at.Index && a.Name == at.Name))
			//	;

			return attributes.IndexOf(at);
		}



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
