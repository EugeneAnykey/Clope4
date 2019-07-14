using NUnit.Framework;
using ClopeLib.Helpers;

namespace ClopeLib.UnitTests.Helpers
{
	[TestFixture]
	public class ConvertHelperTests
	{
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
