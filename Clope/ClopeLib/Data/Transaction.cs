using System;
using ClopeLib.Helpers;

namespace ClopeLib.Data
{
	public class Transaction : ITransaction
	{
		static uint uniqueId = 0;

		public uint Id { get; } = uniqueId++;

		public int[] Links { get; }

		public int Length => Links.Length;

		readonly int hashCode = 0;



		// init
		public Transaction(int[] links)
		{
			Links = links ?? throw new ArgumentNullException();
			hashCode = MakeHashCode();
		}



		// Equals
		public bool Equals(ITransaction t) => t != null && Id == t.Id && hashCode == t.GetHashCode() && EqualsArrays(t);

		bool EqualsArrays(ITransaction t) => ArrayHelper.Equals(Links, t.Links);



		// HashCode
		public override int GetHashCode() => hashCode;

		int MakeHashCode()
		{
			const int seed = 0xad7f;

			int hash = 0;

			unchecked { hash = seed * ((int)Id); }

			for (int i = 0; i < Links.Length; i++)
			{
				unchecked { hash += seed * ((i + 1 + (int)Id) * Links[i].GetHashCode()); }
			}

			return hash;
		}
	}
}
