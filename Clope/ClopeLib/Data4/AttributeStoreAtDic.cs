using System;
using System.Collections.Generic;
using System.Linq;

namespace ClopeLib.Data
{
	public class AttributeStoreAtDic : IAttributeStore
	{
		// fields
		int latestNewLink = 0;

		readonly Dictionary<IAttribute, int> dic = new Dictionary<IAttribute, int>();



		// init
		public AttributeStoreAtDic() { }



		// PlaceAttribute
		int PlaceAttributeAtDic(IAttribute at)
		{
			if (!dic.ContainsKey(at))
			{
				dic.Add(at, latestNewLink++);
			}

			return dic[at];
		}



		public IAttribute GetAttributeByLink(int link) => dic.FirstOrDefault(p => p.Value == link).Key;



		public IAttribute[] GetAttributes(int position) => dic.Where(p => p.Key.Position == position).Select(p => p.Key).ToArray();

		public int[] GetAttributesLinks(int position) => dic.Where(p => p.Key.Position == position).Select(p => p.Value).ToArray();



		public int[] PlaceAndGetLinks(params string[] items)
		{
			if (items == null)
				throw new ArgumentNullException();

			if (items.Length == 0)
				throw new EmptyArrayException();

			var res = new int[items.Length];

			for (int col = 0; col < items.Length; col++)
			{
				IAttribute att = new Attribute4(col, items[col]);
				res[col] = PlaceAttributeAtDic(att);
			}

			return res;
		}
	}
}
