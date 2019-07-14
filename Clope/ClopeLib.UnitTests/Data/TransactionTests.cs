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

			Assert.Catch<ArgumentNullException>(() => new Transaction(links));
		}



		[Test]
		public void Equals_OneArray_IsFalse()
		{
			var links = new[] { 1, 3, 7 };
			var trans1 = new Transaction(links);
			var trans2 = new Transaction(links);

			Assert.IsFalse(trans1.Equals(trans2));
		}



		[Test]
		public void Equals_SameArrayChanged_IsFalse()
		{
			var links = new[] { 1, 3, 7 };
			var trans1 = new Transaction(links);
			links[0] = 2;
			var trans2 = new Transaction(links);

			var equals = trans1.Equals(trans2);

			Assert.IsFalse(equals);
		}



		[Test]
		public void Equals_SimilarArrays_IsFalse()
		{
			var links1 = new[] { 1, 3, 7 };
			var links2 = new[] { 1, 3, 7 };
			var trans1 = new Transaction(links1);
			var trans2 = new Transaction(links2);

			Assert.IsFalse(trans1.Equals(trans2));
		}



		[TestCase(0, new[] { 1, 7, 19 }, new[] { 1, 7, 23 })]  // near similar
		[TestCase(1, new[] { 3, 7, 19 }, new[] { 1, 5, 23 })]  // different
		[TestCase(2, new[] { 5, 1, 23 }, new[] { 1, 5, 23 })]  // different
		public void Equals_DifferentArrays_IsFalse(int id, int[] links1, int[] links2)
		{
			var lines1 = new[] { 3, 7, 19 };
			var lines2 = new[] { 1, 5, 23 };
			var trans1 = new Transaction(links1);
			var trans2 = new Transaction(links2);

			var equals = trans1.Equals(trans2);

			Assert.IsFalse(equals);
		}



		List<int> hashes = new List<int>();

		[TestCase('a', new int[0])]
		[TestCase('b', new int[0])]
		[TestCase('c', new[] { 1 })]
		[TestCase('d', new[] { 2 })]
		[TestCase('e', 1, 3)]
		[TestCase('f', 1, 3)]
		[TestCase('g', 3, 1)]
		[TestCase('h', 7, 5)]
		[TestCase('i', 2, 6, 9)]
		[TestCase('j', 2, 6, 9, 15, 23, 41)]
		public void GetHashCode_MiscInput_IsGood(char dummy, params int[] links)
		{
			var t = new Transaction(links);
			var result = t.GetHashCode();

			Assert.IsFalse(
				hashes.Contains(result)
			);

			hashes.Add(result);
		}
	}
}
