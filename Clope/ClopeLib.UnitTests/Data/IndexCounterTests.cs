using System.Linq;
using NUnit.Framework;
using ClopeLib.Data;

namespace ClopeLib.UnitTests.Data
{
	[TestFixture]
	public class IndexCounterTests
	{
		// factory
		IIndexCounter _GetCounter() =>
			new IndexCounter();


		// Init
		[Test]
		public void Init_Simple_IsGood() => Assert.IsNotNull(_GetCounter());



		// Inc, Dec, Positives
		[Test]
		public void Inc_ToEmpty_IsGood()
		{
			int[] vals = new[] { 1, 7, 17 };

			var counter = _GetCounter();

			counter.Inc(vals);

			Assert.IsTrue(counter.Positives > 0);
		}

		[Test]
		public void IncDec_ToEmpty_IsGood()
		{
			int[] vals = new[] { 1, 7, 17 };

			var counter = _GetCounter();

			counter.Inc(vals);
			counter.Dec(vals);

			Assert.IsTrue(counter.Positives == 0);
		}



		// Positives
		[Test]
		public void Positives_FewTimesInc_OnlyUniqueCounts()
		{
			int[] vals1 = new[] { 1, 7, 17 };
			int[] vals2 = new[] { 1, 5, 13 };

			var expected = vals1.Concat(vals2).Distinct().Count();

			var counter = _GetCounter();

			counter.Inc(vals1);
			counter.Inc(vals2);

			Assert.AreEqual(
				expected,
				counter.Positives
			);
		}

		[Test]
		public void Positives_IncDec_OnlyUniqueCounts()
		{
			int[] vals1 = new[] { 1, 7, 17 };
			int[] vals2 = new[] { 1 };

			var expected = vals1.Except(vals2).Count();
			var counter = _GetCounter();

			counter.Inc(vals1);
			counter.Dec(vals2);

			Assert.AreEqual(
				expected,
				counter.Positives
			);
		}

		[Test]
		public void Positives_TwiceIncOnceDec_OnlyUniqueCounts()
		{
			int[] vals1 = new[] { 1, 7, 17 };
			int[] vals2 = new[] { 1 };

			var expected = vals1.Count();
			var counter = _GetCounter();

			counter.Inc(vals1);
			counter.Inc(vals1);
			counter.Dec(vals2);

			Assert.AreEqual(
				expected,
				counter.Positives
			);
		}



		// indexer (this)
		[Test]
		public void This_TwiceInc_DoubleCountIsGood()
		{
			int[] vals1 = new[] { 1, 7, 17 };
			int[] vals2 = new[] { 1, 5, 13 };

			const int expected = 2;
			var index = vals1.Intersect(vals2).First();
			var counter = _GetCounter();

			counter.Inc(vals1);
			counter.Inc(vals2);

			Assert.AreEqual(
				expected,
				counter[index]
			);
		}

		[Test]
		public void This_NoSuchItem_ZeroIsGood()
		{
			int[] vals1 = new[] { 1, 7, 17 };

			const int expected = 0;
			const int index = 3;
			var counter = _GetCounter();

			counter.Inc(vals1);

			Assert.AreEqual(
				expected,
				counter[index]
			);
		}

		[Test]
		public void This_OnEmpty_ZeroIsGood()
		{
			const int expected = 0;
			const int index = 3;

			Assert.AreEqual(
				expected,
				_GetCounter()[index]
			);
		}
	}
}
