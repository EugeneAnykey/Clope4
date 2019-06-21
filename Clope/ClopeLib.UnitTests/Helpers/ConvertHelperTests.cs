using NUnit.Framework;

using ClopeLib.Helpers;

namespace ClopeLib.UnitTests.Helpers
{
	[TestFixture]
	public class ConvertHelperTests
	{
		[TestCase("", null, "")]
		[TestCase("[]", new string[0], null)]
		[TestCase("[a b]", new[] { "a", "b" }, null)]
		[TestCase("[ab, cd, ef]", new[] { "ab", "cd", "ef" }, ", ")]
		public void ConvertToString_MiscInput_IsGood(string expected, object[] array, string separator)
		{
			var result = ConvertHelper.ConvertToString(array, separator);

			Assert.AreEqual(
				expected,
				result
			);
		}



		[Test]
		public void ConvertToString_Int_IsGood()
		{
			const string separator = ", ";
			const string expected = "[3, 5, 7]";
			var array = new[] { 3, 5, 7 };

			var result = ConvertHelper.ConvertToString(array, separator);

			Assert.AreEqual(
				expected,
				result
			);
		}



		[Test]
		public void ConvertToString_Float_IsGood()
		{
			const string separator = "; ";
			var expected = "[3.6; 5.2; 7.9]".Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);

			var array = new[] { 3.6, 5.2, 7.9 };

			var result = ConvertHelper.ConvertToString(array, separator);

			Assert.AreEqual(
				expected,
				result
			);
		}



		// ToStrings - In Array - Returns True
		[Test]
		public void ToStrings_FromArray_IntIsGood()
		{
			var fromArray = new[] { 13 };
			var expectedArray = new string[] { "13" };
			
			var res = fromArray.ToStrings();
			Assert.AreEqual(
				expectedArray,
				res
			);
		}

		[Test]
		public void ToStrings_FromArrayWithNull_StringIsGood()
		{
			var fromArray = new string[] { "b", null, "z" };
			var expectedArray = new string[] { "b", null, "z" };

			var res = fromArray.ToStrings();
			Assert.AreEqual(
				expectedArray,
				res
			);
		}

		[Test]
		public void ToStrings_FromArray_DoubleIsGood()
		{
			var fromArray = new[] { 1.3, 3.9, 0.8 };
			var expectedArray = new string[] { "1.3", "3.9", "0.8" };
			for (int i = 0; i < expectedArray.Length; i++)
			{
				expectedArray[i] = expectedArray[i].Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
			}

			var res = fromArray.ToStrings();
			Assert.AreEqual(
				expectedArray,
				res
			);
		}

		[Test]
		public void ToStrings_FromArray_StringIsGood()
		{
			var fromArray = new[] { "r" };
			var expectedArray = new string[] { "r" };

			var res = fromArray.ToStrings();
			Assert.AreEqual(
				expectedArray,
				res
			);
		}
	}
}
