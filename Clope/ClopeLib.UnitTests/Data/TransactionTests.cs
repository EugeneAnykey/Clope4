using System;
using NUnit.Framework;

using ClopeLib.Data;
using System.Collections.Generic;

namespace ClopeLib.UnitTests.Data
{
	[TestFixture]
	public class TransactionTests
	{
		[Test]
		public void Init_NullItems_ThrowsNullReference()
		{
			string[] lines = null;

			Assert.Catch<NullReferenceException>(() => new Transaction(lines));
		}

		[Test]
		public void Equals_SingleArray_IsTrue()
		{
			var lines = new[] { "a", "c" };
			var trans1 = new Transaction(lines);
			var trans2 = new Transaction(lines);

			var equals = trans1.Equals(trans2);

			Assert.IsTrue(equals);
		}

		[Test]
		public void Equals_SingleArrayChanged_IsFalse()
		{
			var lines = new[] { "a", "c" };
			var trans1 = new Transaction(lines);
			lines[0] = "b";
			var trans2 = new Transaction(lines);

			var equals = trans1.Equals(trans2);

			Assert.IsFalse(equals);
		}

		[Test]
		public void Equals_DifferentArrays_IsTrue()
		{
			var lines1 = new[] { "a", "c" };
			var lines2 = new[] { "a", "c" };
			var trans1 = new Transaction(lines1);
			var trans2 = new Transaction(lines2);

			var equals = trans1.Equals(trans2);

			Assert.IsTrue(equals);
		}

		[Test]
		public void Equals_DifferentArrays_GoodIsFalse()
		{
			var lines1 = new[] { "a", "c" };
			var lines2 = new[] { "a", "d" };
			var trans1 = new Transaction(lines1);
			var trans2 = new Transaction(lines2);

			var equals = trans1.Equals(trans2);

			Assert.IsFalse(equals);
		}



		List<int> hashes = new List<int>();

		[TestCase(01, new string[0])]
		[TestCase(02, new string[] { null })]
		[TestCase(03, new string[] { null, null })]
		[TestCase(04, new[] { "" })]
		[TestCase(05, new[] { "a" })]
		[TestCase(06, new[] { "a", "b" })]
		[TestCase(07, new[] { "a", "b", "c" })]
		[TestCase(08, new[] { null, "b", "c" })]
		[TestCase(09, new[] { "a", null, "c" })]
		[TestCase(10, new[] { "a", null, null })]
		[TestCase(11, new[] { "a", "b", null })]
		[TestCase(12, new[] { "a", "b", "c", "z", "y", "x" })]
		[TestCase(13, new[] { "a", "b", "c", "x", "z", "y" })]
		[TestCase(14, new[] { "a", null, "c", "z", "y", "x" })]
		[TestCase(15, new[] { "a", "bc", "def", "ghij", "klmno", "pqrstu" })]
		public void GetHashCode_MiscInput_IsGood1(int notExpected, string[] items)
		{
			var t = new Transaction(items);
			var result = t.GetHashCode();

			Assert.IsFalse(
				hashes.Contains(result)
			);

			hashes.Add(result);
		}
	}
}
