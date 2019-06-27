using System;
using System.Collections.Generic;
using System.Linq;

namespace ClopeLib.Data
{
	// currently not for use
	public class AttributeStoreAtDicReversed : IAttributeStore
	{
		// (<int, IAttribute> == <attr.link, attr>)

		// fields
		readonly IDictionary<int, IAttribute> Dic = new Dictionary<int, IAttribute>();



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
		public IAttribute[] GetAttributes(int index) => (from a in Dic where a.Value.Position == index select a.Value).ToArray();

		public IAttribute GetAttributeByLink(int id) => Dic.FirstOrDefault(a => a.Value.Link == id).Value;
	}
}
