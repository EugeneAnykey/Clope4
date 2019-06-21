using NUnit.Framework;

using ClopeLib.Helpers;
using System;

namespace ClopeLib.UnitTests.Helpers
{
	[TestFixture]
	public class ArrayHelperTests
	{
		// Equals
		[Test]
		public void Equals_Simple_IsTrue()
		{
			var lines1 = new[] { "a", "b" };
			var lines2 = lines1;

			var res = ArrayHelper.Equals(lines1, lines2);
			Assert.IsTrue(res);
		}



		[TestCase(new string[0], new string[0])]
		[TestCase(new[] { "a" }, new[] { "a" })]
		[TestCase(new[] { "a", "b" }, new[] { "a", "b" })]
		public void Equals_MiscStrings_IsTrue(string[] lines1, string[] lines2)
		{
			var res = ArrayHelper.Equals(lines1, lines2);

			Assert.IsTrue(res);
		}



		[TestCase(new string[0], new string[] { null })]
		[TestCase(new string[] { null }, new string[0])]
		[TestCase(new string[] { null }, new string[] { null })]
		[TestCase(new[] { "a" }, new string[] { null })]
		[TestCase(new[] { "a" }, new[] { " b" })]
		[TestCase(new[] { "a", "d" }, new[] { "b" })]
		[TestCase(new[] { "a", "d" }, new[] { "a", "c" })]
		public void Equals_MiscStrings_IsFalse(string[] lines1, string[] lines2)
		{
			var res = ArrayHelper.Equals(lines1, lines2);

			Assert.IsFalse(res);
		}



		[TestCase(new int[0], new int[0])]
		[TestCase(new[] { 4 }, new[] { 4 })]
		[TestCase(new[] { 4, 2 }, new[] { 4, 2 })]
		public void Equals_MiscInts_IsTrue(int[] lines1, int[] lines2)
		{
			var res = ArrayHelper.Equals(lines1, lines2);

			Assert.IsTrue(res);
		}



		[TestCase(new int[0], new[] { 3 })]
		[TestCase(new[] { 4 }, new[] { 2 })]
		[TestCase(new[] { 4, 2 }, new[] { 4 })]
		[TestCase(new[] { 4, 2 }, new[] { 4, 3 })]
		public void Equals_MiscInts_IsFalse(int[] lines1, int[] lines2)
		{
			var res = ArrayHelper.Equals(lines1, lines2);

			Assert.IsFalse(res);
		}



		// Contains - In Array - Returns True
		[TestCase(13, new[] { 13 })]
		[TestCase(5, new[] { 1, 17, 5 })]
		[TestCase(17, new[] { 17, 13, 5 })]
		[TestCase(1.3, new[] { 3.9, 1.3, 1.0 })]
		[TestCase("b", new[] { "r", "b", "e" })]
		public void Contains_InArray_ReturnsTrue<T>(T item, T[] inArray) where T : IEquatable<T>
		{
			var res = inArray.Contains(item);
			Assert.IsTrue(res);
		}



		// Contains - Not In Array - Returns False
		[TestCase(4, new int[0])]
		[TestCase(7, new[] { 1, 3, 5 })]
		[TestCase(1.2, new[] { 3.9, 1.3, 1.0 })]
		[TestCase("v", new[] { "r", "b", "e" })]
		public void Contains_NotInArray_ReturnsFalse<T>(T item, T[] inArray) where T : IEquatable<T>
		{
			var res = inArray.Contains(item);
			Assert.IsFalse(res);
		}



		// Contains - Nulls - Throw ArgumentNull (! some values couldn't be null, i.e. int.)
		[TestCase(null, new[] { "a", "b" })]
		[TestCase("a", null)]
		public void Contains_Nulls_ThrowArgumentNull(string item, string[] inArray)
		{
			Assert.Catch<ArgumentNullException>(() => ArrayHelper.Contains(inArray, item));
		}




		// Position - In Array - item Exists
		[TestCase(3, 0, new[] { 3 })]
		[TestCase(1, 0, new[] { 1, 3 })]
		[TestCase(5, 2, new[] { 1, 3, 5 })]
		[TestCase(1.3, 1, new[] { 3.9, 1.3, 1.0 })]
		[TestCase("b", 1, new[] { "r", "b", "e" })]
		public void Position_InArray_Exists<T>(T item, int index, T[] inArray) where T : IEquatable<T>
		{
			var res = inArray.Position(item);
			Assert.AreEqual(
				index,
				res
			);
		}



		// Position - Not In Array - item Not Exists
		[TestCase(4, new int[0])]
		[TestCase(7, new[] { 1, 3, 5 })]
		[TestCase(1.2, new[] { 3.9, 1.3, 1.0 })]
		[TestCase("v", new[] { "r", "b", "e" })]
		public void Position_NotInArray_NotExists<T>(T item, T[] inArray) where T : IEquatable<T>
		{
			const int index = -1;

			var res = inArray.Position(item);
			Assert.AreEqual(
				index,
				res
			);
		}
	}
}
