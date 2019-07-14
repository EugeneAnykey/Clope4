using NUnit.Framework;
using ClopeLib.Helpers;

namespace ClopeLib.UnitTests.Helpers
{
	[TestFixture]
	public class ArrayHelperTests
	{
		#region Equals
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
		#endregion
	}
}
