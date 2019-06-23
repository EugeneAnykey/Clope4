using System;
using ClopeLib.Helpers;

namespace ClopeLib.Data4
{
	public class Transaction4 : ITransaction
	{
		public static bool PreciseComparing { get; set; }
		
		public int[] Links { get; }

		public int Length { get { return Links.Length; } }

		readonly int hashCode = 0;



		// init
		static Transaction4()
		{
			PreciseComparing = true;
		}

		public Transaction4(int[] links)
		{
			Links = links ?? throw new NullReferenceException();
			hashCode = MakeHashCode();
		}



		// Equals
		public override bool Equals(object obj)
		{
			return Equals(obj as ITransaction);
		}

		public bool Equals(ITransaction t)
		{
			return PreciseComparing ?
				t != null && hashCode == t.GetHashCode() && Equ(t) :
				t != null && hashCode == t.GetHashCode();
		}

		bool Equ(ITransaction t) => ArrayHelper.Equals(Links, t.Links);



		// HashCode
		public override int GetHashCode() => hashCode;

		int MakeHashCode() => HashCodeForLinks();

		int HashCodeForLinks()
		{
			const int seed = 0xad7f;

			int hash = 0;

			for (int i = 0; i < Links.Length; i++)
			{
				unchecked { hash += seed * (i + 1) * Links[i].GetHashCode(); }
			}

			return hash;
		}
	}
}
