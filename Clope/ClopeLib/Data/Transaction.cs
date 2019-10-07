using System;

namespace ClopeLib.Data
{
	public class Transaction : ITransaction
	{
		static long uniqueId = 0;



		public long Id { get; } = uniqueId++;

		public int[] Links { get; }

		public int Length => Links.Length;



		// init
		public Transaction(params int[] links)
		{
			Links = links ?? throw new ArgumentNullException();

			if (links.Length == 0)
				throw new EmptyArrayException();
		}



		// Equals
		public bool Equals(ITransaction t) => t != null && Id == t.Id;
	}
}
