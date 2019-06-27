using System;
using System.Collections.Generic;
using System.Linq;

namespace ClopeLib.Data
{
	public class AttributeStoreAtList : IAttributeStore
	{
		int latestNewLink = 1;

		// fields
		readonly List<IAttribute> attributes = new List<IAttribute>();



		// init
		public AttributeStoreAtList() { }



		// PlaceAttribute
		int PlaceAttributeAtList(IAttribute at)
		{
			if (!attributes.Contains(at))
			{
				at.Link = latestNewLink++;
				attributes.Add(at);
			}

			return attributes[attributes.IndexOf(at)].Link;
		}



		// IAttributeStore: GetAttributeByLink, GetAttributes, PlaceAndGetLinks
		public IAttribute GetAttributeByLink(int link) => attributes.FirstOrDefault(a => a.Link == link);

		public IAttribute[] GetAttributes(int position) => attributes.Where(a => a.Position == position).ToArray();

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
				res[col] = PlaceAttributeAtList(att);
			}

			return res;
		}
	}
}
