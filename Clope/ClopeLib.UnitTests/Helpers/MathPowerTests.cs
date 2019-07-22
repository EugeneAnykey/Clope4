using System;
using NUnit.Framework;
using ClopeLib.Helpers;

namespace ClopeLib.UnitTests.Helpers
{
	[TestFixture]
	public class MathPowerTests
	{
		const double eps = 0.00001;



		[Test]
		public void Init_ZeroExponent_ReturnsOne()
		{
			const double expected = 1;
			var mp = new MathPower(0);
			Assert.AreEqual(
				expected,
				mp[0],
				eps
			);
		}



		[Test]
		public void Init_NonZeroExponent_ReturnsZero()
		{
			const double expected = 0;
			var mp = new MathPower(2.5);
			Assert.AreEqual(
				expected,
				mp[0],
				eps
			);
		}



		[TestCase(0)]
		[TestCase(1)]
		[TestCase(3)]
		[TestCase(87)]
		[TestCase(387)]
		public void Indexer_PositiveValue87_IsGood(int val)
		{
			const double pow = 2.5;
			var mp = new MathPower(pow);
			var expected = Math.Pow(val, pow);
			Assert.AreEqual(
				expected,
				mp[val],
				eps
			);
		}



		[Test]
		public void Indexer_NegativeValue_ThrowsNegativeValue()
		{
			double p;
			Assert.Catch<NegativeValueException>(() => p = new MathPower(2)[-2] );
		}
	}
}
