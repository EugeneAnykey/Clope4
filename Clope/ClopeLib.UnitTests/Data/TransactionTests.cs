using System;
using System.Collections.Generic;
using ClopeLib.Data;
using NUnit.Framework;

namespace ClopeLib.UnitTests.Data
{
	[TestFixture]
	public class TransactionTests
	{
		[Test]
		public void Init_NullLinks_ThrowsArgumentNull()
		{
			int[] links = null;

			Assert.Catch<ArgumentNullException>(() => new Transaction4(links));
		}



		[Test]
		public void Equals_SameArray_IsTrue()
		{
			var links = new[] { 1, 3, 7 };
			var trans1 = new Transaction4(links);
			var trans2 = new Transaction4(links);

			var equals = trans1.Equals(trans2);

			Assert.IsTrue(equals);
		}



		[Test]
		public void Equals_SameArrayChanged_IsFalse()
		{
			var links = new[] { 1, 3, 7 };
			var trans1 = new Transaction4(links);
			links[0] = 2;
			var trans2 = new Transaction4(links);

			var equals = trans1.Equals(trans2);

			Assert.IsFalse(equals);
		}



		[Test]
		public void Equals_SimilarArrays_IsTrue()
		{
			var links1 = new[] { 1, 3, 7 };
			var links2 = new[] { 1, 3, 7 };
			var trans1 = new Transaction4(links1);
			var trans2 = new Transaction4(links2);

			var equals = trans1.Equals(trans2);

			Assert.IsTrue(equals);
		}



		[TestCase(0, new[] { 1, 7, 19 }, new[] { 1, 7, 23 })]  // near similar
		[TestCase(1, new[] { 3, 7, 19 }, new[] { 1, 5, 23 })]  // different
		[TestCase(2, new[] { 5, 1, 23 }, new[] { 1, 5, 23 })]  // different
		public void Equals_DifferentArrays_IsFalse(int id, int[] links1, int[] links2)
		{
			var lines1 = new[] { 3, 7, 19 };
			var lines2 = new[] { 1, 5, 23 };
			var trans1 = new Transaction4(links1);
			var trans2 = new Transaction4(links2);

			var equals = trans1.Equals(trans2);

			Assert.IsFalse(equals);
		}



		List<int> hashes = new List<int>();

		[TestCase(01, new int[0])]
		[TestCase(02, new int[] { 1 })]
		[TestCase(03, new[] { 2 })]
		[TestCase(04, new[] { 1, 3 })]
		[TestCase(05, new[] { 3, 1 })]
		[TestCase(06, new[] { 7, 5 })]
		[TestCase(07, new[] { 2, 6, 9 })]
		[TestCase(08, new[] { 2, 6, 9, 15, 23, 41 })]
		public void GetHashCode_MiscInput_IsGood(int notExpected, int[] links)
		{
			var t = new Transaction4(links);
			var result = t.GetHashCode();

			Assert.IsFalse(
				hashes.Contains(result)
			);

			hashes.Add(result);
		}
	}
}
