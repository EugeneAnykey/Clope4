using System;
using ClopeLib.Helpers;

namespace ClopeLib.Data
{
	public class Transaction : ITransaction
	{
		public static bool PreciseComparing { get; set; }
		public string[] Items { get; }

		public int Length { get { return Items.Length; } }

		readonly int hashCode = 0;



		// init
		static Transaction()
		{
			PreciseComparing = true;
		}

		public Transaction(string[] items)
		{
			this.Items = items ?? throw new NullReferenceException();
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
				t != null && hashCode == t.GetHashCode() && ArrayHelper.Equals(Items, t.Items) :
				t != null && hashCode == t.GetHashCode();
		}



		// HashCode
		int MakeHashCode()
		{
			const int seed = 0xad7f;

			int hash = 0;

			for (int i = 0; i < Items.Length; i++)
			{
				unchecked { hash += seed * ((i + 1) * (Items[i] != null ? Items[i].GetHashCode() : 1)); }
			}

			return hash;
		}

		public override int GetHashCode()
		{
			return hashCode;
		}
	}
}
