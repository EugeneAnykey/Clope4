using System;
using System.Collections.Generic;
using ClopeLib.Data4;

namespace ClopeLib.Algo4
{
	public class AttributeStore
	{
		// asd
		readonly List<IAttribute> attributes = new List<IAttribute>();
		readonly IDictionary<int, IAttribute> dic1 = new Dictionary<int, IAttribute>();
		IDictionary<IAttribute, int> Dic { get; } = new Dictionary<IAttribute, int>();



		// init
		public AttributeStore()
		{
			//attributes = new List<IAttribute>();

		}

		public int Add(IAttribute at)
		{
			if (!attributes.Contains(at))
			{
				attributes.Add(at);
				//dic1.Add(dic1.Count, at);
			}

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
		public int[] GetIndices(string[] items)
		{
			if (items == null)
				throw new ArgumentNullException();

			if (items.Length == 0)
				throw new ArgumentException();

			var res = new int[items.Length];

			for (int i = 0; i < items.Length; i++)
			{
				IAttribute at = new TransactionAttribute4(0, i, items[i]);
				res[i] = Add(at);
			}

			return res;
		}



		//public int GetIndex(IAttribute attr) => Dic[attr];
	}
}
