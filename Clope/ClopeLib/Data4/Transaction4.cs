using System;
using ClopeLib.Helpers;

namespace ClopeLib.Data
{
	public class Transaction4 : ITransaction
	{
		static uint uniqueId = 0;

		public uint Id { get; } = uniqueId++;

		public int[] Links { get; }

		public int Length => Links.Length;

		readonly int hashCode = 0;



		// init
		public Transaction4(int[] links)
		{
			Links = links ?? throw new ArgumentNullException();
			hashCode = MakeHashCode();
		}



		// Equals
		//public override bool Equals(object obj) => Equals(obj as ITransaction);
		public override bool Equals(object obj) => EqualsPrecise(obj as ITransaction);

		public bool EqualsPrecise(ITransaction t) => t != null && hashCode == t.GetHashCode() && EqualsArrays(t) && Id == t.Id;

		public bool Equals(ITransaction t) => t != null && hashCode == t.GetHashCode();

		bool EqualsArrays(ITransaction t) => ArrayHelper.Equals(Links, t.Links);



		// HashCode
		public override int GetHashCode() => hashCode;

		int MakeHashCode() => HashCodeForLinks();

		int HashCodeForLinks()
		{
			const int seed = 0xad7f;

			int hash = 0;
			
			unchecked { hash = seed * (int)Id; }

			for (int i = 0; i < Links.Length; i++)
			{
				unchecked { hash += seed * (i + 1) * Links[i].GetHashCode(); }
			}

			return hash;
		}
	}
}
