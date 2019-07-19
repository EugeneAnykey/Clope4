using System;
using System.Linq;
using NUnit.Framework;
using ClopeLib.Data;
using ClopeLib.Helpers;

namespace ClopeLib.UnitTests.Data
{
	[TestFixture]
	public class ClusterTests
	{
		// helper
		readonly MathPower mathPower = new MathPower(2);

		ICluster _GetCluster() => new Cluster(mathPower);



		[Test]
		public void Init_NullMathPowers_ThrowsArgumentNull() => Assert.Catch<ArgumentNullException>(() => new Cluster(null));

		[Test]
		public void Init_Normal_IsGood() => Assert.AreEqual(0, _GetCluster().Area);

		[Test]
		public void Init_Normal_IsEmpty() => Assert.IsTrue(_GetCluster().IsEmpty);

		[Test]
		public void Init_Normal_NoTransactions() => Assert.AreEqual(0, _GetCluster().TransactionsCount);



		[TestCase('a', new[] { 1 }, new[] { 1 })]
		[TestCase('b', new[] { 1 }, new[] { 3 })]
		[TestCase('c', new[] { 1, 3 }, new[] { 2, 4 })]
		public void GetAddCost_DifferentTransactionsSameLength_Equal(char a, int[] links1, int[]links2)
		{
			var cost1 = _GetCluster().GetAddCost(new Transaction(links1));
			var cost2 = _GetCluster().GetAddCost(new Transaction(links2));

			Assert.AreEqual(
				cost1,
				cost2
			);
		}



		[TestCase('a', new[] { 1 }, new[] { 1, 2 })]
		[TestCase('b', new[] { 1, 2 }, new[] { 3 })]
		[TestCase('c', new[] { 1, 3, 7, 8 }, new[] { 2, 4 })]
		public void GetAddCost_DifferentTransactionsDifLength_NotEqual(char a, int[] links1, int[] links2)
		{
			var cost1 = _GetCluster().GetAddCost(new Transaction(links1));
			var cost2 = _GetCluster().GetAddCost(new Transaction(links2));

			Assert.AreNotEqual(
				cost1,
				cost2
			);
		}



		[TestCase(1)]
		[TestCase(1, 2)]
		[TestCase(1, 3, 7, 8)]
		public void GetAddCost_SecondTime_DiffersWithPreviousTime(params int[] links)
		{
			var t = new Transaction(links);
			var c = _GetCluster();
			var cost1 = c.GetAddCost(t);
			c.Add(t);
			var cost2 = c.GetAddCost(t);

			Assert.AreNotEqual(
				cost1,
				cost2
			);
		}



		[Test]
		public void GetAddCost_NoInput_ReturnsZero()
		{
			var cost = _GetCluster().GetAddCost(null);

			Assert.AreEqual(
				0,
				cost
			);
		}



		[TestCase(1)]
		[TestCase(1, 3, 7)]
		[TestCase(1, 3, 5, 7, 11, 13)]
		public void Width_SingleTransactions_IsGood(params int[] links)
		{
			var c = _GetCluster();
			c.Add(new Transaction(links));

			Assert.AreEqual(
				links.Length,
				c.Width
			);
		}



		[TestCase(1)]
		[TestCase(1, 3, 7)]
		[TestCase(1, 3, 5, 7, 11, 13)]
		public void Width_SameTransactions_IsGood(params int[] links)
		{
			var c = _GetCluster();
			c.Add(new Transaction(links));
			c.Add(new Transaction(links));

			Assert.AreEqual(
				links.Length,
				c.Width
			);
		}



		[TestCase(1, 2)]
		[TestCase(1, 3, 7)]
		[TestCase(1, 3, 5, 7, 11, 13)]
		[TestCase(11, 13, 17, 19)]
		public void Width_MiscTransactions_IsGood(params int[] links)
		{
			int[] constLinks = new[] { 1, 2, 3, 4, 5};

			int diffs = new[] { 1, 2, 3, 4, 5 }.Union(links).Count();

			var c = _GetCluster();
			c.Add(new Transaction(constLinks));
			c.Add(new Transaction(links));

			Assert.AreEqual(
				diffs,
				c.Width
			);
		}



		[TestCase(0)]
		[TestCase(1)]
		[TestCase(3)]
		[TestCase(7)]
		public void Add_SeveralTransactions_CountEquals(int timesAdd)
		{
			var c = _GetCluster();
			for (int i = 0; i < timesAdd; i++)
			{
				c.Add(new Transaction(new[] { 1 }));
			}

			Assert.AreEqual(
				timesAdd,
				c.TransactionsCount
			);
		}



		[TestCase(0)]
		[TestCase(1)]
		[TestCase(3)]
		[TestCase(7)]
		public void Occurrence_SeveralTransactions_CountEquals(int timesAdd)
		{
			const int attribute = 3;

			var c = _GetCluster();
			for (int i = 0; i < timesAdd; i++)
			{
				c.Add(new Transaction(new[] { attribute }));
			}

			Assert.AreEqual(
				timesAdd,
				c.Occurrence(attribute)
			);
		}
	}
}
